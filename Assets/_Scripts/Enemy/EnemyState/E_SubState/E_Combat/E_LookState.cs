using UnityEngine;

public class E_LookState : EnemyCombatState
{
    float minDistance;
    public E_LookState(Enemy enemy, FiniteStateMachine stateMachine) : base(enemy, stateMachine)
    {

    }
	public override void Enter() {        
		base.Enter();
        //minDistance = Random.Range( enemy.minAgroDistance -1.5f, enemy.minAgroDistance + 1 );
        movement.SetVelocityZero();
	}

	public override void Exit() {
		base.Exit();
		
	}
	public override void LogicUpdate() {
		base.LogicUpdate();
        if(isExitingState) return;

        movement.CheckIfShouldFlip(xDirTarget);

        movement.SetVelocityZero();

        if(XDisTarget > enemy.minAgroDistance + 1){                        
			stateMachine.ChangeState(enemy.chargeState);
		} 
        
        if(TimeRate(0.4f/enemyData.speedMove)) return;
        
        switch(enemyData.type){
            case 0:
				enemy.Paint(0);
				break;
            case 1:                             
            case 2:
            case 3:
            
                enemy.Paint(FrameCurrent);
                FrameCurrent = ( FrameCurrent + 1)%2;
                enemy.Paint(0);
                break;
            case 5: 
            case 4: 
            
                enemy.Paint(FrameCurrent);
                FrameCurrent = ( FrameCurrent + 1)%2;
                break;
        }                  
	}
}