using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_AirState : NPCState
{

    int inputX;
    public bool isFall = true;
    public NPC_AirState(NPC npc, FiniteStateMachine stateMachine) : base(npc, stateMachine)
    {

    }
    public override void Enter(){
        base.Enter();
        movement.SetVelocityX(0);
    }
    public override void LogicUpdate(){
        base.LogicUpdate();
        if(isExitingState) return;
        

        if( isGrounded && movement.Velocity.y < 1f ){
            isFall = true;           
            stateMachine.ChangeState( npc.idle ); 

        }else if(isFall){

            CheckDir();
            paintChar.state = mState.InAir;
            paintChar.stagejump = movement.Velocity.y;

        }
        else if( paintChar.state == mState.JumpMin ){

            CheckDir();
            paintChar.stagejump = movement.Velocity.y; 
        }
        else{
            CheckDir();          
            paintChar.stagejump = movement.Velocity.y;         
        }


    }

    public override void Exit(){
        base.Exit();
    }
    private void CheckDir(){

        inputX = npc.findPathHandle.moveInput;

        movement.CheckIfShouldFlip( inputX );

        movement.SetVelocityX( npcData.speedMove * inputX );

        if(isFollowPath) return;
        

        if( npc.Target != null ){
            float xDisTarget = npc.Target.position.x - npc.transform.position.x ;
            if( Mathf.Abs( xDisTarget ) > 1f ){
                int dir = (npc.Target.position.x > npc.transform.position.x) ? 1 : -1;
                movement.CheckIfShouldFlip( dir );
            }
            movement.SetVelocityX( npcData.speedMove * movement.facingDirection );
        }
        
    }
}