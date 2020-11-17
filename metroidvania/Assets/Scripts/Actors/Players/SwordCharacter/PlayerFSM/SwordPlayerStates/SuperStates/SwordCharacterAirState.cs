using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordCharacterAirState : SwordCharaterState
{
    private int _xInput;
    private bool _isGrounded;
    private bool _jumpInput;
    private bool _coyoteTime;
    private bool _isJumping;
    private bool _isDoubleJumping;
    private bool _jumpInputStop;

    public SwordCharacterAirState(SwordCharacter swordCharacter, SwordCharaterStateMachine statemachine, SwordCharacterData swordCharacterData, string _animBoolName) : base(swordCharacter, statemachine, swordCharacterData, _animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
        _isGrounded = SwordCharacter.CheckIfTouchingGround();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        CheckCoyoteTime();
        _xInput = SwordCharacter.InputHandler.NormalizeInputX;
        _jumpInput = SwordCharacter.InputHandler.JumpInput;
        _jumpInputStop = SwordCharacter.InputHandler.JumpInputStop;

        CheckJumpMultiplier();

        if (_isGrounded && SwordCharacter.CurrentVelocity.y < 0.01f)
        {
            SwordCharaterStateMachine.ChangeState(SwordCharacter.LandState);
        }
        else if (_jumpInput && SwordCharacter.JumpState.CanJump())
        {
            SwordCharacter.Anim.SetTrigger("isDoubleJumpTrigger");
            SwordCharaterStateMachine.ChangeState(SwordCharacter.JumpState);
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
