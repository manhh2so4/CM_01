using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_IdleState : EnemyNormalState
{

	
    public E_IdleState(Enemy enemy,FiniteStateMachine stateMachine) : base(enemy, stateMachine) {}
	public override void Enter() {
		base.Enter();	
		movement?.SetVelocityZero();
	}
	public override void Exit() {
		base.Exit();
		if( GetRandomBoolean() ) movement.Flip();
	}
	
	public override void LogicUpdate() {
		base.LogicUpdate();
		if(isExitingState) return;
		
		movement?.SetVelocityX(0);
		if (Time.time >= startTime + timeChangeState) {
			stateMachine.ChangeState(enemy.moveState);
		}

		if(TimeRate(0.4f/enemyData.speedMove)) return;
		
		switch(enemyData.type){
            case 0:
			case 1:                             
            case 2:
            case 3:
				enemy.Paint(0);
                break;
			case 5: 
            case 4: 
				movement?.SetVelocityZero();               
                enemy.Paint(FrameCurrent);
                FrameCurrent = ( FrameCurrent + 1)%2; 
                break;
			
        }		
	}
}
