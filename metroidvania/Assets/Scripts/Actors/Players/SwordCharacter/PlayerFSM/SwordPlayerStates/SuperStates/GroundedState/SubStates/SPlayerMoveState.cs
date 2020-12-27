using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SPlayerMoveState : SwordCharacterGroundedState
{
    public SPlayerMoveState(SPlayer swordCharacter, SPlayerStateMachine statemachine, SPlayerData swordCharacterData, string _animBoolName) 
        : base(swordCharacter, statemachine, swordCharacterData, _animBoolName)
    {
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        
        SwordCharacter.CheckIfShouldFlip(XInput);
        SwordCharacter.SetVelocityX(SwordCharacterData.MovementVelocity * XInput);
        if (isExitingState) return;
        if (XInput == 0) SwordCharaterStateMachine.ChangeState(SwordCharacter.IdleState);
    }
}
