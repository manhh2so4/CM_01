using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B_IdleState : BossNormalState
{

	
    public B_IdleState(Boss boss,FiniteStateMachine stateMachine) : base(boss, stateMachine) {}
	public override void Enter() {
		base.Enter();
		boss.draw_Boss.state = StateEnemy.Idle;		
		movement?.SetVelocityZero();
        movement.CanSetVelocity = false;
		SetRandomIdleTime();
		

	}

	public override void Exit() {
		base.Exit();
        movement.CanSetVelocity = true;
		if(GetRandomBoolean()) movement.Flip();
	}
	
	public override void LogicUpdate() {
		base.LogicUpdate();
		if (Time.time >= startTime + timeChangeState) {
			stateMachine.ChangeState(boss.moveState);
		}	
	}
	private void SetRandomIdleTime() {
		timeChangeState = Random.Range(boss.minIdleTime, boss.maxIdleTime);
	}
}
