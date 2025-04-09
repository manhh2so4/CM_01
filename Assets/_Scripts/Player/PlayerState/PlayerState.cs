using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : State
{

    protected Player player;
    protected PlayerData playerData;
    protected FiniteStateMachine stateMachine;
    protected Cooldown cooldowns;
    
    protected float startTime;
    protected bool isAbilityDone = true;
    private mState mState;
    protected bool isGrounded;

    public PlayerState(Player _player,FiniteStateMachine _stateMachine, PlayerData _playerData ,mState _state){
        this.player = _player;
        this.stateMachine = _stateMachine;
        this.playerData = _playerData;
        this.mState = _state;
        this.cooldowns = _player.cooldowns;
        core = player.core;
        movement = core.GetCoreComponent<Movement>();
    }
    public override void Enter(){
        base.Enter();
        DoCheck();
        player.paintChar.state = mState;
        startTime = Time.time;
    }
    public override void Exit(){
        base.Exit();
    }
    public override void LogicUpdate(){
        base.LogicUpdate();
        DoCheck();
    }
    public virtual void DoCheck(){
        isGrounded = movement.isGround();
    }
    protected void ChangeAttack( PlayerAttackState Skill , bool fly = false ){

        if( cooldowns.IsDone(Skill) == false) return;

        if( Skill.GetSkill().hasWeapon == false ){
            Common.Log("Player haven't weapon");
            return;
        }
        if(fly){
            movement.SetVelocityY(1); 
        }
        stateMachine.ChangeState(Skill);
        
    }
}
