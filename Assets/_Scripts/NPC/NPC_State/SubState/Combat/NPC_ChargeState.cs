using UnityEngine;

public class NPC_ChargeState : NPCCombatState
{
    public NPC_ChargeState(NPC npc, FiniteStateMachine stateMachine) : base(npc, stateMachine){}
	public override void Enter() {
		base.Enter();
        paintChar.state = mState.Moving;

	}

	public override void Exit() {
		base.Exit();
		
	}
	public override void LogicUpdate() {
		base.LogicUpdate();
		if(isExitingState) return;

        if( Mathf.Abs( xDisTarget ) < 1.5 ) {
            stateMachine.ChangeState( npc.look );
            return;
        }

		if( (isWall || !isledge) && isGrounded){
			stateMachine.ChangeState( npc.jump );
			return;
		}	

		movement.CheckIfShouldFlip(xDirTarget);
		if( isGrounded ) movement?.SetVelocityX( npcData.speedMove * movement.facingDirection*1.5f);
        
	}
}