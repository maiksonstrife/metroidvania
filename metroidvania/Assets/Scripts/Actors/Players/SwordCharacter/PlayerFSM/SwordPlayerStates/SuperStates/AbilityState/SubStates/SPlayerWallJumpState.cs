using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SPlayerWallJumpState : SwordCharacterAbilityState
{
    private int _wallJumpDirection;
    public SPlayerWallJumpState(SPlayer swordCharacter, SPlayerStateMachine statemachine, SPlayerData swordCharacterData, string _animBoolName) : base(swordCharacter, statemachine, swordCharacterData, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        SwordCharacter.InputHandler.JumpButtonUsed();
        SwordCharacter.JumpState.ResetAmountOfJumpsLeft();
        SwordCharacter.SetVelocityAngular(SwordCharacterData.WallJumpVelocity, SwordCharacterData.WallJumpAngle, _wallJumpDirection);
        SwordCharacter.CheckIfShouldFlip(_wallJumpDirection);
        SwordCharacter.JumpState.DecreaseAmountOfJumpsLeft();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        SwordCharacter.Anim.SetFloat("yVelocity", SwordCharacter.CurrentVelocity.y);
        SwordCharacter.Anim.SetFloat("xVelocity", Mathf.Abs(SwordCharacter.CurrentVelocity.x));

        if (Time.time >= (startTime + SwordCharacterData.WallJumpTime)) IsAbilityDone = true;
    }

    public void CheckWallJumpDirection(bool isTouchingWall)
    {
        if (isTouchingWall)
        {
            _wallJumpDirection = -SwordCharacter.FacingDirection;
        }
        else
        {
            _wallJumpDirection = SwordCharacter.FacingDirection;
        }
    }
}
