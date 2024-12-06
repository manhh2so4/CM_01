using UnityEngine;

public class B_ChargeState : BossCombatState
{
    float xMove;
    float yMove;
    public B_ChargeState(Boss boss, FiniteStateMachine stateMachine) : base(boss, stateMachine)
    {

    }
	public override void Enter() {
		base.Enter();
        boss.draw_Boss.state = StateEnemy.Moving;

	}

	public override void Exit() {
		base.Exit();
		
	}
	public override void LogicUpdate() {
		base.LogicUpdate();
		movement.CheckIfShouldFlip(xDirPlayer);


		if(distancePlayer < boss.minAgroDistance){
			boss.stateMachine.ChangeState(boss.lookState);
            return;
		}

		if(distancePlayer > 5){
			boss.stateMachine.ChangeState(boss.jump);
            return;
		}  

        if( (isWall || !isLedge ) && isGround ){
            return;              
        }
        if(isGround) movement?.SetVelocityX(bossData.speed_move * movement.facingDirection*2);
        
	}

	public override void PhysicsUpdate() {
		base.PhysicsUpdate();
	}
}