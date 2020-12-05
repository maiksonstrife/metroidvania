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

        if (YInput == 0 && IsTouchingGrabbable && GrabInput || IsTouchingWallAbove && YInput > 0) SwordCharaterStateMachine.ChangeState(SwordCharacter.WallGrabState);
        
        if (YInput > 0 && IsTouchingGrabbable && GrabInput) SwordCharacter.SetVelocityY(SwordCharacterData.WallClimbVelocity);
        if(YInput < 0 && IsTouchingGrabbable && GrabInput) SwordCharacter.SetVelocityY(-SwordCharacterData.WallClimbVelocity);

        //If player hold down while grabbing, it should stands idle
        if (YInput < 0 && IsTouchingGrabbable && GrabInput && IsGrounded)
            SwordCharaterStateMachine.ChangeState(SwordCharacter.IdleState);

        //Smooth transition between climb and slide
        if ((IsTouchingGrabbable || IsTouchingWall) && !GrabInput && XInput == SwordCharacter.FacingDirection)
            SwordCharaterStateMachine.ChangeState(SwordCharacter.WallSlideState);
    }
}
