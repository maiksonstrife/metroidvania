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
    private bool _isGrounded;
    private bool _isGrabbable;
    public SwordCharacterGroundedState(SwordCharacter swordCharacter, SwordCharaterStateMachine statemachine, SwordCharacterData swordCharacterData, string _animBoolName)
    : base(swordCharacter, statemachine, swordCharacterData, _animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
        _isGrounded = SwordCharacter.CheckIfTouchingGround();
        _isGrabbable = SwordCharacter.CheckIfIsGrabbable();
    }

    public override void Enter()
    {
        base.Enter();
        SwordCharacter.JumpState.ResetAmountOfJumpsLeft();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        XInput = SwordCharacter.InputHandler.InputX;
        YInput = SwordCharacter.InputHandler.InputY;
        _jumpInput = SwordCharacter.InputHandler.JumpInput;
        _grabInput = SwordCharacter.InputHandler.GrabInput;

        if (_jumpInput && SwordCharacter.JumpState.CanJump())
        {
            SwordCharacter.InputHandler.JumpButtonUsed();
            SwordCharaterStateMachine.ChangeState(SwordCharacter.JumpState);
        }
        else if (_isGrabbable && _grabInput && YInput >= 0)
        {
            SwordCharaterStateMachine.ChangeState(SwordCharacter.WallGrabState);
        }
        else if (!_isGrounded)
        {

            SwordCharacter.AirState.StartCoyoteTime();
            SwordCharaterStateMachine.ChangeState(SwordCharacter.AirState);
        }

    }
}
