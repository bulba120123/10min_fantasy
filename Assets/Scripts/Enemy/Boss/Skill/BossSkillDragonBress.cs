using EnumTypes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Carmone
{
    public class BossSkillDragonBress : BossSkill
    {
        public override void SkillAction()
        {
            StartCoroutine(CoDragonBress());
        }
        public IEnumerator CoDragonBress()
        {
            boss.isMove = false;
            yield return new WaitForSeconds(0.5f);
            // int baseProjectileCount = 6;
            Vector3 targetPso = player.transform.position;

            // AudioManager.instance.PlaySfx(AudioManager.Sfx.Range);

            float allDegree = 80f;
            float degree = allDegree / baseProjectileCount;

            for (int i = 0; i < baseProjectileCount; i++)
            {
                StartCoroutine(CoDragonBressLine(i, allDegree, degree, baseProjectileCount, targetPso));
                yield return new WaitForSeconds(0.3f);
            }
            boss.isMove = true;
        }

        public IEnumerator CoDragonBressLine(int sequence, float allDegree, float degree, int baseProjectileCount, Vector3 targetPso)
        {
            for (int i = 0; i < 5; i++)
            {
                Vector3 direction = Quaternion.Euler(0, 0, (sequence * degree) - (allDegree / 2)) * (targetPso - transform.position).normalized;
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

                Transform dragonBressProjectile = GameManager.instance.pool.Get(BossSkillType.DRAGON_BRESS.ToString()).transform;
                dragonBressProjectile.position = transform.position;
                dragonBressProjectile.rotation = Quaternion.Euler(0, 0, angle + 30f);

                dragonBressProjectile.GetComponent<BossProjectile>().DragonBressInit(baseDamage, baseProjectileSpeed, isProjectilePenetrating ? -100 : 1, 10.0f, direction, bossSkillType);

                yield return new WaitForSeconds(0.2f);
            }

        }

    }
}
