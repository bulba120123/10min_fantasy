using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnumTypes;
using DG.Tweening;
using Sirenix.OdinInspector;

public abstract class BaseBoss : SerializedMonoBehaviour
{
    private FSM _fsm;

    public int _curState;

    //public BossType bossType;
    public float speed;
    public float damage;

    //public RuntimeAnimatorController[] animCon;
    public Rigidbody2D target;

    public int exp;
    public int goldCount;
    public Transform renderTransform;
    public bool isMove = true;
    public bool isRotate = true;
    public bool isAction = false;
    public float cooltime = 1f;

    public Vector3 playerVec;

    public Rigidbody2D rigid;
    public Collider2D[] colls;
    public Animator anim;
    public SpriteRenderer[] spriters;
    public HealthObject healthObject;
    public DamageObject[] damageObjects;
    public BossScanner bossScanner;

    public Dictionary<BossSkillType, GameObject> skillObjects = new Dictionary<BossSkillType, GameObject>();
    private void Awake()
    {
        rigid = rigid == null ? GetComponent<Rigidbody2D>() : rigid;
        colls = GetComponentsInChildren<Collider2D>();
        anim = GetComponent<Animator>();
        spriters = GetComponentsInChildren<SpriteRenderer>();
        //bossWeapon = GetComponentInChildren<BossWeapon>();
        healthObject = healthObject == null ? GetComponent<HealthObject>() : healthObject;
        damageObjects = GetComponentsInChildren<DamageObject>();
        bossScanner = bossScanner == null ? GetComponent<BossScanner>() : bossScanner;
    }

    private void OnEnable()
    {
        target = GameManager.instance.player.GetComponent<Rigidbody2D>();
        foreach (var coll in colls) { coll.enabled = true; }
        rigid.simulated = true;
        //foreach (var spriter in spriters)
        //{
        //    spriter.sortingOrder = 2;
        //}
        //anim.SetBool("Dead", false);
    }

    private void FixedUpdate()
    {
        playerVec = GameManager.instance.playerPosWithZConvert - transform.position;

        if (!healthObject.isLive)
            return;
        if (!isMove)
            return;
        FixedUpdateFunc();
    }

    public virtual void FixedUpdateFunc()
    {
        Vector2 nextVec = playerVec.normalized * speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVec);
        rigid.velocity = Vector2.zero;
    }

    private void LateUpdate()
    {
        if (!healthObject.isLive)
            return;
        LateUpdateFunc();
    }

    public virtual void LateUpdateFunc()
    {

    }

    public virtual IEnumerator SwitchingPattern()
    {
        _curState = 0;
        int nextState = 0;
        while (true)
        {
            yield return new WaitForSeconds(1f);
            switch (_curState)
            {
                case 0:
                    ChangeState(nextState);
                    break;
            }
        }
    }

    private void ChangeState(int nextState)
    {
        _curState = nextState;
        switch (_curState)
        {
            case 0:
                _fsm.ChangeState(new IdleState(this));
                break;
        }
    }


    public void Init(BossData data)
    {
        healthObject.Init(HealthObjectType.BOSS, data.health, data.knockBackResistance);
        healthObject.InitBoss(this);
        foreach (var damageObject in damageObjects)
        { damageObject.Init(data.damage); }

        //bossType = data.bossType;
        //anim.runtimeAnimatorController = animCon[(int)data.bossType];
        speed = data.speed;
        damage = data.damage;
        exp = data.exp;
        goldCount = data.goldCount;
        InitByBoss();
    }
    public virtual void InitByBoss() { }

    public void Hit()
    {
        Debug.Log("Boss Hit");
        // anim.SetTrigger("hit");
    }

    public void Dead()
    {
        GameManager.instance.kill++;
        GameManager.instance.GetExp(exp);
        for (int i = 0; i < goldCount; i++)
        {
            var gold = GameManager.instance.pool.Get(PoolType.Gold).transform;
            gold.position = transform.position;
            DoTweenUtil.DOVectorJump(gold, gold.transform.position + new Vector3(Random.Range(-3, 3), Random.Range(-3, 3), 0), Vector3.back, 3, 1f);
        }
        foreach (var spriter in spriters)
        {
            spriter.sortingOrder = 1;
        }

        GameManager.instance.pool.Release(gameObject);
    }
}
