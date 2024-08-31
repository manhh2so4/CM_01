using UnityEngine;

public class E_ChargeState : EnemyCombatState
{
    float xMove;
    float yMove;
    public E_ChargeState(Enemy enemy, FiniteStateMachine stateMachine) : base(enemy, stateMachine)
    {
    }
    public override void DoChecks() {
		base.DoChecks();
		
	}
	public override void Enter() {
		base.Enter();
        enemy.state = StateEnemy.Charge;

	}

	public override void Exit() {
		base.Exit();
		
	}
	public override void LogicUpdate() {
		base.LogicUpdate();
		Movement.CheckIfShouldFlip(xDirPlayer);

        if(canAttack) stateMachine.changeStage(enemy.attackState);

		if(distancePlayer < enemy.minAgroDistance){
			enemy.stateMachine.changeStage(enemy.lookState);
		}       

        if(TimeRate(0.3f/enemyData.speedMove)) return; 
		switch(enemyData.type){			
			case 0:
				enemy.Paint(0);
				break;
            case 1:                                
                enemy.Paint(FrameCurrent);
                FrameCurrent = ( FrameCurrent + 1)%2;

                if( (isWall || !isLedge ) && isGround ){
                    return;              
                }

                if(isGround) Movement?.SetVelocityX(enemyData.speedMove * Movement.facingDirection);
                break;
            case 2:
                break;
            case 4:  
                dirY = yDirPlayer;
                vY *= -1;
                enemy.Paint(FrameCurrent);
                FrameCurrent = ( FrameCurrent + 1)%2;
                xMove = 0;
                yMove = 0;
                if(Mathf.Abs( YDirPos ) > 2 ){
                    yMove = enemyPos.y - enemy.transform.position.y;
                }
                if( Mathf.Abs( XDirPos ) > enemy.RangeMove ){
					xMove = enemyPos.x - enemy.transform.position.x;
                }
                Movement?.SetVelocity( enemyData.speedMove/3 * Movement.facingDirection + xMove,  dirY - vY + yMove);
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