using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PlayerGroundedState : PlayerState
{   
    protected int inputX;
    private bool jumpInput;
    bool dashInput;
    public PlayerGroundedState(Player player, FiniteStateMachine stateMachine, PlayerData playerData, mState state) : base(player, stateMachine, playerData, state)
    {
    }
    public override void DoCheck(){
        base.DoCheck();
    }
    public override void Enter(){
        base.Enter();
        player.jumpState.ResetAmountJunpsLeft();
        player.dashState.ResetCanDash();
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

        if(player.inputPlayer.AttackInputs[(int)CombatInput.Attack1] ){

            ChangeAttack(player.Attack_1);
            
        }else if(player.inputPlayer.AttackInputs[(int)CombatInput.Attack2] ){
            
            ChangeAttack(player.Attack_2);

        }else if(player.inputPlayer.AttackInputs[(int)CombatInput.Attack3] ){
            
            ChangeAttack(player.Attack_3);
        }
        else if(jumpInput && player.jumpState.CanJump()){

            player.inputPlayer.UseJumpInput();
            stateMachine.ChangeState(player.jumpState);    

        }else if(!isGrounded){     

            stateMachine.ChangeState(player.airState);
            player.jumpState.DecreaseAmountJumps();

        }else if(dashInput && player.dashState.CheckIfCanDash()){

            stateMachine.ChangeState(player.dashState);
        }
    }
        
}
