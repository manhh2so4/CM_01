using UnityEngine;

public class B_DeadState : BossState
{
	ItemDrop itemDrop;
    public B_DeadState(Boss boss, FiniteStateMachine stateMachine) : base(boss, stateMachine)
    {
		itemDrop = core.GetCoreComponent<ItemDrop>();
    }
	public override void Enter() {
		base.Enter();
        boss.draw_Boss.state = StateEnemy.Jump;
		stateMachine.canChange = false;
		boss.CharStats.gameObject.SetActive(false);
        boss.knockBackReceiver.gameObject.SetActive(false);
        movement.SetDrag(0);
		movement.SetVelocity(-movement.facingDirection*5,10f);
        
		movement.IsColision(false);
		movement.CanSetVelocity = false;

	}
	public override void LogicUpdate() {
		base.LogicUpdate();
		if(isExitingState) return;

		if (Time.time >= startTime + 10f){
			stateMachine.canChange = true;
			stateMachine.ChangeState(boss.idleState);
			return;
		}
	}
	public override void Exit() {
		base.Exit();

		movement.CanSetVelocity = true;
		movement.IsColision(true);
        movement.SetDrag(10);
		movement.SetVelocityZero();

		boss.CharStats.gameObject.SetActive(true); 
        boss.knockBackReceiver.gameObject.SetActive(true);    
		boss.transform.position = StartPos;
		boss.CharStats.ResetMaxHealth(); 
		
	}
}