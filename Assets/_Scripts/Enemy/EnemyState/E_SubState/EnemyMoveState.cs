using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveState : EnemyState
{
    private Movement Movement { get => movement ?? core.GetCoreComponent(ref movement); }
    private Movement movement;
    protected bool isDetectingWall;
	protected bool isDetectingLedge;
	protected bool isPlayerInMinAgroRange;
    public EnemyMoveState(Enemy enemy,FiniteStateMachine stateMachine,Enemy_SO enemyData) : base(enemy, stateMachine, enemyData) {
		
	}
    public override void DoChecks() {
		base.DoChecks();
		isDetectingLedge = enemy.isledge();
		isDetectingWall = enemy.isWall();
		isPlayerInMinAgroRange = enemy.CheckPlayerInMaxAgroRange();
	}

	public override void Enter() {
		base.Enter();
		Movement?.SetVelocityX(enemyData.speedMove * Movement.facingDirection);
	}

	public override void Exit() {
		base.Exit();
	}

	public override void LogicUpdate() {
		base.LogicUpdate();
        switch(enemyData.type){			
			case 0:
				enemy.Paint(0);
				break;
            case 1:
                if(isDetectingWall || !isDetectingLedge)
                {
                    Movement.Flip();
                }
                Movement?.SetVelocityX(enemyData.speedMove * Movement.facingDirection);
                if(TimeRate(0.35f/enemyData.speedMove)) return;
                enemy.Paint(FrameCurrent);
                FrameCurrent = ( FrameCurrent + 1)%2; 
                break;
            case 2:
                break;

            case 4:
                if( Mathf.Abs(enemy.transform.position.x  - enemy.enemyPos.x ) > enemy.RangeMove ){
                    if(!CountDown(2)) {
                        
                    } else {
                        Movement?.Flip();        
                    }                 
                }
                if(TimeRate(0.35f/enemyData.speedMove)) return;
                if(Mathf.Abs(enemy.transform.position.y  - enemy.enemyPos.y) > 1.5 ){
                    enemy.dirY = enemy.enemyPos.y - enemy.transform.position.y;
                }else{
                    enemy.dirY = Random.Range(-1f,1f);
                }
                enemy.vY *= -1;

                Movement?.SetVelocity( enemyData.speedMove/3*Movement.facingDirection,  enemy.dirY - enemy.vY);
                Debug.Log(enemyData.speedMove/3*Movement.facingDirection);
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

}
