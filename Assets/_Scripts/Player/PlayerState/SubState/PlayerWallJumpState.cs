using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallJumpState : PlayerAbilityState
{
    int wallJumpDir;
    public PlayerWallJumpState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, mState state) : base(player, stateMachine, playerData, state)
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
        player.SetVeclocity(playerData.wallJumpVelocity,playerData.wallJumpAngle,wallJumpDir);
        player.CheckIfShouldFlip(wallJumpDir);
        player.jumpState.DecreaseAmountJumps();
    }
    public override void LogicUpdate(){
        base.LogicUpdate();
        player.Anim.stagejump = (int)System.Math.Round(player.currentVeclocity.y, System.MidpointRounding.AwayFromZero);
        if(Time.time >= startTime + playerData.wallJumpTime){
            isAbilityDone = true;
        }

    }
    public void DetermineWallJumpDir(bool isWall){
        if(isWall){
            wallJumpDir = -player.facingDirection;
        }else{
            wallJumpDir = player.facingDirection;
        }
    }

}
