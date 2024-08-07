using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using EnumTypes;
using System;
using Carmone;

public class BossUndeadGeneral : BaseBoss
{
    private FSM _fsm;
    public Dictionary<BossSkillType, BossCreateSkillData> bossSkillDatas = new Dictionary<BossSkillType, BossCreateSkillData>();

    private void Start()
    {
        _curState = 0;
        _fsm = new FSM(new IdleState(this));
    }


    public override void LateUpdateFunc()
    {
        foreach (var spriter in spriters)
        {
            spriter.flipX = target.position.x < transform.position.x;
        }
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
        string tempBossGeneralSkillName = "GENERAL_ROTATE_SWORD";
        BossSkillType skillType = (BossSkillType)Enum.Parse(typeof(BossSkillType), tempBossGeneralSkillName);
        InitBossSkill(skillObjects[skillType], bossSkillDatas[skillType]);
    }

    public void InitBossSkill(GameObject skillObj, BossCreateSkillData bossSkillData)
    {
        BossSkill bossSkill = skillObj.GetComponent<BossSkill>();
        bossSkill.Init(this, bossSkillData);
    }
}
