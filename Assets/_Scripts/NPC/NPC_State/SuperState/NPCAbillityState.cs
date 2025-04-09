using UnityEngine;
public class NPCAbillityState : NPCState
{
    protected bool isAbilityDone;
    public NPCAbillityState(NPC npc, FiniteStateMachine stateMachine) : base(npc, stateMachine)
    {

    }
    public override void Enter(){
        base.Enter();
        isAbilityDone = false;
    }
    public override void Exit(){
        base.Exit();
    }

    public override void LogicUpdate(){
        base.LogicUpdate();
        if(isExitingState) return;

        if(isAbilityDone){

            if ( isGrounded && movement.Velocity.y < 0.1f )
            {
                if(npc.Target != null)
                stateMachine.ChangeState( npc.look );
                else
                stateMachine.ChangeState( npc.idle );
            }else{
                stateMachine.ChangeState(npc.air);
            }

        }
    }

    protected void ExitHandler(){
        isAbilityDone = true;
    }

}