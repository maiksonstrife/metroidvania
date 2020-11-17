using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordCharacterAbilityState : SwordCharaterState
{
    protected bool isAbilityDone;
    protected bool _isGrounded;
    public SwordCharacterAbilityState(SwordCharacter swordCharacter, SwordCharaterStateMachine statemachine, SwordCharacterData swordCharacterData, string _animBoolName) : base(swordCharacter, statemachine, swordCharacterData, _animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
        _isGrounded = SwordCharacter.CheckIfTouchingGround();
    }

    public override void Enter()
    {
        base.Enter();
        isAbilityDone = false;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (isAbilityDone)
        {
            if (_isGrounded && SwordCharacter.CurrentVelocity.y < 0.01f)
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
