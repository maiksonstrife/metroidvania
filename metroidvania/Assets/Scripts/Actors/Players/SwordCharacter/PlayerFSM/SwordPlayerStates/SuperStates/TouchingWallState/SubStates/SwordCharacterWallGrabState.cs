using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordCharacterWallGrabState : SwordCharacterTouchingWallState
{
    private Vector2 _holdPosition;

    public SwordCharacterWallGrabState(SwordCharacter swordCharacter, SwordCharaterStateMachine statemachine, SwordCharacterData swordCharacterData, string animBoolName) : base(swordCharacter, statemachine, swordCharacterData, animBoolName)
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
    }

    public override void Enter()
    {
        base.Enter();
        _holdPosition = SwordCharacter.transform.position;
        HoldPosition();
        SwordCharacter.JumpState.ResetAmountOfJumpsLeft();
    }

    public override void Exit()
    {
        base.Exit();
        SwordCharacter.JumpState.DecreaseAmountOfJumpsLeft();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        HoldPosition();
        if (YInput > 0 && IsTouchingGrabbable && GrabInput && !IsTouchingWallAbove)
            SwordCharaterStateMachine.ChangeState(SwordCharacter.WallClimbState);
        if (YInput < 0 && IsTouchingGrabbable && GrabInput)
            SwordCharaterStateMachine.ChangeState(SwordCharacter.WallClimbState);
        if (!GrabInput) SwordCharaterStateMachine.ChangeState(SwordCharacter.AirState);
    }

    private void HoldPosition()
    {
        SwordCharacter.transform.position = _holdPosition;
        SwordCharacter.SetVelocityX(0f);
        SwordCharacter.SetVelocityY(0f);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
