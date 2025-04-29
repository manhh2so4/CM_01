using UnityEngine;

public class E_StuneState : EnemyAbilityState
{
    public E_StuneState(Enemy enemy, FiniteStateMachine stateMachine) : base(enemy, stateMachine)
    {

    }
	public override void Enter() {
		base.Enter();

		movement.SetVelocityZero();
		switch(enemyData.type){
            case 0:
			case 1:                             
            case 2:
            case 3:
            case 5: 
            case 4:                
                enemy.Paint(0);
                break;
        }
	}
	public override void Exit() {
		base.Exit();
	}
	public override void LogicUpdate() {
		base.LogicUpdate(); 
		if(isExitingState) return;

		if(enemy.poiseReceiver.IsPoise() == false){
			isAbilityDone = true;
		}

	}
}