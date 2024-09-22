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

	
	public override void Enter() {
		base.Enter();


	}

	public override void Exit() {
		base.Exit();
		canAttack = false;
	}

	public override void LogicUpdate() {

		base.LogicUpdate();	
		if(enemy.playerCheck == null){
			stateMachine.ChangeState(enemy.idleState);
			return;
		}	

		distancePlayer = Vector2.Distance(enemy.playerCheck.position,enemy.transform.position);
		xDirPlayer = (enemy.playerCheck.position.x > enemy.transform.position.x) ? 1 : -1;
		yDirPlayer = (enemy.playerCheck.position.y > enemy.transform.position.y) ? 1 : -1;
		if(distancePlayer >= enemy.AgroDistance){
			movement.Flip();
			stateMachine.ChangeState(enemy.idleState);			
		}
		if(TimeAction(enemyData.speedAtk)){
			canAttack = true;
		}
		enemy.canAttack =  this.canAttack;
	}

	public override void PhysicsUpdate() {
		base.PhysicsUpdate();
	}
}