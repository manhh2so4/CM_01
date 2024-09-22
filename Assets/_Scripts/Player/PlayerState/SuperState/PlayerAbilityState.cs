using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilityState : PlayerState
{   
    
    
    protected bool isAbilityDone;
    protected bool isGrounded;
    public PlayerAbilityState(Player player, FiniteStateMachine stateMachine, PlayerData playerData, mState state) : base(player, stateMachine, playerData, state)
    {
    }
    public override void DoCheck(){
        base.DoCheck();
        isGrounded = collisionSenses.isGround;    
    }
    public override void Enter(){
        base.Enter();
        isAbilityDone = false;
    }
    public override void Exit(){
        base.Exit();
    }
    public override void LogicUpdate(){
        base.LogicUpdate();
        if(isAbilityDone){          
            if (isGrounded && movement.CurrentVelocity.y < 0.1f)
            {
                stateMachine.ChangeState(player.idleState);
            }
            else
            {
                stateMachine.ChangeState(player.airState);
            }
        }
    }
    public override void PhysicsUpdate(){
        base.PhysicsUpdate();
    }        
}
