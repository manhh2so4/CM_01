using UnityEngine;

public class BossCombatState : BossState
{
	
    public BossCombatState( Boss boss, FiniteStateMachine stateMachine ) : base( boss, stateMachine )
    {
    }
	public override void Enter() {
		base.Enter();
	}
	public override void Exit() {
		base.Exit();
	}

	public override void LogicUpdate() {

		base.LogicUpdate();	
		if( distancePlayer >= boss.AgroDistance || boss.playerCheck == null){
			movement.Flip();
			stateMachine.ChangeState(boss.idleState);
			return;			
		}
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

