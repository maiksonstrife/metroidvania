using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SPlayerLandState : SwordCharacterGroundedState
{
    public SPlayerLandState(SPlayer swordCharacter, SPlayerStateMachine statemachine, SPlayerData swordCharacterData, string _animBoolName) : base(swordCharacter, statemachine, swordCharacterData, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        SwordCharacter.SetVelocityX(0f);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (isExitingState) return;

        if (XInput != 0) SwordCharaterStateMachine.ChangeState(SwordCharacter.MoveState);
        else if (isAnimationFinished) SwordCharaterStateMachine.ChangeState(SwordCharacter.IdleState);
    }
}
