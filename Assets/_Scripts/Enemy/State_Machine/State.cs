using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class State 
{
    protected FiniteStateMachine stateMachine;
    protected Entity entity;
    protected float startTime;
    
    public State(Entity entity,FiniteStateMachine stateMachine){
        this.entity = entity;
        this.stateMachine = stateMachine;
    }
    public virtual void Enter(){
        startTime = Time.time;
    }
    public virtual void Exit(){
        
    }
    public virtual void LogicUpdate(){
        
    }
    public virtual void PhysicsUpdate(){
        
    }
}
