﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordCharaterState
{
    protected SPlayer SwordCharacter;
    protected SPlayerStateMachine SwordCharaterStateMachine;
    protected SPlayerData SwordCharacterData;
    protected bool isAnimationFinished;
    protected bool isExitingState;
    protected float startTime;
    private string _animBoolName;

    public SwordCharaterState(SPlayer swordCharacter, SPlayerStateMachine statemachine, 
        SPlayerData swordCharacterData, string animBoolName)
    {
        SwordCharacter = swordCharacter;
        SwordCharaterStateMachine = statemachine;
        SwordCharacterData = swordCharacterData;
        _animBoolName = animBoolName;
    }

    public virtual void Enter()
    {
        DoChecks();
        SwordCharacter.Anim.SetBool(_animBoolName, true);
        startTime = Time.time;
        isAnimationFinished = false;
        isExitingState = false;
    }

    public virtual void Exit()
    {
        SwordCharacter.Anim.SetBool(_animBoolName, false);
        isExitingState = true;
    }

    public virtual void LogicUpdate() { }

    public virtual void PhysicsUpdate() => DoChecks();

    public virtual void DoChecks() { }
    public virtual void AnimationTrigger() { }
    public virtual void AnimationFinishTrigger() => isAnimationFinished = true;
}
