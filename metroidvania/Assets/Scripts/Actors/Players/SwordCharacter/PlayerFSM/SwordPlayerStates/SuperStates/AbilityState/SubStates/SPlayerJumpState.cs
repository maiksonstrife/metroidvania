﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SPlayerJumpState : SwordCharacterAbilityState
{
    private int _amountsOfJumpsLeft;
    public SPlayerJumpState(SPlayer swordCharacter, SPlayerStateMachine statemachine, SPlayerData swordCharacterData, string _animBoolName) : base(swordCharacter, statemachine, swordCharacterData, _animBoolName)
    {
        _amountsOfJumpsLeft = swordCharacterData.AmountOfJumps;
    }

    public override void Enter()
    {
        base.Enter();
        SwordCharacter.InputHandler.JumpButtonUsed();
        SwordCharacter.SetVelocityY(SwordCharacterData.JumpVelocity);
        IsAbilityDone = true;
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
