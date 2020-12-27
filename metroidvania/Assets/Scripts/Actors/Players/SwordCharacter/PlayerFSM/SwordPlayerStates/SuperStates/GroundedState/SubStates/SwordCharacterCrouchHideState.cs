using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordCharacterCrouchHideState : SwordCharacterGroundedState
{
    public SwordCharacterCrouchHideState(SwordCharacter swordCharacter, SwordCharaterStateMachine statemachine, SwordCharacterData swordCharacterData, string _animBoolName) : base(swordCharacter, statemachine, swordCharacterData, _animBoolName)
    {
    }

    //Entando no State
    public override void Enter()
    {
        base.Enter(); //isAnimationFinished = false; //startTime
    }

    //Update do State
    public override void LogicUpdate()
    {
        base.LogicUpdate(); //isExitingState (Se superstate está executando Exit)
    }

    //Faz checagens bools antes de PhysicsUpdate
    public override void DoChecks()
    {
        base.DoChecks();
    }

    //FixedUpdate do State
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    //Saindo do State
    public override void Exit()
    {
        base.Exit();
    }

    //Evento de Animação (Apontar Animation Event a esse método)
    public override void AnimationTrigger()
    {
        base.AnimationTrigger();
    }

    //Evento de Animação que seta isAnimationFinished (Apontar Animation Event a esse método)
    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger(); //isAnimationFinished = true;
    }
}
