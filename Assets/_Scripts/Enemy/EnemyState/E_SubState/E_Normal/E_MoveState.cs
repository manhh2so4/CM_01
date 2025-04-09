using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_MoveState : EnemyNormalState
{    
    
    public E_MoveState(Enemy enemy,FiniteStateMachine stateMachine) : base(enemy, stateMachine) {
		
	}


	public override void Enter() {
		base.Enter();
	}

	public override void Exit() {
		base.Exit();
	}
	public override void LogicUpdate() {
		base.LogicUpdate();
        if(isExitingState) return;
        
        if (Time.time >= startTime + timeChangeState){
			stateMachine.ChangeState(enemy.idleState);
		}

        
        switch(enemyData.type){			
			case 0:
				enemy.Paint(0);
				break;
            case 1:                             
                
                if( (isWall || !isLedge ) && isGround) movement.Flip();
                if(isGround) movement?.SetVelocityX(enemyData.speedMove * movement.facingDirection);

                if(TimeRate(0.35f/enemyData.speedMove)) return;

                enemy.Paint(FrameCurrent);
                FrameCurrent = ( FrameCurrent + 1)%2; 
                break;

            case 2:
                break;

            case 4:
                if(TimeRate(0.35f/enemyData.speedMove)) return;
                if( Mathf.Abs(XDirPos) > enemy.RangeMove ){                    
                    movement.CheckIfShouldFlip( XDirPos > 0 ? -1 : 1 );
                } 

                if( Mathf.Abs( enemy.transform.position.y  - enemyPos.y ) > 1.5 ){
                    dirY = enemyPos.y - enemy.transform.position.y;
                }else{
                    dirY = Random.Range(-1f,1f);
                }
                vY *= -1;

                movement?.SetVelocity( enemyData.speedMove/3 * movement.facingDirection,  dirY - vY);

                enemy.Paint(FrameCurrent);
                FrameCurrent = ( FrameCurrent + 1)%2; 

                break;
                
            default:
                enemy.Paint(0);
				break;           
        } 

        
	}
    

}
