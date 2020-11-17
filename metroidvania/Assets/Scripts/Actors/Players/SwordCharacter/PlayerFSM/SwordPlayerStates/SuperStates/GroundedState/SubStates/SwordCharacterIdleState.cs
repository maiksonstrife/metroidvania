using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class SwordCharacterIdleState : SwordCharacterGroundedState
{
    public SwordCharacterIdleState(SwordCharacter swordCharacter, SwordCharaterStateMachine statemachine, SwordCharacterData swordCharacterData, string _animBoolName)
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
        if (XInput != 0) SwordCharaterStateMachine.ChangeState(SwordCharacter.MoveState);
    }
}
