using UnityEngine;

public class EnemyCombatState : EnemyState
{
	protected float distancePlayer;
	protected int xDirPlayer;
	protected int yDirPlayer;
	protected bool canAttack;
    public EnemyCombatState(Enemy enemy, FiniteStateMachine stateMachine) : base(enemy, stateMachine)
    {
    }
    public override void DoChecks() {
		base.DoChecks();
		
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
		distancePlayer = Vector2.Distance(enemy.playerCheck.position,enemy.transform.position);
		xDirPlayer = (enemy.playerCheck.position.x > enemy.transform.position.x) ? 1 : -1;
		yDirPlayer = (enemy.playerCheck.position.y > enemy.transform.position.y) ? 1 : -1;

		if(enemy.playerCheck == null){
			stateMachine.changeStage(enemy.idleState);
		}
		if(distancePlayer >= enemy.AgroDistance){
			Movement.Flip();
			stateMachine.changeStage(enemy.idleState);			
		}

		if (Time.time >= startTime + enemyData.speedAtk) {
			canAttack = true;
		}
		enemy.canAttack =  this.canAttack;
	}

	public override void PhysicsUpdate() {
		base.PhysicsUpdate();
	}
}