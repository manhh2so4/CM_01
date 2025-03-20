using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : State
{
	protected CollisionSenses collisionSenses;
    protected Player player;
    protected PlayerData playerData;
    protected bool isExitingState;
    protected float startTime;
    protected bool isAbilityDone = true;
    private mState mState;
    protected FiniteStateMachine stateMachine;

    public PlayerState(Player _player,FiniteStateMachine _stateMachine, PlayerData _playerData ,mState _state){
        this.player = _player;
        this.stateMachine = _stateMachine;
        this.playerData = _playerData;
        this.mState = _state;
        core = player.core;
        movement = core.GetCoreComponent<Movement>();
        collisionSenses = core.GetCoreComponent<CollisionSenses>();
    }
    public override void Enter(){
        base.Enter();
        DoCheck();
        player.Anim.state = mState;
        startTime = Time.time;
        isExitingState = false;
    }
    public override void Exit(){
        base.Exit();
        isExitingState = true;
    }
    public override void LogicUpdate(){
        base.LogicUpdate();
        DoCheck();
    }
    public override void PhysicsUpdate(){
        base.PhysicsUpdate();       
    }
    public virtual void DoCheck(){
        
    }
    protected void ChangeAttack( PlayerAttackState Skill){
        if(isAbilityDone == false ) return;

        if(Skill.GetSkill().Data == null){
            Common.Log("Player haven't weapon");
            return;
        }
        stateMachine.ChangeState(Skill);
        player.Anim.setSkill(Skill.GetSkill());
    }
}
