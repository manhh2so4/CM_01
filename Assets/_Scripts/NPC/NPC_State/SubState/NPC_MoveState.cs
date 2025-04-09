using UnityEngine;

public class NPC_MoveState : NPCNormalState {
    bool isIdle;
    public NPC_MoveState(NPC _npc, FiniteStateMachine _stateMachine) : base(_npc, _stateMachine) {
    
    }
    public override void Enter(){
        base.Enter();
        paintChar.state = mState.Moving;
        movement?.SetVelocityX(0f);
    }
    public override void LogicUpdate(){
        base.LogicUpdate();
        if(isExitingState) return;

        movement.SetVelocityX(npc.mSpeed* inputX);
        movement.CheckIfShouldFlip(inputX);

        if( Mathf.Abs(movement.Velocity.x) < .1f && inputX == 0){
            stateMachine.ChangeState(npc.idle);
        }


        // if(Time.time >= startTime + timeChangeState) {
        //     stateMachine.ChangeState( npc.idle );
        // }
        
        // if(isGrounded) movement?.SetVelocityX( 5 * movement.facingDirection);

        // if( ( isWall || !isledge ) && isGrounded ){
        //     if( GetRandomBoolean() ){
        //         stateMachine.ChangeState( npc.idle );
        //         return;
        //     }
        //     movement.Flip();
        // }

        // if( Mathf.Abs(XDirPos) > npc.RangeMove ){
        //     if( GetRandomBoolean() && ( isIdle == false) ){
        //         isIdle = true;
        //         stateMachine.ChangeState( npc.idle );
        //         return;
        //     }
            
        //     movement.CheckIfShouldFlip( XDirPos > 0 ? -1 : 1 );
        // } 
        // if( Mathf.Abs(XDirPos) < (npc.RangeMove - 1) ){
        //         isIdle = false;
        // }
        
    }
    //---------Follow Path---------

}