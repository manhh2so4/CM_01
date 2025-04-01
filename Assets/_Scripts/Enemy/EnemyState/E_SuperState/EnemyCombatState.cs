using UnityEngine;

public class EnemyCombatState : E_State
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
		checkPlayer();


	}

	public override void Exit() {
		base.Exit();
		canAttack = false;
	}

	public override void LogicUpdate() {
		base.LogicUpdate();	
		if(isExitingState) return;

		if(enemy.playerCheck == null){
			stateMachine.ChangeState(enemy.idleState);
			return;
		}

		if( cooldowns.IsDone(enemy.attackState) ) {
            stateMachine.ChangeState(enemy.attackState);  
            return;
        }

		checkPlayer();	

		if(distancePlayer >= enemy.AgroDistance){
			movement.Flip();
			stateMachine.ChangeState(enemy.moveState);
			return;			
		}
		
		
		enemy.canAttack =  this.canAttack;
	}
	void checkPlayer(){
		if(enemy.playerCheck == null) return;
		distancePlayer = Vector2.Distance(enemy.playerCheck.position,enemy.transform.position);
		xDirPlayer = (enemy.playerCheck.position.x > enemy.transform.position.x) ? 1 : -1;
		yDirPlayer = (enemy.playerCheck.position.y > enemy.transform.position.y) ? 1 : -1;
	}
}