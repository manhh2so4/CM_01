using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTouchingWall : PlayerState
{   
    protected bool isWall;
    protected int inputX;
    protected bool jumpInput;
    public PlayerTouchingWall(Player _player, FiniteStateMachine _stateMachine, PlayerData _playerData, mState _state) : base(_player, _stateMachine, _playerData, _state)
    {
    }
    public override void DoCheck(){
        base.DoCheck();
        isWall = movement.isWall();
    }
    public override void Enter(){
        base.Enter();
    }
    public override void Exit(){
        base.Exit();
    }
    public override void LogicUpdate(){
        base.LogicUpdate();
        if(isExitingState) return;
        
        inputX = player.inputPlayer.MoveInput;
        jumpInput = player.inputPlayer.jumpInput;
        if(jumpInput){

            player.wallJumpState.DetermineWallJumpDir(isWall);
            stateMachine.ChangeState(player.wallJumpState);

        }else if(isGrounded){

            stateMachine.ChangeState(player.idleState);

        }else if( inputX != movement.facingDirection){

            stateMachine.ChangeState(player.airState);

        }
    }
}
