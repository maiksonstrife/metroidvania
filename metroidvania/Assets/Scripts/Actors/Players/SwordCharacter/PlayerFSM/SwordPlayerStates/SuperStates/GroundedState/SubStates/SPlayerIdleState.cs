using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class SPlayerIdleState : SwordCharacterGroundedState
{
    public SPlayerIdleState(SPlayer swordCharacter, SPlayerStateMachine statemachine, SPlayerData swordCharacterData, string _animBoolName)
    : base(swordCharacter, statemachine, swordCharacterData, _animBoolName)
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

    }
}
