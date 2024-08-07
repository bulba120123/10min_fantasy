using EnumTypes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack1State : BaseState
{
    public Attack1State(BaseBoss boss) : base(boss) { }

    public override void OnStateEnter()
    {
    }

    public override void OnStateUpdate()
    {
    }

    public override void OnStateExit()
    {
    }
}

public class DragonFireState : BaseState
{
    public DragonFireState(BaseBoss boss) : base(boss) { }

    public override void OnStateEnter()
    {
        boss.skillObjects[BossSkillType.DRAGON_FIRE].GetComponent<BossSkill>().SkillAction();
    }

    public override void OnStateUpdate()
    {
    }

    public override void OnStateExit()
    {
    }

}

public class SwingSwordState : BaseState
{
    public SwingSwordState(BaseBoss boss) : base(boss) { }

    public override void OnStateEnter()
    {
        boss.anim.SetTrigger("swingSword");
    }

    public override void OnStateUpdate()
    {
    }

    public override void OnStateExit()
    {
    }

}