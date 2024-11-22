using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B_MoveState : BossNormalState
{    
    
    public B_MoveState(Boss boss,FiniteStateMachine stateMachine) : base(boss, stateMachine) {
		
	}


	public override void Enter() {
		base.Enter();
        boss.draw_Boss.state = StateEnemy.Moving;
        SetRandomMoveTime();
	}

	public override void Exit() {
		base.Exit();
	}
	public override void LogicUpdate() {
		base.LogicUpdate();
        if (Time.time >= startTime + timeChangeState){
			stateMachine.ChangeState(boss.idleState);
		}
        if(isWall || !isLedge && isGround) movement.Flip();

        if(isGround) movement?.SetVelocityX(bossData.speed_move * movement.facingDirection);
	}

	public override void PhysicsUpdate() {
		base.PhysicsUpdate();
	}
    private void SetRandomMoveTime() {
		timeChangeState = Random.Range(boss.minIdleTime, boss.maxIdleTime);
	}

}
