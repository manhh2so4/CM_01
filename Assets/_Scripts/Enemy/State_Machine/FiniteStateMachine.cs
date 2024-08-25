using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiniteStateMachine
{
    public EnemyState currentStage {get ; private set;}
    public void Initilize(EnemyState startingState){
        currentStage = startingState;
        currentStage.Enter();
    }
    public void changeStage(EnemyState newState){
        currentStage.Exit();
        currentStage = newState;
        currentStage.Enter();
    }
}
