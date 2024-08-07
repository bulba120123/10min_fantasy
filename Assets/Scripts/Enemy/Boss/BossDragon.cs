using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using EnumTypes;
using System;
using Carmone;

public class BossDragon : BaseBoss
{
    private FSM _fsm;
    public List<float> skillChangeTime = new List<float>();
    public Dictionary<BossSkillType, BossSkillData> bossSkillDatas = new Dictionary<BossSkillType, BossSkillData>();

    private void OnEnable()
    {
        _curState = 0;
        _fsm = new FSM(new IdleState(this));
    }

    public override void LateUpdateFunc()
    {
        if (isRotate)
        {
            renderTransform.rotation = Quaternion.FromToRotation(Vector3.up, -1 * playerVec);
        }
    }

    public override IEnumerator SwitchingPattern()
    {
        _curState = 0;
        int nextState = 0;
        while (true)
        {
            // yield return new WaitForSeconds(cooltime);
            switch (_curState)
            {
                case 0:
                case 1:
                case 2:
                    nextState = 1;

                    if (IsTooFar())
                    {
                        nextState = 2;
                    }
                    if (IsTooClose())
                    {
                        nextState = 3;
                    }
                    break;
                case 3:
                    nextState = 1;

                    if (IsTooFar())
                    {
                        nextState = 2;
                    }
                    break;
            }
            // Debug.Log($"Switching Pattern {nextState}");
            ChangeState(nextState);
            while (isAction)
            {
                yield return new WaitForSeconds(0.5f);
            }
            yield return new WaitForSeconds(skillChangeTime[_curState]);
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
            case 1:
                _fsm.ChangeState(new DragonFireState(this));
                break;
            case 2:
                _fsm.ChangeState(new DragonRushState(this));
                skillObjects[BossSkillType.DRAGON_RUSH].GetComponent<BossSkill>().SkillAction();
                break;
            case 3:
                _fsm.ChangeState(new DragonBressState(this));
                skillObjects[BossSkillType.DRAGON_BRESS].GetComponent<BossSkill>().SkillAction();
                break;
        }
    }

    private bool IsTooFar()
    {
        if (bossScanner.targetDistance > 15f)
            return true;
        return false;
    }

    private bool IsTooClose()
    {
        if (bossScanner.targetDistance < 8f)
            return true;
        return false;
    }

    public override void InitByBoss()
    {
        string[] tempBossDragonSkillName = { "DRAGON_RUSH", "DRAGON_FIRE", "DRAGON_BRESS" };

        foreach (string skillName in tempBossDragonSkillName)
        {
            BossSkillType skillType = (BossSkillType)Enum.Parse(typeof(BossSkillType), skillName);
            InitBossSkill(skillObjects[skillType], bossSkillDatas[skillType]);
        }
        StartCoroutine(SwitchingPattern());
    }


    public void InitBossSkill(GameObject skillObj, BossSkillData bossSkillData)
    {
        BossSkill bossSkill;
        bossSkill = skillObj.GetComponent<BossSkill>();

        if (bossSkillData is BossProjectileSkillData skilData)
        {
            bossSkill.Init(this, skilData);
        }
        else
        {
            bossSkill.Init(this, bossSkillData);
        }
    }
}
