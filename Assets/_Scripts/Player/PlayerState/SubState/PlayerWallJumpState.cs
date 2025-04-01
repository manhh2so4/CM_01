using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallJumpState : PlayerAbilityState
{
    int wallJumpDir;
    public PlayerWallJumpState(Player player, FiniteStateMachine stateMachine, PlayerData playerData, mState state) : base(player, stateMachine, playerData, state)
    {
    }
   public override void DoCheck(){
        base.DoCheck();
    }
    public override void Enter(){

        base.Enter();
        player.inputPlayer.UseJumpInput();
        player.jumpState.ResetAmountJunpsLeft();
        player.airState.isFall = true;
        movement.SetVelocity(playerData.wallJumpVelocity,playerData.wallJumpAngle,wallJumpDir);
        movement.CheckIfShouldFlip(wallJumpDir);
        player.jumpState.DecreaseAmountJumps();
    }
    public override void LogicUpdate(){
        base.LogicUpdate();
        if(isExitingState) return;
        
        player.Anim.stagejump = (int)System.Math.Round(movement.Velocity.y, System.MidpointRounding.AwayFromZero);
        if(Time.time >= startTime + playerData.wallJumpTime){
            isAbilityDone = true;
        }

    }
    public void DetermineWallJumpDir(bool isWall){
        if(isWall){
            wallJumpDir = -movement.facingDirection;
        }else{
            wallJumpDir = movement.facingDirection;
        }
    }

}
