using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : PlayerAbilityState
{
    public PlayerAttackState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, mState state) : base(player, stateMachine, playerData, state)
    {

    }
    public override void DoCheck(){
        base.DoCheck();
    }
    public override void Enter(){
        base.Enter();
    }
    public override void Exit(){
        base.Exit();
    }
    public override void LogicUpdate(){
        base.LogicUpdate();
    }
    public override void PhysicsUpdate(){
        base.PhysicsUpdate();
    }
    public override void AnimationFinishTrigger()
    {   
        base.AnimationFinishTrigger();                
        isAbilityDone = true;
    }
}
