using UnityEngine;

public class B_Attack : BossAbilityState
{
	StateEnemy attackState;
	float holdTime = .2f;
	bool isAttack;
    public B_Attack(Boss boss, FiniteStateMachine stateMachine, StateEnemy attackState) : base(boss, stateMachine)
    {
		this.attackState = attackState;
    }
	public override void Enter() {
		base.Enter();
		boss.draw_Boss.state = StateEnemy.Hold;
		isAttack = false;
		boss.draw_Boss.OnAttackDone += ExitHandler;
		boss.draw_Boss.OnTakeDamage += TakeDamage;
		movement.SetVelocityZero();
	}
	public override void LogicUpdate(){
		base.LogicUpdate();
		if(isExitingState) return;

		if(isAttack) return;
		if(Time.time >= startTime + holdTime){
			boss.draw_Boss.state = attackState;
			isAttack = true;
		}

	}
	public override void Exit() {
		base.Exit();
		boss.draw_Boss.OnAttackDone -= ExitHandler;
		boss.draw_Boss.OnTakeDamage -= TakeDamage;
	}
}