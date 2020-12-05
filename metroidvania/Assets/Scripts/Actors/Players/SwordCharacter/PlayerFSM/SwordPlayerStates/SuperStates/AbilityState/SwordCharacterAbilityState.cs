using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordCharacterAbilityState : SwordCharaterState
{
    protected bool IsAbilityDone;
    protected bool IsGrounded;
    protected bool IsTouchingWall;
    public SwordCharacterAbilityState(SwordCharacter swordCharacter, SwordCharaterStateMachine statemachine, SwordCharacterData swordCharacterData, string _animBoolName) : base(swordCharacter, statemachine, swordCharacterData, _animBoolName)
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
        if (IsAbilityDone)
        {
            if (IsGrounded && SwordCharacter.CurrentVelocity.y < 0.01f)
            {
                SwordCharaterStateMachine.ChangeState(SwordCharacter.IdleState);
            }
            else
            {
                SwordCharaterStateMachine.ChangeState(SwordCharacter.AirState);
            }
        }
    }
}
