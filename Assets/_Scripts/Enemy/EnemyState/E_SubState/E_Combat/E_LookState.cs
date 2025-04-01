using UnityEngine;

public class E_LookState : EnemyCombatState
{
    public E_LookState(Enemy enemy, FiniteStateMachine stateMachine) : base(enemy, stateMachine)
    {

    }
	public override void Enter() {        
		base.Enter();
        movement.SetVelocityZero();
	}

	public override void Exit() {
		base.Exit();
		
	}
	public override void LogicUpdate() {
		base.LogicUpdate();
        if(isExitingState) return;

        movement.CheckIfShouldFlip(xDirPlayer);

        movement.SetVelocityZero();
        if(distancePlayer > enemy.minAgroDistance){                        
			stateMachine.ChangeState(enemy.chargeState);
		} 
        
        if(TimeRate(0.4f/enemyData.speedMove)) return;
        
        switch(enemyData.type){
            case 0:
				enemy.Paint(0);
				break;
            case 1:

                enemy.Paint(FrameCurrent);
                FrameCurrent = ( FrameCurrent + 1)%2;
                enemy.Paint(0);
                break;
            case 4:
            
                enemy.Paint(FrameCurrent);
                FrameCurrent = ( FrameCurrent + 1)%2;
                break;
        }                  
	}
}