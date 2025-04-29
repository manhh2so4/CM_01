using UnityEngine;

public class E_ChargeState : EnemyCombatState
{
    float xMove;
    float yMove;
    float minDistance;
    public E_ChargeState(Enemy enemy, FiniteStateMachine stateMachine) : base(enemy, stateMachine)
    {
    }

	public override void Enter() {
		base.Enter();
        minDistance = Random.Range( enemy.minAgroDistance -1.5f, enemy.minAgroDistance + 1 );
	}
	public override void Exit() {
		base.Exit();
	}
	public override void LogicUpdate() {

		base.LogicUpdate();
        if(isExitingState) return;

		movement.CheckIfShouldFlip(xDirTarget);

		if( XDisTarget < minDistance ){
            movement.SetVelocityZero();
            return;
			//enemy.stateMachine.ChangeState(enemy.lookState);
		}

        if( Mathf.Abs(XDirPos) > enemy.RangeMove ) {
            movement.SetVelocityZero();
            return;
        }      

        if(TimeRate(0.3f/enemyData.speedMove)) return; 
		switch(enemyData.type){			
			case 0:
				enemy.Paint(0);
				break;
            case 1:                             
            case 3:                              
                enemy.Paint(FrameCurrent);
                FrameCurrent = ( FrameCurrent + 1)%2;

                if( (isWall || !isLedge ) && isGround ){
                    return;              
                }
                if(isGround) movement?.SetVelocityX(enemyData.speedMove * movement.facingDirection);
                break;

            case 2:            
                if( (isWall || !isLedge ) && isGround) movement.Flip();
                    if(TimeRate(0.35f/enemyData.speedMove)) return;
                        enemy.Paint(FrameCurrent);

                        if( Mathf.Abs(XDirPos) > enemy.RangeMove ) movement.CheckIfShouldFlip( XDirPos > 0 ? -1 : 1 );

                        if( isGround && (FrameCurrent == 1) ) movement?.SetVelocityX(enemyData.speedMove * movement.facingDirection);

                        FrameCurrent = ( FrameCurrent + 1)%2;
                break;

            case 5:
            case 4:   
                dirY = yDirTarget;
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

                movement?.SetVelocity( enemyData.speedMove/3 * movement.facingDirection + xMove,  dirY - vY + yMove);
                break;

            default:
                enemy.Paint(0);
				break;           
        }
        
	}
}