using UnityEngine;

public class E_KnockBack : EnemyAbilityState
{
    public E_KnockBack(Enemy enemy, FiniteStateMachine stateMachine) : base(enemy, stateMachine){}
	public override void Enter() {
		base.Enter();
		enemy.Paint(2);

	}
	public override void Exit() {
		base.Exit();
		
	}
	public override void LogicUpdate() {
		base.LogicUpdate();
		if(isExitingState) return;

		if(enemy.knockBackReceiver.isKnockBack == false) isAbilityDone = true;
		
	}
}