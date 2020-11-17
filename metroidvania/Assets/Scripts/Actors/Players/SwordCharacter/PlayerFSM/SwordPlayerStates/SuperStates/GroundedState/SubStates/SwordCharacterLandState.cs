using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordCharacterLandState : SwordCharacterGroundedState
{
    public SwordCharacterLandState(SwordCharacter swordCharacter, SwordCharaterStateMachine statemachine, SwordCharacterData swordCharacterData, string _animBoolName) : base(swordCharacter, statemachine, swordCharacterData, _animBoolName)
    {
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (XInput != 0)
        {
            SwordCharaterStateMachine.ChangeState(SwordCharacter.MoveState);
        }
        else if (isAnimationFinished)
        {
            SwordCharaterStateMachine.ChangeState(SwordCharacter.IdleState);
        }
    }
}
