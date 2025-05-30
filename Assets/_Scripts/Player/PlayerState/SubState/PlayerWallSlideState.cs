using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallSlideState : PlayerTouchingWall
{
    public PlayerWallSlideState(Player _player, FiniteStateMachine _stateMachine, PlayerData _playerData, mState _state) : base(_player, _stateMachine, _playerData, _state)
    {
    }
    public override void LogicUpdate(){
        base.LogicUpdate();
        if(isExitingState) return;
        
        movement.SetVelocityY(-playerData.wallSlideVelocity);
    }
}
