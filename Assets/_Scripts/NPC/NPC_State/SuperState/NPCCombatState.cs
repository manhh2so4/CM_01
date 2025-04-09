using UnityEngine;

public class NPCCombatState : NPCState
{
    protected int xDirTarget,yDirTarget;
    protected float xDisTarget,yDisTarget;
    protected float distanceTarget;

    public NPCCombatState( NPC npc, FiniteStateMachine stateMachine ) : base( npc, stateMachine )
    {

    }
	public override void Enter() {
		base.Enter();
        CheckTarget();
	}
	public override void Exit() {
		base.Exit();
	}

	public override void LogicUpdate() {
		base.LogicUpdate();
        if(isExitingState) return;

        CheckTarget();
        ChangeAttack( npc.attack1 );
	}
    private void CheckTarget()
    {
        if (npc.Target != null)
        {
            distanceTarget = Vector2.Distance( npc.Target.position, npc.transform.position );
            xDisTarget = npc.Target.position.x - npc.transform.position.x ;
            yDisTarget = npc.Target.position.y - npc.transform.position.y;
            xDirTarget = (npc.Target.position.x > npc.transform.position.x) ? 1 : -1;
            yDirTarget = (npc.Target.position.y > npc.transform.position.y) ? 1 : -1;
        }
        else{
            Common.Log("Player not found");
            stateMachine.ChangeState(npc.idle);
        }
    }
    protected void ChangeAttack( NPC_AttackState Skill , bool fly = false ){
        
        if( Skill.GetSkill().hasWeapon == false ){
            Common.Log("Player haven't weapon");
            return;
        }
        if( distanceTarget > Skill.rangeAttack ) return;

        if( cooldowns.IsDone( Skill ) == false) return;
        
        if(fly){
            movement.SetVelocityY(1); 
        }
        stateMachine.ChangeState( Skill );
        
    }
	
	#region funcTimer
    private float timeAction = 0;
    protected bool TimeAction(float timeWait){        
        timeAction += Time.deltaTime;
        if(timeAction >= timeWait){
            timeAction = 0;
            return true;
        }
        return false;
    }
    #endregion

}