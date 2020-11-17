using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordCharacterGroundedState : SwordCharaterState
{
    protected int XInput;
    private bool JumpInput;
    protected bool isGrounded;
    public SwordCharacterGroundedState(SwordCharacter swordCharacter, SwordCharaterStateMachine statemachine, SwordCharacterData swordCharacterData, string _animBoolName)
    : base(swordCharacter, statemachine, swordCharacterData, _animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
        isGrounded = SwordCharacter.CheckIfTouchingGround();
    }

    public override void Enter()
    {
        base.Enter();
        SwordCharacter.JumpState.ResetAmountOfJumpsLeft();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        XInput = SwordCharacter.InputHandler.NormalizeInputX;
        JumpInput = SwordCharacter.InputHandler.JumpInput;
        if (JumpInput && SwordCharacter.JumpState.CanJump())
        {
            SwordCharacter.InputHandler.JumpButtonUsed();
            SwordCharaterStateMachine.ChangeState(SwordCharacter.JumpState);
        }
        else if (!isGrounded)
        {
            SwordCharacter.AirState.StartCoyoteTime();
            SwordCharaterStateMachine.ChangeState(SwordCharacter.AirState);
        }
    }
}
