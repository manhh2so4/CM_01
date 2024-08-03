using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallGrabState : PlayerTouchingWall
{
    public PlayerWallGrabState(Player _player, PlayerStateMachine _stateMachine, PlayerData _playerData, mState _state) : base(_player, _stateMachine, _playerData, _state)
    {
    }    
}
