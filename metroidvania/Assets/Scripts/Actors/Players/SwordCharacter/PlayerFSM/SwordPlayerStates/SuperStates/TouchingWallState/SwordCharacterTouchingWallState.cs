using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordCharacterTouchingWallState : SwordCharaterState
{
    protected bool IsGrounded;
    protected bool IsTouchingWall;
    protected bool IsTouchingWallAbove;
    protected bool IsTouchingGrabbable;
    protected int XInput;
    protected int YInput;
    protected bool GrabInput;
    protected bool JumpInput;

    public SwordCharacterTouchingWallState(SwordCharacter swordCharacter, SwordCharaterStateMachine statemachine, SwordCharacterData swordCharacterData, string animBoolName) : base(swordCharacter, statemachine, swordCharacterData, animBoolName)
    {
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();
    }

    public override void DoChecks()
    {
        base.DoChecks();
        IsGrounded = SwordCharacter.CheckIfTouchingGround();
        IsTouchingWall = SwordCharacter.CheckIfTouchingWall();
        IsTouchingWallAbove = SwordCharacter.CheckIfTouchingWallAbove();
        IsTouchingGrabbable = SwordCharacter.CheckIfIsGrabbable();
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        XInput = SwordCharacter.InputHandler.InputX;
        YInput = SwordCharacter.InputHandler.InputY;
        GrabInput = SwordCharacter.InputHandler.GrabInput;
        JumpInput = SwordCharacter.InputHandler.JumpInput;

        if (IsGrounded && !GrabInput)
        {
            SwordCharaterStateMachine.ChangeState(SwordCharacter.IdleState);
        }
        else if (!IsTouchingWall && !IsTouchingGrabbable || XInput != SwordCharacter.FacingDirection && !GrabInput)
        {
            SwordCharaterStateMachine.ChangeState(SwordCharacter.AirState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
