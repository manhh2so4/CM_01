using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiniteStateMachine
{
    public State currentStage {get ; private set;}
    public void Initilize(State startingState){
        currentStage = startingState;
        currentStage.Enter();
    }
    public void changeStage(State newState){
        currentStage.Exit();
        currentStage = newState;
        currentStage.Enter();
    }
}
