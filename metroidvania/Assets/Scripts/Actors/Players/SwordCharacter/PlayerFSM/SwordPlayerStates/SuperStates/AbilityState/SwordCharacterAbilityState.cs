using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordCharacterAbilityState : SwordCharaterState
{
    protected bool IsAbilityDone;
    protected bool IsGrounded;
    protected bool IsTouchingWall;
    protected bool _jumpInput;
    protected bool _dashInput;
    protected int XInput;

    public SwordCharacterAbilityState(SPlayer swordCharacter, SPlayerStateMachine statemachine, SPlayerData swordCharacterData, string _animBoolName) : base(swordCharacter, statemachine, swordCharacterData, _animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
        IsGrounded = SwordCharacter.CheckIfTouchingGround();
        IsTouchingWall = SwordCharacter.CheckIfTouchingWall();
    }

    public override void Enter()
    {
        base.Enter();
        IsAbilityDone = false;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        XInput = SwordCharacter.InputHandler.InputX;
        _jumpInput = SwordCharacter.InputHandler.JumpInput;
        _dashInput = SwordCharacter.InputHandler.DashInput;

        if (IsAbilityDone)
        {
            if (IsGrounded && SwordCharacter.CurrentVelocity.y < 0.01f)
            {
                SwordCharaterStateMachine.ChangeState(SwordCharacter.IdleState);
            }
            
            if (!IsGrounded || SwordCharacter.CurrentVelocity.y > 0.01f)
            {
                SwordCharaterStateMachine.ChangeState(SwordCharacter.AirState);
            }
        }
    }
}
