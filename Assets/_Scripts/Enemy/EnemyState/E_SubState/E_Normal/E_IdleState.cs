using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_IdleState : EnemyNormalState
{

	
    public E_IdleState(Enemy enemy,FiniteStateMachine stateMachine) : base(enemy, stateMachine) {}
	public override void Enter() {
		base.Enter();
		enemy.state = StateEnemy.Idle;		
		movement?.SetVelocityZero();
		SetRandomIdleTime();
		

	}

	public override void Exit() {
		base.Exit();
		if(GetRandomBoolean()) movement.Flip();
	}
	
	public override void LogicUpdate() {
		base.LogicUpdate();
		if (Time.time >= startTime + timeChangeState) {
			stateMachine.ChangeState(enemy.moveState);
		}
		if(TimeRate(0.4f/enemyData.speedMove)) return;
		movement?.SetVelocityZero();
		switch(enemyData.type){
            case 0:
				enemy.Paint(0);
				break;
            case 1:

				enemy.Paint(0);
                break;
            case 4:                
                enemy.Paint(FrameCurrent);
                FrameCurrent = ( FrameCurrent + 1)%2; 

                break;
        }		
	}
	private void SetRandomIdleTime() {
		timeChangeState = Random.Range(enemy.minIdleTime, enemy.maxIdleTime);
	}
}
