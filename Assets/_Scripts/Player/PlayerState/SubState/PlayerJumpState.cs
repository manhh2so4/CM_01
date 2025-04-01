using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerAbilityState
{
    int amountOfjumpLeft;
    public PlayerJumpState(Player player, FiniteStateMachine stateMachine, PlayerData playerData, mState state) : base(player, stateMachine, playerData, state)
    {
        amountOfjumpLeft = playerData.amountOfJumps;
    }
    public override void DoCheck(){
        base.DoCheck();
    }
    public override void Enter(){
        base.Enter();
        player.inputPlayer.UseJumpInput();
        player.airState.isFall = false;
        float velJump = Mathf.Lerp(playerData.MinJumpVelocity, playerData.MaxJumpVelocity,player.inputPlayer.amountJump);
        
        if( player.inputPlayer.amountJump < 0.5 ){
            player.Anim.state = mState.JumpMin;
        }else{
            player.Anim.state = mState.JumpMax;
            player.Anim.SetStateVy(velJump);
        }
        movement.SetVelocityY(velJump); 
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
