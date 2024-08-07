using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Linq;
using EnumTypes;
using System;

public class BossUndeadKing : BaseBoss
{
    private FSM _fsm;
    public List<float> skillCoolTime = new List<float>();
    public string curAnimName = "boss3_idle";
    public List<string> allAnimName = new List<string>();
    public Dictionary<BossSkillType, BossSkillData> bossSkillDatas = new Dictionary<BossSkillType, BossSkillData>();

    private void Start()
    {
        _curState = 0;
        _fsm = new FSM(new IdleState(this));
        allAnimName = allAnimName.Count == 0 ? new List<string> { "Boss3 idle", "Boss3 sword", "Boss3 rush" } : allAnimName;
    }

    public override void FixedUpdateFunc()
    {
        transform.position = target.position + new Vector2(0, 3f);
    }


    public override void LateUpdateFunc()
    {
    }

    public override IEnumerator SwitchingPattern()
    {
        _curState = 0;
        int nextState = 0;
        while (true)
        {
            yield return new WaitForSeconds(cooltime);
            switch (_curState)
            {
                case 0:
                    nextState = UnityEngine.Random.Range(1, 3);
                    break;
                case 1:
                case 2:
                    nextState = 0;
                    break;
            }
            Debug.Log($"Switching Pattern {nextState}");
            ChangeState(nextState);
            while (isAction)
            {
                yield return new WaitForSeconds(0.5f);
            }
            yield return new WaitForSeconds(skillCoolTime[_curState]);
        }
    }

    private void ChangeState(int nextState)
    {

        _curState = nextState;
        switch (_curState)
        {
            case 0:
                curAnimName = allAnimName[0];
                _fsm.ChangeState(new IdleState(this));
                break;
            case 1:
                curAnimName = allAnimName[1];
                _fsm.ChangeState(new SwingSwordState(this));
                StartCoroutine(CoAnim());
                break;
            case 2:
                curAnimName = allAnimName[2];
                _fsm.ChangeState(new UndeadKingRushState(this));
                StartCoroutine(CoAnim());
                break;
        }
    }

    public IEnumerator CoAnim()
    {
        isMove = false;
        isAction = true;
        AnimationClip[] clips = anim.runtimeAnimatorController.animationClips;
        float curAnimLength = clips.Where((clip) => clip.name == curAnimName).ToList()[0].length;
        Debug.Log(curAnimLength);

        yield return new WaitForSeconds(curAnimLength);
        isMove = true;
        isAction = false;
    }

    //private bool CanSeePlayer()
    //{
    //    // TODO:: 플레이어 탐지 구현
    //}

    //private bool CanAttackPlayer()
    //{
    //    // TODO:: 사정거리 체크 구현
    //}

    public override void InitByBoss()
    {
        string[] tempBossKingSkillName = { "KING_SWING_SWORD", "KING_RUSH" };

        foreach (var skillName in tempBossKingSkillName)
        {
            BossSkillType skillType = (BossSkillType)Enum.Parse(typeof(BossSkillType), skillName);
            InitBossSkill(skillObjects[skillType], bossSkillDatas[skillType]);
        }
        StartCoroutine(SwitchingPattern());
    }

    public void InitBossSkill(GameObject skillObj, BossSkillData bossSkillData)
    {
        BossSkill bossSkill = skillObj.GetComponent<BossSkill>();
        bossSkill.Init(this, bossSkillData);
    }
}
