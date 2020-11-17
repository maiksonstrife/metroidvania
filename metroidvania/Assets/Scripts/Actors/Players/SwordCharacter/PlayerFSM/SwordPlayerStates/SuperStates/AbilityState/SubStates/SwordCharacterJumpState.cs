using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordCharacterJumpState : SwordCharacterAbilityState
{
    private int _amountsOfJumpsLeft;
    public SwordCharacterJumpState(SwordCharacter swordCharacter, SwordCharaterStateMachine statemachine, SwordCharacterData swordCharacterData, string _animBoolName) : base(swordCharacter, statemachine, swordCharacterData, _animBoolName)
    {
        _amountsOfJumpsLeft = swordCharacterData.AmountOfJumps;
    }

    public override void Enter()
    {
        base.Enter();
        SwordCharacter.SetVelocityY(SwordCharacterData.JumpVelocity);
        isAbilityDone = true;
        _amountsOfJumpsLeft--;
        SwordCharacter.AirState.SetIsJumping();
    }

    public bool CanJump()
    {
        return _amountsOfJumpsLeft > 0;
    }

    public void ResetAmountOfJumpsLeft()
    {
        _amountsOfJumpsLeft = SwordCharacterData.AmountOfJumps;
    }
    public void DecreaseAmountOfJumpsLeft()
    {
        _amountsOfJumpsLeft--;
    }
}
