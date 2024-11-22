using UnityEngine;

public class E_DeadState : E_State
{
	ItemDrop itemDrop;
    public E_DeadState(Enemy enemy, FiniteStateMachine stateMachine) : base(enemy, stateMachine)
    {
		itemDrop = core.GetCoreComponent<ItemDrop>();
    }
	public override void Enter() {
		base.Enter();
		enemy.state = StateEnemy.Dead;
		stateMachine.canChange = false;
		enemy.mCollider.enabled = false;
        enemy.mRB.gravityScale = 2;

		movement.SetVelocity(-movement.facingDirection,10f);
		movement.CanSetVelocity = false;
		itemDrop.GenerateDrop();
		Debug.Log("Drop" + itemDrop.transform.parent.parent.name);
		enemy.Paint(2);
	}
	public override void LogicUpdate() {
		if (Time.time >= startTime + 5f){
			stateMachine.canChange = true;
			stateMachine.ChangeState(enemy.idleState);
		}
	}
	public override void Exit() {
		base.Exit();
		movement.CanSetVelocity = true;
		movement.SetVelocityZero();
        enemy.mCollider.enabled = true;       

		enemy.transform.position = enemyPos;
		enemy.CharStats.ResetMaxHealth(); 

		if(enemyData.type == 4 || enemyData.type == 5){
            enemy.mRB.gravityScale = 0;
        }
	}
}