using UnityEngine;

public class NPC_LookState : NPCCombatState
{
	
    public NPC_LookState( NPC npc, FiniteStateMachine stateMachine) : base( npc, stateMachine)
    {

    }
	public override void Enter() {        
		base.Enter();
        paintChar.state = mState.Idle;	
        movement.SetVelocityZero();
	}
	
	public override void LogicUpdate() {
		base.LogicUpdate();
		if(isExitingState) return;
		
        movement.CheckIfShouldFlip( xDirTarget );
        

        if( Mathf.Abs( xDisTarget ) >= 1.5 ){                        
			stateMachine.ChangeState( npc.charge );
            return;
		}                
	}

	public override void Exit() {
		base.Exit();
	}
}