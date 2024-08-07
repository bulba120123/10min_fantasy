using System;
using System.Collections;
using System.Collections.Generic;
using EnumTypes;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
    private EnemyProjectileType enemyProjectileType;
    private float damage;
    private int count;
    private float coolTime;
    private float speed;

    float timer;
    Enemy enemy;
    Player player;
    float delay;

    private void Awake()
    {
        player = GameManager.instance.player;
        delay = 1f;
    }

    private void Start()
    {

    }

    private void OnEnable()
    {
        timer = 0;
    }

    void Update()
    {
        if (!GameManager.instance.isLive)
            return;

        switch (enemyProjectileType)
        {
            default:
                timer += Time.deltaTime;
                if (delay < 0) delay -= Time.deltaTime;

                if (timer > coolTime + delay)
                {
                    timer = 0f;
                    Fire();
                }
                break;
        }
    }

    public void Init(Enemy enemy, EnemyProjectileData data)
    {
        this.enemy = enemy;

        // Basic Set
        name = "Enemy Projectile " + data.enemyProjectileType.ToString();
        transform.parent = enemy.transform;
        transform.localPosition = Vector3.zero;


        // Property Set
        enemyProjectileType = data.enemyProjectileType;
        damage = data.baseDamage;
        count = data.baseCount;
        coolTime = data.coolTime;
        speed = data.speed;
    }

    void Fire()
    {
        if (!player || !GameManager.instance.isLive)
            return;

        Vector3 targetPso = player.transform.position;
        Vector3 dir = (targetPso - transform.position).normalized;
        dir = dir.normalized;

        switch (enemy.enemyType)
        {
            case EnemyType.UNDEAD_MAGE:
                StartCoroutine(StopEnemy());
                break;
            default:
                // AudioManager.instance.PlaySfx(AudioManager.Sfx.Range);
                break;
        }

        Transform enemyBullet = GameManager.instance.pool.Get(enemyProjectileType.ToString()).transform;
        enemyBullet.position = transform.position;
        enemyBullet.rotation = Quaternion.FromToRotation(Vector3.up, dir);
        enemyBullet.GetComponent<EnemyProjectile>().Init(damage, speed, count, dir);
    }

    public IEnumerator StopEnemy()
    {
        var prevSpeed = enemy.speed;
        enemy.speed = 0;
        yield return new WaitForSeconds(2.1f);
        enemy.speed = prevSpeed;
        yield break;
    }

}
