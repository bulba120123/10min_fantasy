using EnumTypes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Carmone
{
    public class BossSkillDragonFire : BossSkill
    {

        public override void SkillAction()
        {
            // int count = 8;
            Vector3 targetPso = player.transform.position;

            // AudioManager.instance.PlaySfx(AudioManager.Sfx.Range);

            float allDegree = 360;
            float degree = allDegree / baseProjectileCount;

            for (int i = 0; i < baseProjectileCount; i++)
            {
                Vector3 direction = Quaternion.Euler(0, 0, (i * degree) - (allDegree / 2)) * (targetPso - transform.position).normalized;
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

                Transform dragonFireProjectile = GameManager.instance.pool.Get(BossSkillType.DRAGON_FIRE.ToString()).transform;
                dragonFireProjectile.position = transform.position;
                dragonFireProjectile.rotation = Quaternion.Euler(0, 0, angle + 90);

                dragonFireProjectile.GetComponent<BossProjectile>().ProjectileInit(baseDamage, baseProjectileSpeed, isProjectilePenetrating ? -100 : 1, direction, bossSkillType);
            }
        }
    }
}
