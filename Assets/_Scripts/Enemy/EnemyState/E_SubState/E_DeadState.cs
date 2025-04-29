using UnityEngine;

public class E_DeadState : E_State
{
	ItemDrop itemDrop;
    public E_DeadState(Enemy enemy, FiniteStateMachine stateMachine) : base(enemy, stateMachine)
    {
		itemDrop = core.GetCoreComponent<ItemDrop>();
    }
	public override void Enter(){
		base.Enter();
		stateMachine.canChange = false;
		movement.IsColision(false);
		movement.SetVelocity( -movement.facingDirection*3, 7f );
      	enemy.mPhysic2D.Gravity = (2* -9.8f);
		enemy.CharStats.gameObject.SetActive(false);
		enemy.knockBackReceiver.gameObject.SetActive(false);

		movement.CanSetVelocity = false;
		itemDrop.GenerateDrop();
		
		enemy.Paint(2);
	}
	public override void LogicUpdate() {
		base.LogicUpdate();
		if(isExitingState) return;
		if(YDirPos > 30f){
			movement.CanSetVelocity = true;
			movement.SetVelocityZero();
		}
		if (Time.time >= startTime + enemyData.timeReSpont){
			stateMachine.canChange = true;
			stateMachine.ChangeState(enemy.idleState);
			return;
		}
	}
	public override void Exit() {
		base.Exit();

		movement.CanSetVelocity = true;
		movement.IsColision(true);
		movement.SetVelocityZero();

		enemy.CharStats.gameObject.SetActive(true);  
		enemy.knockBackReceiver.gameObject.SetActive(true);
		enemy.transform.position = enemyPos;
		enemy.CharStats.ResetMaxHealth(); 

		if(enemyData.type == 4 || enemyData.type == 5){
            enemy.mPhysic2D.Gravity = (0);
        }else{
			enemy.mPhysic2D.Gravity = (-9.8f *2f);
		}
		enemy.gameObject.SetActive(false);
	}
}