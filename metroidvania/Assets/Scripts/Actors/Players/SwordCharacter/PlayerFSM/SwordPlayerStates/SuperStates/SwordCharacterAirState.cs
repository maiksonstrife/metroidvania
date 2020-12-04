using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordCharacterAirState : SwordCharaterState
{
    private int _xInput;
    private bool _grabInput;
    private bool _isGrounded;
    private bool _isTouchingWall;
    private bool _isTouchingGrabbable;
    private bool _jumpInput;
    private bool _coyoteTime;
    private bool _isJumping;
    private bool _jumpInputStop;

    public SwordCharacterAirState(SwordCharacter swordCharacter, SwordCharaterStateMachine statemachine, SwordCharacterData swordCharacterData, string _animBoolName) : base(swordCharacter, statemachine, swordCharacterData, _animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
        _isGrounded = SwordCharacter.CheckIfTouchingGround();
        _isTouchingWall = SwordCharacter.CheckIfTouchingWall();
        _isTouchingGrabbable = SwordCharacter.CheckIfIsGrabbable();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        CheckCoyoteTime();
        _xInput = SwordCharacter.InputHandler.InputX;
        _jumpInput = SwordCharacter.InputHandler.JumpInput;
        _jumpInputStop = SwordCharacter.InputHandler.JumpInputStop;
        _grabInput = SwordCharacter.InputHandler.GrabInput;

        CheckJumpMultiplier();

        if (_isGrounded && SwordCharacter.CurrentVelocity.y < 0.01f)
        {
            SwordCharaterStateMachine.ChangeState(SwordCharacter.LandState);
        }
        else if (SwordCharacterData.canDoubleJump && _jumpInput && SwordCharacter.JumpState.CanJump())
        {
            SwordCharacter.Anim.SetTrigger("isDoubleJumpTrigger");
            SwordCharacter.InputHandler.JumpButtonUsed();
            SwordCharaterStateMachine.ChangeState(SwordCharacter.JumpState);
        }
        else if (_isTouchingGrabbable && _grabInput)
        {
            SwordCharaterStateMachine.ChangeState(SwordCharacter.WallGrabState);
        }
        else if ((_isTouchingWall || _isTouchingGrabbable) && _xInput == SwordCharacter.FacingDirection && SwordCharacter.CurrentVelocity.y <= 0 && SwordCharacterData.canSlide)
        {
            SwordCharaterStateMachine.ChangeState(SwordCharacter.WallSlideState);
        }
        else
        {
            SwordCharacter.CheckIfShouldFlip(_xInput);
            SwordCharacter.SetVelocityX(SwordCharacterData.MovementVelocity * _xInput);
            SwordCharacter.Anim.SetFloat("yVelocity", SwordCharacter.CurrentVelocity.y);
            SwordCharacter.Anim.SetFloat("xVelocity", Mathf.Abs(SwordCharacter.CurrentVelocity.x));
        }
    }

    private void CheckJumpMultiplier()
    {
        if (_isJumping)
        {
            if (_jumpInputStop)
            {
                SwordCharacter.SetVelocityY(SwordCharacter.CurrentVelocity.y * SwordCharacterData.JumpHeightMultiplier);
                _isJumping = false;
            }
            else if (SwordCharacter.CurrentVelocity.y <= 0f)
            {
                _isJumping = false;
            }
        }
    }

    private void CheckCoyoteTime()
    {
        if (_coyoteTime && Time.time > startTime + SwordCharacterData.CoyoteTime)
        {
            _coyoteTime = false;
            SwordCharacter.JumpState.DecreaseAmountOfJumpsLeft();
        }
    }

    public void StartCoyoteTime() => _coyoteTime = true;
    public void SetIsJumping() => _isJumping = true;
}
