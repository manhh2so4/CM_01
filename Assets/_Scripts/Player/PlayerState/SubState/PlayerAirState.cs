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
    public PlayerAirState(Player player, FiniteStateMachine stateMachine, PlayerData playerData, mState state) : base(player, stateMachine, playerData, state)
    {

    }
    public override void DoCheck(){
        base.DoCheck();
        isGrounded = collisionSenses.isGround;
        isWall = collisionSenses.isWall;
        isWallBack = collisionSenses.isGround;
    }
    public override void Enter(){
        movement.SetVelocityX(0);
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
            movement.SetVelocityY(4); 
            ChangeAttack(player.Attack_1);
            
        }else if(player.inputPlayer.AttackInputs[(int)CombatInput.Attack2]){
            isFall = true;
            movement.SetVelocityY(4); 
            ChangeAttack(player.Attack_2);

        }else if(player.inputPlayer.AttackInputs[(int)CombatInput.Attack3]){
            isFall = true;
            movement.SetVelocityY(4); 
            ChangeAttack(player.Attack_3);

        }else if(isGrounded && movement.CurrentVelocity.y < 1f){

            isFall = true;           
            stateMachine.ChangeState(player.idleState); 
        }   
        else if(jumpInput && player.jumpState.CanJump()){

            player.inputPlayer.UseJumpInput(); 
            stateMachine.ChangeState(player.jumpState);

        
        }else if(isWall && inputX == movement.facingDirection && !isExitingState && (movement.CurrentVelocity.y < 0) ){

            isFall = true; 
            stateMachine.ChangeState(player.wallSlideState);
        }
        else if(dashInput && player.dashState.CheckIfCanDash()){
            isFall = true;
            stateMachine.ChangeState(player.dashState);
        }
        else if(isFall){
            CheckDir();
            player.Anim.state = mState.InAir;
            player.Anim.stagejump = movement.CurrentVelocity.y;
        }
        else if( player.Anim.state == mState.JumpMin ){
            CheckDir();
            player.Anim.stagejump = movement.CurrentVelocity.y; 
        }
        else{
            CheckDir();
            player.Anim.stagejump = movement.CurrentVelocity.y;         
        }
    }
    public override void PhysicsUpdate(){
        base.PhysicsUpdate();
    }
    private void CheckDir(){
        movement.CheckIfShouldFlip(inputX);
        movement.SetVelocityX(playerData.movementSpeed * inputX);
    }
}
