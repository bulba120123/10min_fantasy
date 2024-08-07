using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnumTypes;

public class BossProjectile : MonoBehaviour
{
    //프리펩 친구들은 변수 초기화를 하는게 좋다
    private int per;
    private float speed;
    public float disableAfterTime;
    private Vector3 dir;
    private BossSkillType skillType;
    BossSkillType bossSkillType;
    Rigidbody2D rigid;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    public void DefaultInit(float damage, float speed, int per, Vector3 dir, BossSkillType bossSkillType)
    {
        GetComponent<DamageObject>().Init(damage);
        this.per = per;
        this.speed = speed;
        this.dir = dir;
        this.bossSkillType = bossSkillType;
    }

    public void HealthObjectInit(float damage, float speed, int per, float health, Vector3 dir, BossSkillType bossSkillType)
    {
        GetComponent<DamageObject>().Init(damage);
        GetComponent<HealthObject>().Init(HealthObjectType.PROJECTILE, health, 0);

        this.per = per;
        this.speed = speed;
        this.dir = dir;
        this.bossSkillType = bossSkillType;
    }

    public void RotateSwordInit(float damage, float speed, int per, float health, Vector3 dir, BossSkillType bossSkillType)
    {
        HealthObjectInit(damage, speed, per, health, dir, bossSkillType);

        Vector3 currentRotation = transform.rotation.eulerAngles;
        Vector3 newRotation = currentRotation + new Vector3(0, 0, 180);
        transform.rotation = Quaternion.Euler(newRotation);
    }
    public void ProjectileInit(float damage, float speed, int per, Vector3 dir, BossSkillType bossSkillType)
    {
        DefaultInit(damage, speed, per, dir, bossSkillType);

        dir.Normalize();

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle + 45));
        if (per >= 0)
        {
            rigid.velocity = dir * speed;
        }
    }
    public void DragonBressInit(float damage, float speed, int per, float disableAfterTime, Vector3 dir, BossSkillType bossSkillType)
    {
        ProjectileInit(damage, speed, per, dir, bossSkillType);

        disableAfterTime = disableAfterTime == 0 ? 10.0f : disableAfterTime;
        StartCoroutine(DisableAfterSeconds(disableAfterTime));
    }

    private IEnumerator DisableAfterSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        GameManager.instance.pool.Release(gameObject);
    }

    //관통
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (per == -100)
                return;
            per--;
            if (per < 0)
            {
                rigid.velocity = Vector2.zero;
                GameManager.instance.pool.Release(gameObject);
            }
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Area") || per == -100)
        {
            return;
        }
        gameObject.SetActive(false);
    }
}
