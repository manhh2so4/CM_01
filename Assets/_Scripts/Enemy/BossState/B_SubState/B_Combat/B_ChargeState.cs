using UnityEngine;

public class B_ChargeState : BossCombatState
{
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
		if(isExitingState) return;

		movement.CheckIfShouldFlip(xDirPlayer);

		if(distancePlayer < boss.minAgroDistance){
			boss.stateMachine.ChangeState(boss.lookState);
            return;
		}

		
		if( distancePlayer > 6 ){
			State ability = boss.GetAbility();
			if( ability != null ) {
				boss.stateMachine.ChangeState( ability );
            	return;
			}
		}
		if( distancePlayer < 8f){
			if( cooldowns.IsDone( boss.rage ) ){
				boss.stateMachine.ChangeState( boss.rage );
				return;
			}
		}

        if( ( isWall || !isLedge ) && isGround ){
			boss.stateMachine.ChangeState( boss.jump );
            return;              
        }

        if(isGround) movement?.SetVelocityX(bossData.speed_move * movement.facingDirection*2);
        
	}
}