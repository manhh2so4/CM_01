using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKnockState : PlayerAbilityState
{
    
    public PlayerKnockState(Player player, FiniteStateMachine stateMachine, PlayerData playerData, mState state) : base(player, stateMachine, playerData, state)
    {

    }
    public override void Enter(){
        base.Enter();
        //Time.timeScale = 0.01f;

    }
    public override void Exit(){
        base.Exit();
    }
    public override void LogicUpdate(){
        base.LogicUpdate();
        if(isExitingState) return;
        
        if(player.knockBackReceiver.isKnockBack == false){
            isAbilityDone = true;
            //Time.timeScale = 1f;
        }
    }

}