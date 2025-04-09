using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_JumpState : NPCAbillityState
{
    int amountJumpMax = 3;
    int amountJump;
    public NPC_JumpState(NPC npc, FiniteStateMachine stateMachine) : base( npc, stateMachine)
    {

    }
    public override void DoCheck(){
        base.DoCheck();
    }
    public override void Exit(){
        base.Exit();
        npc.findPathHandle.amountJump = 0;
        npc.findPathHandle.jumpInput = false;
    }
    public override void Enter(){
        base.Enter();
        npc.air.isFall = false;
        amountJump = npc.findPathHandle.amountJump;

        if(amountJump == 0){
            isAbilityDone = true;
            return;
        }

        float velJump = Mathf.Lerp( npcData.MinJumpVel, npcData.MaxJumpVel , (float)amountJump / amountJumpMax);
        
        if( amountJump > 1 ){
            paintChar.state = mState.JumpMax;
            paintChar.SetStateVy(velJump);

        }else{
            paintChar.state = mState.JumpMin;
        }
        movement.SetVelocityY(velJump); 

        isAbilityDone = true;

    }
}
