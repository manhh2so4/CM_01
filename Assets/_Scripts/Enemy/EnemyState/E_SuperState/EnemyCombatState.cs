using UnityEngine;

public class EnemyCombatState : E_State
{
	protected float distanceTarget;
	protected float XDisTarget;
	protected float YDisTarget;
	protected int xDirTarget;
	protected int yDirTarget;
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

		if( cooldowns.IsDone(enemy.attackState) && distanceTarget <= enemy.AgroDistance ) {
            stateMachine.ChangeState(enemy.attackState);  
            return;
        }

		checkPlayer();	

		if(distanceTarget >= enemy.AgroDistance){
			stateMachine.ChangeState(enemy.idleState);
			return;			
		}
		
		enemy.canAttack =  this.canAttack;
	}
	void checkPlayer(){
		if(enemy.playerCheck == null) return;
		distanceTarget = Vector2.Distance(enemy.playerCheck.position,enemy.transform.position);
		XDisTarget = Mathf.Abs(enemy.playerCheck.position.x - enemy.transform.position.x);
		YDisTarget = Mathf.Abs(enemy.playerCheck.position.y - enemy.transform.position.y);
		xDirTarget = (enemy.playerCheck.position.x > enemy.transform.position.x) ? 1 : -1;
		yDirTarget = (enemy.playerCheck.position.y > enemy.transform.position.y) ? 1 : -1;
	}
}