using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLandState : PlayerGroundedState
{
    public PlayerLandState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, mState state) : base(player, stateMachine, playerData, state)
    {
        
    }
    public override void LogicUpdate(){
        base.LogicUpdate();
        if(!isExitingState){
            if(inputX != 0){
            stateMachine.ChangeState(player.moveState);
            }else{
            stateMachine.ChangeState(player.idleState);
        }
        }
        
    }
}
