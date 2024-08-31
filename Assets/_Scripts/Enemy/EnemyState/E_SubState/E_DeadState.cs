using UnityEngine;

public class E_DeadState : EnemyState
{
    public E_DeadState(Enemy entity, FiniteStateMachine stateMachine, Enemy_SO enemyData) : base(entity, stateMachine)
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
	}

	public override void PhysicsUpdate() {
		base.PhysicsUpdate();
	}
}