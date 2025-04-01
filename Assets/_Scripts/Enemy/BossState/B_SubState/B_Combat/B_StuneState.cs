using UnityEngine;

public class B_StuneState : BossAbilityState
{
    public B_StuneState(Boss boss, FiniteStateMachine stateMachine) : base(boss, stateMachine)
    {

    }
	public override void Enter() {
		base.Enter();
		movement.SetVelocityZero();
	}
	public override void Exit() {
		base.Exit();
	}
	public override void LogicUpdate() {
		base.LogicUpdate(); 
		if(isExitingState) return;

		if(boss.poiseReceiver.IsPoise() == false){
			isAbilityDone = true;
		}

	}
}