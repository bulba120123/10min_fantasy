using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnumTypes;
using DG.Tweening;

public class Enemy : MonoBehaviour
{
    public EnemyType enemyType;
    public float speed;
    public float damage;
    public float knockBackResistance;
    public float acceleration;
    public RuntimeAnimatorController[] animCon;
    public Rigidbody2D target;
    public bool isGhost;
    public int exp;
    public int goldCount;

    public List<EnemyProjectileData> enemyProjectileDatas;

    Rigidbody2D rigid;
    Collider2D coll;
    Animator anim;
    SpriteRenderer spriter;
    WaitForFixedUpdate wait;
    EnemyWeapon enemyWeapon;
    HealthObject healthObject;
    DamageObject damageObject;

    private WaitForSeconds deadAfterSeconds = new WaitForSeconds(3f);

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();
        spriter = GetComponent<SpriteRenderer>();
        wait = new WaitForFixedUpdate();
        enemyWeapon = GetComponentInChildren<EnemyWeapon>();
        healthObject = GetComponent<HealthObject>();
        damageObject = GetComponent<DamageObject>();
    }


    private void FixedUpdate()
    {
        if (!GameManager.instance.isLive)
            return;
        if (!GetComponent<HealthObject>().isLive || anim.GetCurrentAnimatorStateInfo(0).IsName("Hit"))
            return;
        Vector2 dirVec;
        Vector2 nextVec;
        switch (enemyType)
        {
            case EnemyType.DULLAHAN:
                dirVec = target.position - rigid.position;
                rigid.AddForce(dirVec * acceleration);
                break;
            default:
                dirVec = target.position - rigid.position;
                nextVec = dirVec.normalized * speed * Time.fixedDeltaTime;
                rigid.MovePosition(rigid.position + nextVec);
                rigid.velocity = Vector2.zero;
                break;
        }
    }

    private void LateUpdate()
    {
        if (!GetComponent<HealthObject>().isLive)
            return;
        spriter.flipX = target.position.x < rigid.position.x;
    }

    private void OnEnable()
    {
        target = GameManager.instance.player.GetComponent<Rigidbody2D>();
        coll.enabled = true;
        rigid.simulated = true;
        spriter.sortingOrder = 2;
        // anim.SetBool("Dead", false);
    }

    public void Init(EnemyData data)
    {
        healthObject.Init(HealthObjectType.ENEMY, data.health, data.knockBackResistance);
        damageObject.Init(data.damage);

        enemyType = data.enemyType;
        anim.runtimeAnimatorController = animCon[(int)data.enemyType];
        speed = data.speed;
        damage = data.damage;
        knockBackResistance = data.knockBackResistance;
        acceleration = data.acceleration;
        isGhost = data.isGhost;
        exp = data.exp;
        goldCount = data.goldCount;
        if (isGhost) { 
            coll.gameObject.layer = 6;
            spriter.sortingOrder = 3;
        }
        switch (data.enemyType)
        {
            case EnemyType.UNDEAD_SOLDIER:
                enemyWeapon.gameObject.SetActive(false);
                break;
            case EnemyType.UNDEAD_ARCHER:
                enemyWeapon.gameObject.SetActive(true);
                enemyWeapon.Init(this, enemyProjectileDatas[0]);
                break;
            case EnemyType.UNDEAD_WARRIOR:
                enemyWeapon.gameObject.SetActive(false);
                break;
            case EnemyType.UNDEAD_MAGE:
                enemyWeapon.gameObject.SetActive(true);
                enemyWeapon.Init(this, enemyProjectileDatas[1]);
                break;
            case EnemyType.GHOUL:
                enemyWeapon.gameObject.SetActive(false);
                break;
            case EnemyType.DULLAHAN:
                enemyWeapon.gameObject.SetActive(false);
                break;
        }
    }

    public void Hit()
    {
        anim.SetTrigger("Hit");
    }

    public void Dead()
    {
        GameManager.instance.kill++;
        GameManager.instance.GetExp(exp);
        spriter.sortingOrder = 1;
        anim.SetBool("Dead", true);
        for (int i = 0; i < goldCount; i++)
        {
            var gold = GameManager.instance.pool.Get(PoolType.Gold).transform;
            gold.position = transform.position;
            DoTweenUtil.DOVectorJump(gold, gold.transform.position + new Vector3(Random.Range(-3, 3), Random.Range(-3, 3), 0), Vector3.back, Random.Range(2f, 3f), Random.Range(0.5f, 1.5f));
            StartCoroutine(CoDead());
        }
    }

    IEnumerator CoDead()
    {
        yield return deadAfterSeconds;
        GameManager.instance.spawner.DecreaseEnemyCount(enemyType);
        GameManager.instance.pool.Release(gameObject);
        GameManager.instance.killMonsterEvent.Invoke();
        yield break;
    }

    public void Reposition()
    {
        GameManager.instance.spawner.MoveEnemy(gameObject);
    }
}
