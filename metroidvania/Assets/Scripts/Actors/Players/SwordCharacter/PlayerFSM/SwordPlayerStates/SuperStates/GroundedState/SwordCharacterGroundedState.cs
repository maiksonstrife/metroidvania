using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class SwordCharacterGroundedState : SwordCharaterState
{
    protected int XInput;
    protected int YInput;
    private bool _jumpInput;
    private bool _grabInput;
    private bool _dashInput;
    private bool _isGrounded;
    private bool _isGrabbable;
    private bool _isTouchingWallAbove;
    public SwordCharacterGroundedState(SPlayer swordCharacter, SPlayerStateMachine statemachine, SPlayerData swordCharacterData, string _animBoolName)
    : base(swordCharacter, statemachine, swordCharacterData, _animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
        _isGrounded = SwordCharacter.CheckIfTouchingGround();
        _isGrabbable = SwordCharacter.CheckIfIsGrabbable();
        _isTouchingWallAbove = SwordCharacter.CheckIfTouchingWallAbove();
    }

    public override void Enter()
    {
        base.Enter();
        SwordCharacter.JumpState.ResetAmountOfJumpsLeft();
        SwordCharacter.AirDashState.ResetCanDash();
        SwordCharacter.DashState.ResetCanDash();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        XInput = SwordCharacter.InputHandler.InputX;
        YInput = SwordCharacter.InputHandler.InputY;
        _jumpInput = SwordCharacter.InputHandler.JumpInput;
        _grabInput = SwordCharacter.InputHandler.GrabInput;
        _dashInput = SwordCharacter.InputHandler.DashInput;

        if (_jumpInput && SwordCharacter.JumpState.CanJump())
        {
            SwordCharaterStateMachine.ChangeState(SwordCharacter.JumpState);
        }
        else if (_isGrabbable && _grabInput && YInput >= 0 && _isTouchingWallAbove)
        {
            SwordCharaterStateMachine.ChangeState(SwordCharacter.WallGrabState);
        }
        else if (_dashInput && SwordCharacter.DashState.CheckIfCanDash())
        {
            SwordCharaterStateMachine.ChangeState(SwordCharacter.DashState);
        }
        else if (!_isGrounded)
        {

            SwordCharacter.AirState.StartCoyoteTime();
            SwordCharaterStateMachine.ChangeState(SwordCharacter.AirState);
        }
    }
}
