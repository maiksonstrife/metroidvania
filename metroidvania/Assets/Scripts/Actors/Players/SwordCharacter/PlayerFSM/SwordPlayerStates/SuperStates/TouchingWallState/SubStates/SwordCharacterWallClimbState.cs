using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordCharacterWallClimbState : SwordCharacterTouchingWallState
{
    public SwordCharacterWallClimbState(SwordCharacter swordCharacter, SwordCharaterStateMachine statemachine, SwordCharacterData swordCharacterData, string animBoolName) : base(swordCharacter, statemachine, swordCharacterData, animBoolName)
    {
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if(YInput > 0) SwordCharacter.SetVelocityY(SwordCharacterData.WallClimbVelocity);
        if(YInput < 0) SwordCharacter.SetVelocityY(-SwordCharacterData.WallClimbVelocity);
        if(YInput == 0) SwordCharaterStateMachine.ChangeState(SwordCharacter.WallGrabState);
    }
}
