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
    bool isWall;
    public bool isFall = true;
    public PlayerAirState(Player player, FiniteStateMachine stateMachine, PlayerData playerData, mState state) : base(player, stateMachine, playerData, state)
    {

    }
    
    public override void DoCheck(){
        base.DoCheck();
        isWall = movement.isWall();
    }
    public override void Enter(){

        base.Enter();
        movement.SetVelocityX(0);
    }
    public override void Exit(){
        base.Exit();
    }
    public override void LogicUpdate(){
        base.LogicUpdate();
        if(isExitingState) return;
        
        inputX = player.inputPlayer.MoveInput;
        jumpInput = player.inputPlayer.jumpInput;
        dashInput = player.inputPlayer.dashInput;
        
        if( player.inputPlayer.AttackInputs[(int)CombatInput.Attack1] ){
            isFall = true;  
            ChangeAttack(player.Attack_1,true);
            
        }else if(player.inputPlayer.AttackInputs[(int)CombatInput.Attack2]){
            isFall = true;  
            ChangeAttack(player.Attack_2,true);

        }else if(player.inputPlayer.AttackInputs[(int)CombatInput.Attack3]){ 
            isFall = true;  
            ChangeAttack(player.Attack_3,true);

        }else if(isGrounded && movement.Velocity.y < 1f){

            isFall = true;           
            stateMachine.ChangeState(player.idleState); 
        }   
        else if(jumpInput && player.jumpState.CanJump()){

            player.inputPlayer.UseJumpInput(); 
            stateMachine.ChangeState(player.jumpState);

        }

        else if(dashInput && player.dashState.CheckIfCanDash()){
            isFall = true;
            stateMachine.ChangeState(player.dashState);
        }
        else if(isFall){
            CheckDir();
            player.paintChar.state = mState.InAir;
            player.paintChar.stagejump = movement.Velocity.y;
        }
        else if( player.paintChar.state == mState.JumpMin ){
            CheckDir();
            player.paintChar.stagejump = movement.Velocity.y; 
        }
        else{
            CheckDir();
            player.paintChar.stagejump = movement.Velocity.y;         
        }
    }
    private void CheckDir(){
        movement.CheckIfShouldFlip(inputX);
        movement.SetVelocityX(playerData.movementSpeed * inputX);
    }
}
