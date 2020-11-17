using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordCharaterState
{
    protected SwordCharacter SwordCharacter;
    protected SwordCharaterStateMachine SwordCharaterStateMachine;
    protected SwordCharacterData SwordCharacterData;
    protected bool isAnimationFinished;
    protected float startTime;
    private string _animBoolName;

    public SwordCharaterState(SwordCharacter swordCharacter, SwordCharaterStateMachine statemachine, SwordCharacterData swordCharacterData, string animBoolName)
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
    }

    public virtual void Exit()
    {
        SwordCharacter.Anim.SetBool(_animBoolName, false);
    }

    public virtual void LogicUpdate() { }

    public virtual void PhysicsUpdate() => DoChecks();

    public virtual void DoChecks() { }
    public virtual void AnimationTrigger() { }
    public virtual void AnimationFinishTrigger() => isAnimationFinished = true;
}
