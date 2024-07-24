using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClimbState : PlayerTouchingWall
{
    public PlayerClimbState(Player _player, PlayerStateMachine _stateMachine, PlayerData _playerData, mState _state) : base(_player, _stateMachine, _playerData, _state)
    {
    }
}
