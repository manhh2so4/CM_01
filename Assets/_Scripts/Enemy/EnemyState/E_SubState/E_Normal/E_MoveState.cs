using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_MoveState : EnemyNormalState
{    
    
    public E_MoveState(Enemy enemy,FiniteStateMachine stateMachine) : base(enemy, stateMachine) {
		
	}
    public override void DoChecks() {
		base.DoChecks();
	}

	public override void Enter() {
		base.Enter();
        enemy.state = StateEnemy.Moving;

        SetRandomMoveTime();
	}

	public override void Exit() {
		base.Exit();
	}
	public override void LogicUpdate() {
		base.LogicUpdate();
        if (Time.time >= startTime + timeChangeState){
			stateMachine.changeStage(enemy.idleState);
		}
        if(TimeRate(0.35f/enemyData.speedMove)) return;
        switch(enemyData.type){			
			case 0:
				enemy.Paint(0);
				break;
            case 1:                             
                

                if(isWall || !isLedge && isGround) Movement.Flip();

                if(isGround) Movement?.SetVelocityX(enemyData.speedMove * Movement.facingDirection);
                
                enemy.Paint(FrameCurrent);
                FrameCurrent = ( FrameCurrent + 1)%2; 
                break;

            case 2:
                break;

            case 4:

                if( Mathf.Abs(XDirPos) > enemy.RangeMove ){                    
                    Movement.CheckIfShouldFlip( XDirPos > 0 ? -1 : 1 );
                } 

                if(Mathf.Abs(enemy.transform.position.y  - enemyPos.y) > 1.5 ){
                    dirY = enemyPos.y - enemy.transform.position.y;
                }else{
                    dirY = Random.Range(-1f,1f);
                }
                vY *= -1;

                Movement?.SetVelocity( enemyData.speedMove/3 * Movement.facingDirection,  dirY - vY);

                enemy.Paint(FrameCurrent);
                FrameCurrent = ( FrameCurrent + 1)%2; 

                break;
                
            default:
                enemy.Paint(0);
				break;           
        } 

        
	}

	public override void PhysicsUpdate() {
		base.PhysicsUpdate();
	}
    private void SetRandomMoveTime() {
		timeChangeState = Random.Range(enemy.minIdleTime, enemy.maxIdleTime);
	}

}
