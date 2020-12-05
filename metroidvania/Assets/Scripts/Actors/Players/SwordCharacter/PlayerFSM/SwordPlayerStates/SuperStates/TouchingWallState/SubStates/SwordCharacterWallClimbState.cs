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
        if (isExitingState) return;
        
        if (YInput > 0 && IsTouchingGrabbable && GrabInput) SwordCharacter.SetVelocityY(SwordCharacterData.WallClimbVelocity);
        if(YInput < 0 && IsTouchingGrabbable && GrabInput) SwordCharacter.SetVelocityY(-SwordCharacterData.WallClimbVelocity);

        if (YInput == 0 && IsTouchingGrabbable && GrabInput || IsTouchingWallAbove && YInput > 0) SwordCharaterStateMachine.ChangeState(SwordCharacter.WallGrabState);
        //If player hold down while grabbing, it should stands idle
        else if (YInput < 0 && IsTouchingGrabbable && GrabInput && IsGrounded)
            SwordCharaterStateMachine.ChangeState(SwordCharacter.IdleState);
        //Smooth transition between climb and slide
        else if ((IsTouchingGrabbable || IsTouchingWall) && !GrabInput && XInput == SwordCharacter.FacingDirection)
            SwordCharaterStateMachine.ChangeState(SwordCharacter.WallSlideState);
        //If none of it is true just release from wall
        else if (!IsTouchingGrabbable) SwordCharaterStateMachine.ChangeState(SwordCharacter.AirState);
        
    }
}
