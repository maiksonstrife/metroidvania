using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordCharacterMoveState : SwordCharacterGroundedState
{
    public SwordCharacterMoveState(SwordCharacter swordCharacter, SwordCharaterStateMachine statemachine, SwordCharacterData swordCharacterData, string _animBoolName) 
        : base(swordCharacter, statemachine, swordCharacterData, _animBoolName)
    {
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        
        SwordCharacter.CheckIfShouldFlip(XInput);
        SwordCharacter.SetVelocityX(SwordCharacterData.MovementVelocity * XInput);
        if (XInput == 0 && !isExitingState) SwordCharaterStateMachine.ChangeState(SwordCharacter.IdleState);
    }
}
