using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Attack2State : BaseState
{
    public Attack2State(BaseBoss boss) : base(boss) { }

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

public class DragonRushState : BaseState
{
    public DragonRushState(BaseBoss boss) : base(boss) {
    }

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

public class UndeadKingRushState : BaseState
{
    public UndeadKingRushState(BaseBoss boss) : base(boss)
    {
    }

    public override void OnStateEnter()
    {
        boss.anim.SetTrigger("rush");
    }

    public override void OnStateUpdate()
    {
    }

    public override void OnStateExit()
    {
    }
}