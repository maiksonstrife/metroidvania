﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SPlayerWallSlideState : SwordCharacterTouchingWallState
{
    public SPlayerWallSlideState(SPlayer swordCharacter, SPlayerStateMachine statemachine, SPlayerData swordCharacterData, string animBoolName) : base(swordCharacter, statemachine, swordCharacterData, animBoolName)
    {
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (isExitingState) return;

        SwordCharacter.SetVelocityY(-SwordCharacterData.WallSlideVelocity);
        if (GrabInput && IsTouchingGrabbable) SwordCharaterStateMachine.ChangeState(SwordCharacter.WallGrabState);
    }
}
