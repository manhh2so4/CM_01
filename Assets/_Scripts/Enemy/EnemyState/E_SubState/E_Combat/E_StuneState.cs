using UnityEngine;

public class E_StuneState : EnemyState
{
    public E_StuneState(Enemy enemy, FiniteStateMachine stateMachine) : base(enemy, stateMachine)
    {

    }
	public override void Enter() {
		base.Enter();
		
		enemy.state = StateEnemy.Stun;
		movement.SetVelocityZero();

		switch(enemyData.type){
            case 0:
				enemy.Paint(0);
				break;
            case 1:
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
		movement.SetVelocityZero();
		if( enemy.poiseReceiver.IsPoise() == false ){
			stateMachine.ChangeState(enemy.lookState);
		}        
	}
}