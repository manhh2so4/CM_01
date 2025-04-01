using UnityEngine;
[System.Serializable]
public abstract class State {
    protected Core core;
    protected Movement movement;
    protected bool isExitingState;
    
    public virtual void Enter()
    {
        isExitingState = false;
    }
    public virtual void Exit()
    {
        isExitingState = true;
    }

    public virtual void LogicUpdate()
    {

    }

    public virtual void PhysicsUpdate()
    {
        
    }
}