using UnityEngine;
public abstract class State {
    protected Core core;
    protected Movement movement;
    public virtual void Enter()
    {
        
    }
    public virtual void Exit()
    {
        
    }

    public virtual void LogicUpdate()
    {

    }

    public virtual void PhysicsUpdate()
    {
        
    }
}