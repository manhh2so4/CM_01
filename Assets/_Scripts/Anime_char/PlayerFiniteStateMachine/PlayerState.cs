using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState
{
    protected Player player;
    protected PlayerStateMachine stateMachine;
    protected PlayerData playerData;
    protected float startTime;
    private mState mState;

    public PlayerState(Player player,PlayerStateMachine stateMachine, PlayerData playerData ,mState state){
        this.player = player;
        this.stateMachine = stateMachine;
        this.playerData = playerData;
        this.mState = state;
    }
    public virtual void Enter(){
        DoCheck();
        startTime = Time.time;
    }
    public virtual void Exit(){
        
    }
    public virtual void LogicUpdate(){
        
    }
    public virtual void PhysicsUpdate(){
        DoCheck();
    }
    public virtual void DoCheck(){
        
    }
}
