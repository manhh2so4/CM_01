using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerGroundedState
{
    public PlayerMoveState(Player player, FiniteStateMachine stateMachine, PlayerData playerData, mState state) : base(player, stateMachine, playerData, state)
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
        if(isExitingState) return;
        
        movement.SetVelocityX(playerData.movementSpeed * inputX);
        movement.CheckIfShouldFlip(inputX);
        if (!isExitingState)
        {
            if( Mathf.Abs(movement.Velocity.x) < .5f && inputX == 0){
                stateMachine.ChangeState(player.idleState);
            }
        }
    }

    
}
