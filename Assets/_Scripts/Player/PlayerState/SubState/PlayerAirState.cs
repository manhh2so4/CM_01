using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAirState : PlayerState
{
    //Input
    bool jumpInput;
    bool dashInput;
    private int inputX;
    //Check
    bool isGrounded;
    bool isWall;
    bool isWallBack;
    public bool isFall = true;
    public PlayerAirState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, mState state) : base(player, stateMachine, playerData, state)
    {
    }

    public override void DoCheck(){
        base.DoCheck();
        isGrounded = player.CheckTouchingGround();
        isWall = player.CheckTouchingWall();
        isWallBack = player.CheckTouchingGroundBack();
    }
    public override void Enter(){
        base.Enter();

    }
    public override void Exit(){
        base.Exit();
    }
    public override void LogicUpdate(){
        base.LogicUpdate();
        inputX = player.inputPlayer.MoveInput;
        jumpInput = player.inputPlayer.jumpInput;
        dashInput = player.inputPlayer.dashInput;
        if(player.inputPlayer.AttackInputs[(int)CombatInput.Attack1]){
            isFall = true; 
            core.Movement.SetVelocityY(4); 
            stateMachine.ChangeState(player.AttackFly);
            
        }else if(player.inputPlayer.AttackInputs[(int)CombatInput.Attack2]){
            isFall = true;
            core.Movement.SetVelocityY(4); 
            stateMachine.ChangeState(player.AttackFly);

        }else if(isGrounded && core.Movement.CurrentVelocity.y < 1f){

            isFall = true;           
            stateMachine.ChangeState(player.idleState); 
        }   
        else if(jumpInput && player.jumpState.CanJump()){

            player.inputPlayer.UseJumpInput(); 
            stateMachine.ChangeState(player.jumpState);

        
        }else if(isWall && inputX == core.Movement.facingDirection && !isExitingState){

            isFall = true; 
            stateMachine.ChangeState(player.wallSlideState);
        }
        else if(dashInput && player.dashState.CheckIfCanDash()){
            stateMachine.ChangeState(player.dashState);
        }
        else if(isFall){
            CheckDir();
            player.Anim.state = mState.InAir;
            player.Anim.stagejump = (int)System.Math.Round(core.Movement.CurrentVelocity.y, System.MidpointRounding.AwayFromZero);
        }
        else{
            CheckDir();
            player.Anim.state = mState.Jump;
            player.Anim.stagejump = (int)System.Math.Round(core.Movement.CurrentVelocity.y, System.MidpointRounding.AwayFromZero);         
        }
    }
    public override void PhysicsUpdate(){
        base.PhysicsUpdate();
    }
    private void CheckDir(){
        core.Movement.CheckIfShouldFlip(inputX );
        core.Movement.SetVelocityX(playerData.movementSpeed * inputX);
    }
}
