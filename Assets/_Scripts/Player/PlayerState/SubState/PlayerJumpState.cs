using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerAbilityState
{
    int amountOfjumpLeft;
    public PlayerJumpState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, mState state) : base(player, stateMachine, playerData, state)
    {
        amountOfjumpLeft = playerData.amountOfJumps;
    }
    public override void DoCheck(){
        base.DoCheck();
    }
    public override void Enter(){
        //Debug.Log("Enter amountOfjumpLeft: " + amountOfjumpLeft);
        player.inputPlayer.UseJumpInput();
        base.Enter();
        player.airState.isFall = false;
        Movement.SetVelocityY(playerData.jumpVelocity); 
        isAbilityDone = true;
        amountOfjumpLeft--;
    }
    public bool CanJump(){
        if(amountOfjumpLeft > 0){
            return true;
        }else
            return false;
    }
    public void ResetAmountJunpsLeft() => amountOfjumpLeft = playerData.amountOfJumps;
    public void DecreaseAmountJumps() => amountOfjumpLeft--;
}
