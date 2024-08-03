using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerGroundedState
{
    public PlayerMoveState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, mState state) : base(player, stateMachine, playerData, state)
    {
    }

    public override void DoCheck(){
        base.DoCheck();
    }
    public override void Enter(){
        base.Enter();
        core.Movement.SetVelocityX(0f);
    }
    public override void Exit(){
        base.Exit();
    }
    public override void LogicUpdate(){
        base.LogicUpdate();
        core.Movement.SetVelocityX(playerData.movementSpeed * inputX);
        core.Movement.CheckIfShouldFlip(inputX);
        if(inputX == 0 && !isExitingState){
            stateMachine.ChangeState(player.idleState);
        }
    }
    public override void PhysicsUpdate(){
        base.PhysicsUpdate();
    }
    
}
