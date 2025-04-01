using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerGroundedState
{
    
    public PlayerIdleState(Player player, FiniteStateMachine stateMachine, PlayerData playerData, mState state) : base(player, stateMachine, playerData, state)
    {
    }

    public override void DoCheck(){
        base.DoCheck();
    }
    public override void Enter(){
        base.Enter();
        movement?.SetVelocityX(0f);
    }
    public override void Exit(){
        base.Exit();
    }
    public override void LogicUpdate(){
        base.LogicUpdate();
        if(isExitingState) return;
        
        if(inputX != 0 && !isExitingState ){
            stateMachine.ChangeState(player.moveState);
        }
    }

}
