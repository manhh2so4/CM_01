using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiniteStateMachine
{
    public State CurrentState {get ; private set;}
    public bool canChange = true;
    public void Initialize(State startingState){
        CurrentState = startingState;
        CurrentState.Enter();
    }
    public void ChangeState(State newState){
        if( CurrentState == newState ) return; 
        if( canChange == false) return;
        CurrentState.Exit();
        CurrentState = newState;
        CurrentState.Enter();
    }
}
