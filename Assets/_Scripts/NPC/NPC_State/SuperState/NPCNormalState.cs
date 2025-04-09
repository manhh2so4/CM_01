using UnityEngine;

public class NPCNormalState : NPCState {
    protected float timeChangeState;
    //--------Input Action--------
    protected int inputX;
    bool jumpInput;
    public NPCNormalState(NPC _npc, FiniteStateMachine _stateMachine ) : base(_npc, _stateMachine) {
        
    }
    public override void Enter(){
        base.Enter();
        SetRandomMoveTime();

    }
    public override void Exit(){
        base.Exit();

    }
    public override void LogicUpdate(){
        base.LogicUpdate();
        if(isExitingState) return;

        if(!isGrounded){     
            stateMachine.ChangeState(npc.air);
            return;
        }

        inputX =  npc.findPathHandle.moveInput;
        jumpInput = npc.findPathHandle.jumpInput;
        if( jumpInput ){
            stateMachine.ChangeState(npc.jump); 
            return;
        }

        if(isFollowPath) return;
        if( npc.Target ){
			if( Vector2.Distance ( npc.Target.position , npc.transform.position) < npc.RangeMove){
				stateMachine.ChangeState(npc.charge);
                return;
			}
		}
    }

    protected bool GetRandomBoolean()
    {
        int randomInt = Random.Range(0, 2);
        return randomInt == 0 ? false : true;
    }

    void SetRandomMoveTime() {
		timeChangeState = Random.Range( 2, 5);
	}


}