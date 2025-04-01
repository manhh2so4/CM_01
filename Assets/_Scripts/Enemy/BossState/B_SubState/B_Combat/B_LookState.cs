using UnityEngine;

public class B_LookState : BossCombatState
{
	int attackCount = 2;
	
    public B_LookState( Boss boss, FiniteStateMachine stateMachine) : base( boss, stateMachine)
    {

    }
	public override void Enter() {        
		base.Enter();
        boss.draw_Boss.state = StateEnemy.Idle;	
        movement.SetVelocityZero();
	}

	
	public override void LogicUpdate() {
		base.LogicUpdate();
		if(isExitingState) return;

        movement.CheckIfShouldFlip(xDirPlayer);

		if( cooldowns.IsDone( boss.attack_1 ) ){

			if( ChageAttack()) return;

        }
		if( TimeAction(7f) ){

			State ability = boss.GetAbility();
			if( ability != null ) {
				boss.stateMachine.ChangeState( ability );
            	return;
			}
			
		}

        if( distancePlayer > boss.minAgroDistance ){                        
			stateMachine.ChangeState(boss.chargeState);
            return;
		}                
	}
	public override void Exit() {
		base.Exit();
		
	}
	bool ChageAttack(){
		int attackID = Random.Range(0, attackCount);
		switch (attackID){
			case 0:
				stateMachine.ChangeState(boss.attack_1);
			return true;
			

			case 1:
				stateMachine.ChangeState(boss.attack_2);
			return true;
			

			default:
			return false;
			
		}
	}

}