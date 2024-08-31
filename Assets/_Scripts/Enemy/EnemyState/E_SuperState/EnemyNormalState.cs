using UnityEngine;

public class EnemyNormalState : EnemyState
{    
    protected float timeChangeState;
    public EnemyNormalState(Enemy enemy, FiniteStateMachine stateMachine) : base(enemy, stateMachine)
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
	}

	public override void LogicUpdate() {
		base.LogicUpdate();
		if(enemy.playerCheck){
			if(Vector2.Distance(enemy.playerCheck.position,enemy.transform.position) < enemy.AgroDistance)
			stateMachine.changeStage(enemy.lookState);
		}        
	}

	public override void PhysicsUpdate() {
		base.PhysicsUpdate();
	}
	public bool GetRandomBoolean()
    {
        int randomInt = Random.Range(0, 2);
        return randomInt == 0 ? false : true;
    }
}