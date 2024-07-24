using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState
{
    protected Player player;
    protected PlayerStateMachine stateMachine;
    protected PlayerData playerData;

    protected bool isAnimationFinished;
    protected bool isExitingState;
    protected float startTime;
    private mState mState;

    public PlayerState(Player _player,PlayerStateMachine _stateMachine, PlayerData _playerData ,mState _state){
        this.player = _player;
        this.stateMachine = _stateMachine;
        this.playerData = _playerData;
        this.mState = _state;
    }
    public virtual void Enter(){
        DoCheck();
        player.Anim.state = mState;
        startTime = Time.time;
        isAnimationFinished = false;
        isExitingState = false;
    }
    public virtual void Exit(){
        isExitingState = true;
    }
    public virtual void LogicUpdate(){
        //Debug.Log("State_update" + mState.ToString());
    }
    public virtual void PhysicsUpdate(){
        DoCheck();
    }
    public virtual void DoCheck(){
        
    }
    public virtual void AnimationTrigger(){ }
    public virtual void AnimationFinishTrigger() => isAnimationFinished = true;
}
