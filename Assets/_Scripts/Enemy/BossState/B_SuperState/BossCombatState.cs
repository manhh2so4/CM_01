using UnityEngine;

public class BossCombatState : BossState
{
	
	protected bool canAttack;
    public BossCombatState(Boss boss, FiniteStateMachine stateMachine) : base(boss, stateMachine)
    {

    }
	public override void Enter() {
		base.Enter();


	}

	public override void Exit() {
		base.Exit();
		canAttack = false;
	}

	public override void LogicUpdate() {

		base.LogicUpdate();	
		if(boss.playerCheck == null){
			stateMachine.ChangeState(boss.idleState);
			return;
		}	
		
		if(distancePlayer >= boss.AgroDistance){
			movement.Flip();
			stateMachine.ChangeState(boss.idleState);			
		}
		
		if(TimeAction(bossData.speed_attack)){
			canAttack = true;
		}
		boss.canAttack =  this.canAttack;
	}

	public override void PhysicsUpdate() {
		base.PhysicsUpdate();
	}
}