using UnityEngine;

public class E_LookState : EnemyCombatState
{
    public E_LookState(Enemy enemy, FiniteStateMachine stateMachine) : base(enemy, stateMachine)
    {

    }

    public override void DoChecks() {
		base.DoChecks();
		
	}

	public override void Enter() {        
		base.Enter();
        enemy.state = StateEnemy.Look;
        Movement.SetVelocityZero();
        
	}

	public override void Exit() {
		base.Exit();
		
	}

	public override void LogicUpdate() {
		base.LogicUpdate();

        Movement.CheckIfShouldFlip(xDirPlayer);
        if(canAttack) stateMachine.changeStage(enemy.attackState);  

        if(distancePlayer > enemy.minAgroDistance){                        
			stateMachine.changeStage(enemy.chargeState);
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

	public override void PhysicsUpdate() {
		base.PhysicsUpdate();
	}
}