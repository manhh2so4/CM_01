using UnityEngine;

public class EnemyNormalState : E_State
{    
    protected float timeChangeState;
    public EnemyNormalState(Enemy enemy, FiniteStateMachine stateMachine) : base(enemy, stateMachine)
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
		if(enemy.playerCheck){
			if(Vector2.Distance(enemy.playerCheck.position,enemy.transform.position) < enemy.AgroDistance)
			stateMachine.ChangeState(enemy.lookState);
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