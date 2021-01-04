using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SPlayerAirState : SwordCharaterState
{
    //Input
    private int _xInput;
    private bool _grabInput;
    private bool _jumpInput;
    private bool _jumpInputStop;
    private bool _dashInput;

    //Checks
    private bool _isGrounded;
    private bool _isTouchingWall;
    private bool _isTouchingWallBack;
    private bool _isTouchingGrabbable;
    private bool _isTouchingWallAbove;
    private bool _isTouchingGrabbableAbove;
    private bool _coyoteTime;
    private bool _isJumping;

    public SPlayerAirState(SPlayer swordCharacter, SPlayerStateMachine statemachine, SPlayerData swordCharacterData, string _animBoolName) : base(swordCharacter, statemachine, swordCharacterData, _animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
        _isGrounded = SwordCharacter.CheckIfTouchingGround();
        _isTouchingWall = SwordCharacter.CheckIfTouchingWall();
        _isTouchingGrabbable = SwordCharacter.CheckIfIsGrabbable();
        _isTouchingWallBack = SwordCharacter.CheckIfTouchingWallBack();
        _isTouchingWallAbove = SwordCharacter.CheckIfTouchingWallAbove();
        _isTouchingGrabbableAbove = SwordCharacter.CheckIfTouchingGrabbableAbove();

        if ((_isTouchingWall || _isTouchingGrabbable) && !_isTouchingWallAbove && !_isTouchingGrabbableAbove)
        {
            SwordCharacter.LedgeCLimbState.SetDetectedPosition(SwordCharacter.transform.position);
        }
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        CheckCoyoteTime();
        _xInput = SwordCharacter.InputHandler.InputX;
        _jumpInput = SwordCharacter.InputHandler.JumpInput;
        _jumpInputStop = SwordCharacter.InputHandler.JumpInputStop;
        _grabInput = SwordCharacter.InputHandler.GrabInput;
        _dashInput = SwordCharacter.InputHandler.DashInput;

        CheckJumpMultiplier();

        if (_isGrounded && SwordCharacter.CurrentVelocity.y < 0.01f)
        {
            SwordCharaterStateMachine.ChangeState(SwordCharacter.LandState);
        }
        else if ((_isTouchingWall || _isTouchingGrabbable) && !_isTouchingWallAbove && !_isTouchingGrabbableAbove && !_isGrounded)
        {
            SwordCharaterStateMachine.ChangeState(SwordCharacter.LedgeCLimbState);
        }
        else if (SwordCharacterData.canWallJump && _jumpInput && _xInput == SwordCharacter.FacingDirection && (_isTouchingWall || _isTouchingGrabbable || _isTouchingWallBack))
        {
            _isTouchingWall = SwordCharacter.CheckIfTouchingWall(); //update info in Update to increase accuracy
            _isTouchingGrabbable = SwordCharacter.CheckIfIsGrabbable();
            SwordCharacter.WallJumpState.CheckWallJumpDirection(_isTouchingGrabbable|| _isTouchingWall);
            SwordCharaterStateMachine.ChangeState(SwordCharacter.WallJumpState);
        }
        else if (SwordCharacterData.canDoubleJump && _jumpInput && SwordCharacter.JumpState.CanJump())
        {
            SwordCharacter.Anim.SetTrigger("isDoubleJumpTrigger");
            SwordCharaterStateMachine.ChangeState(SwordCharacter.JumpState);
        }
        else if (_isTouchingGrabbable && _grabInput && _isTouchingGrabbableAbove)
        {
            SwordCharaterStateMachine.ChangeState(SwordCharacter.WallGrabState);
        }
        else if ((_isTouchingWall || _isTouchingGrabbable) && _xInput == SwordCharacter.FacingDirection && SwordCharacter.CurrentVelocity.y <= 0 && SwordCharacterData.canSlide)
        {
            SwordCharaterStateMachine.ChangeState(SwordCharacter.WallSlideState);
        }
        else if (_dashInput && SwordCharacter.AirDashState.CheckIfCanDash())
        {
            SwordCharaterStateMachine.ChangeState(SwordCharacter.AirDashState);
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

    public override void Exit()
    {
        base.Exit();
        _isTouchingGrabbable = false;
        _isTouchingWall = false;
        _isTouchingWallBack = false;
    }
}
