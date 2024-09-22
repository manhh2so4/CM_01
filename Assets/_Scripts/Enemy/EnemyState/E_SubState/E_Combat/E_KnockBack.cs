using UnityEngine;

public class E_KnockBack : EnemyState
{
    public E_KnockBack(Enemy enemy, FiniteStateMachine stateMachine) : base(enemy, stateMachine){}
	public override void Enter() {
		base.Enter();
        enemy.state = StateEnemy.Knockback;
        switch(enemyData.type){
            case 0:
				enemy.Paint(0);
				break;
            case 1:
            case 4:                
                enemy.Paint(2);
                break;
        }
	}
	public override void Exit() {
		base.Exit();
		
	}
	public override void LogicUpdate() {
		base.LogicUpdate();
        if (enemy.knockBackReceiver.isKnockBackActive == false){
			stateMachine.ChangeState(enemy.lookState);
		}        
	}
}