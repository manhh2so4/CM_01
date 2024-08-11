using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PlayerGroundedState : PlayerState
{   
    protected Movement Movement { get => movement ?? core.GetCoreComponent(ref movement); }
	private Movement movement;

	private CollisionSenses CollisionSenses { get => collisionSenses ?? core.GetCoreComponent(ref collisionSenses); }
	private CollisionSenses collisionSenses;

    protected int inputX;
    private bool jumpInput;
    private bool isGrounded;
    bool dashInput;
    public PlayerGroundedState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, mState state) : base(player, stateMachine, playerData, state)
    {
    }
    public override void DoCheck(){
        base.DoCheck();
        isGrounded = CollisionSenses.isGround;
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

        inputX = player.inputPlayer.MoveInput;
        jumpInput = player.inputPlayer.jumpInput;
        dashInput = player.inputPlayer.dashInput;
        if(player.inputPlayer.AttackInputs[(int)CombatInput.Attack1]){
            stateMachine.ChangeState(player.AttackStand);
        }else if(player.inputPlayer.AttackInputs[(int)CombatInput.Attack2]){
            
            stateMachine.ChangeState(player.AttackStand);

        }else if(jumpInput && player.jumpState.CanJump()){
            player.inputPlayer.UseJumpInput();
            stateMachine.ChangeState(player.jumpState);    

        }else if(!isGrounded){      
            stateMachine.ChangeState(player.airState);
            player.jumpState.DecreaseAmountJumps();
        }else if(dashInput && player.dashState.CheckIfCanDash()){
            stateMachine.ChangeState(player.dashState);
        }
    }
    public override void PhysicsUpdate(){
        base.PhysicsUpdate();    
    }
        
}
