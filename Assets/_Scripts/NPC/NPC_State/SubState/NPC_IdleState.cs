using UnityEngine;

public class NPC_IdleState : NPCNormalState {
    public NPC_IdleState(NPC _npc, FiniteStateMachine _stateMachine) : base(_npc, _stateMachine) {
    
    }
    public override void Enter(){
        base.Enter();
        paintChar.state = mState.Idle;
        movement.SetVelocityX(0f);
    }
    public override void LogicUpdate(){
        base.LogicUpdate();
        if(isExitingState) return;

        movement.SetVelocityX(0f);

        if( inputX != 0 ){
            stateMachine.ChangeState(npc.move);
        }


        // if(Time.time >= startTime + timeChangeState) {
        //     stateMachine.ChangeState( npc.move );
        //     if(GetRandomBoolean()) movement.Flip();
        // }
    }

}
