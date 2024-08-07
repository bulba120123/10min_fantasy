using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnumTypes;

public class HealthObject : MonoBehaviour
{
    public float health;
    public float maxHealth;
    public float knockBackResistance;
    public bool isLive;

    public HealthObjectType healthObjectType;
    Rigidbody2D rigid;
    Collider2D coll;
    WaitForFixedUpdate wait;
    public BaseBoss baseBoss;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        wait = new WaitForFixedUpdate();
    }

    private void OnEnable()
    {
        health = maxHealth;
        isLive = true;
        coll = GetComponent<Collider2D>();
    }

    public void Init(HealthObjectType objectType, float maxHealth, float knockBackResistance)
    {
        this.maxHealth = maxHealth;
        this.health = maxHealth;
        this.healthObjectType = objectType;
        this.knockBackResistance = knockBackResistance;
        this.isLive = true;
        coll.enabled = true;
        if (rigid != null)
            rigid.simulated = true;
    }

    public void InitBoss(BaseBoss baseBoss)
    {
        this.baseBoss = baseBoss;
    }


    public void UpdateObjectType(HealthObjectType objectType)
    {
        this.healthObjectType = objectType;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Bullet"))
            return;

        health -= collision.GetComponent<Bullet>().damage;

        if (health > 0) // 모든 오브젝트에 동일하게 일어나는 히트 효과만
        {
            //anim.SetTrigger("Hit");
            float knockbackDistance = collision.GetComponent<Bullet>().knockback - knockBackResistance;
            switch (this.healthObjectType)
            {
                case HealthObjectType.ENEMY:
                    StartCoroutine(CoKnockBack(knockbackDistance));
                    GetComponent<Enemy>().Hit();
                    break;
                case HealthObjectType.BOSS:
                    StartCoroutine(CoKnockBack(knockbackDistance));
                    baseBoss.Hit();
                    break;
                case HealthObjectType.PROJECTILE:
                    break;
            }
            AudioManager.instance.PlaySfx(AudioManager.Sfx.Hit);
        }
        else // 모든 오브젝트에 동일하게 일어나는 사망 효과만
        {
            isLive = false;
            coll.enabled = false;
            if(rigid!=null)
                rigid.simulated = false;
            //anim.SetBool("Dead", true);

            if (!GameManager.instance.isLive)
                return;

            switch (this.healthObjectType)
            {
                case HealthObjectType.BOSS:
                    baseBoss.Dead();
                    break;
                case HealthObjectType.ENEMY:
                    GetComponent<Enemy>().Dead();
                    break;
                case HealthObjectType.PROJECTILE:
                    GameManager.instance.pool.Release(gameObject);
                    break;
            }
            AudioManager.instance.PlaySfx(AudioManager.Sfx.Dead);
        }
    }
    IEnumerator CoKnockBack(float knockBack)
    {
        yield return wait;
        Vector3 playerPos = GameManager.instance.playerPos;
        Vector3 dirVec = transform.position - playerPos;
        rigid.AddForce(dirVec.normalized * knockBack, ForceMode2D.Impulse);
    }
}
