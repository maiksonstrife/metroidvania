using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordCharacterWallSlideState : SwordCharacterTouchingWallState
{
    public SwordCharacterWallSlideState(SwordCharacter swordCharacter, SwordCharaterStateMachine statemachine, SwordCharacterData swordCharacterData, string animBoolName) : base(swordCharacter, statemachine, swordCharacterData, animBoolName)
    {
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        SwordCharacter.SetVelocityY(-SwordCharacterData.WallSlideVelocity);

        if (GrabInput && YInput == 0 && IsTouchingGrabbable) SwordCharaterStateMachine.ChangeState(SwordCharacter.WallGrabState);
    }
}
