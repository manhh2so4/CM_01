using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilityState : PlayerState
{   
    public PlayerAbilityState(Player player, FiniteStateMachine stateMachine, PlayerData playerData, mState state) : base(player, stateMachine, playerData, state)
    {
    }
    public override void DoCheck(){
        base.DoCheck();
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
        if(isExitingState) return;
        
        if(isAbilityDone){          
            if (isGrounded && movement.Velocity.y < 0.1f)
            {
                stateMachine.ChangeState(player.moveState);
            }
            else
            {
                stateMachine.ChangeState(player.airState);
            }
        }
    }
    
}
