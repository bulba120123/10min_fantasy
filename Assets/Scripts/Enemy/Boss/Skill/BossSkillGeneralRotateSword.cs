using EnumTypes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Carmone
{
    public class BossSkillGeneralRotateSword : BossSkill
    {
        private void OnEnable()
        {
            StartCoroutine(CoRotateSword());
        }
        private void OnDisable()
        {
            StopCoroutine(CoRotateSword());
        }

        private void Update()
        {
            if (!GameManager.instance.isLive)
                return;
            transform.Rotate(Vector3.back * 150 * Time.deltaTime);
        }

        public IEnumerator CoRotateSword()
        {
            while (true)
            {
                BatchRotateSword();
                yield return new WaitForSeconds(baseProjectileCoolTime);
            }
        }

        private void BatchRotateSword()
        {
            Vector3 targetPso = player.transform.position;
            Vector3 dir = (targetPso - transform.position).normalized;
            dir = dir.normalized;
            for (int index = 0; index < baseProjectileCount; index++)
            {
                Transform sword;

                if (index < transform.childCount)
                {
                    sword = transform.GetChild(index);
                    if (sword.gameObject.activeSelf == false)
                    {
                        sword.gameObject.SetActive(true);
                    }
                }
                else
                {
                    sword = GameManager.instance.pool.Get(bossSkillType.ToString()).transform;
                    sword.parent = transform;
                }
                sword.localPosition = Vector3.zero;
                Vector3 rotVec = Vector3.forward * 360 * index / baseProjectileCount;
                sword.Rotate(rotVec);
                sword.Translate(sword.up * 1.5f, Space.World);

                sword.GetComponent<BossProjectile>().RotateSwordInit(baseDamage, baseProjectileSpeed, isProjectilePenetrating ? -100 : 1, maxHealth, dir, bossSkillType);
            }
        }
    }
}
