using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Carmone
{
    public class BossSkillDragonRush : BossSkill
    {
        TargetingLine targetingLine;

        private void Awake()
        {
            targetingLine = GetComponentInChildren<TargetingLine>();
        }
        public override void SkillAction()
        {
            StartCoroutine(CoDragonRush());
        }

        public IEnumerator CoDragonRush()
        {

            boss.isMove = false;
            boss.isRotate = false;
            boss.isAction = true;
            targetingLine.Init(boss);
            float rushSpeed = 10f;
            Transform bossTransform = boss.transform;

            targetingLine.DrawTargetingLine(3f);
            yield return new WaitForSeconds(3f);

            Vector3 localMoveVector = (bossTransform.position - GameManager.instance.playerPosWithZConvert) * -1;
            float distance = Vector2.Distance(Vector2.zero, localMoveVector);
            Vector3 targetPos = GameManager.instance.playerPosWithZConvert + (localMoveVector) / 10f;
            targetPos.z = -0.3f;

            bossTransform.DOLocalMove(targetPos, distance / rushSpeed);
            targetingLine.StopTargeting();

            yield return new WaitForSeconds(distance / rushSpeed);

            boss.isMove = true;
            boss.isRotate = true;
            boss.isAction = false;
            yield break;
        }
    }
}
