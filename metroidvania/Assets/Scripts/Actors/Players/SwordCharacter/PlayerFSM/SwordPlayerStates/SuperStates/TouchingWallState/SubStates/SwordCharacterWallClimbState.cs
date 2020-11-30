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

        if(YInput > 0 && IsTouchingGrabbable && GrabInput) SwordCharacter.SetVelocityY(SwordCharacterData.WallClimbVelocity);
        if(YInput < 0 && IsTouchingGrabbable && GrabInput) SwordCharacter.SetVelocityY(-SwordCharacterData.WallClimbVelocity);

        if (!GrabInput) SwordCharaterStateMachine.ChangeState(SwordCharacter.AirState);
        if(YInput < 0 && IsTouchingGrabbable && GrabInput && IsGrounded) 
            SwordCharaterStateMachine.ChangeState(SwordCharacter.IdleState);
        if (YInput == 0 && IsTouchingGrabbable && GrabInput || !IsTouchingGrabbable && IsTouchingWall && YInput > 0 && GrabInput) 
            SwordCharaterStateMachine.ChangeState(SwordCharacter.WallGrabState);
        if (!IsTouchingGrabbable && IsTouchingWall && XInput == SwordCharacter.FacingDirection && YInput < 0)
            SwordCharaterStateMachine.ChangeState(SwordCharacter.WallSlideState);
    }
}
