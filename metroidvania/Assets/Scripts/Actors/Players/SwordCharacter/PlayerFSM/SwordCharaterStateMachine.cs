using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordCharaterStateMachine
{
    public SwordCharaterState CurrentState { get; private set; } 

    public void Initialize(SwordCharaterState startingState)
    {
        CurrentState = startingState;
        CurrentState.Enter();
    }

    public void ChangeState(SwordCharaterState newState)
    {
        CurrentState.Exit();
        CurrentState = newState;
        CurrentState.Enter();
    }
}
