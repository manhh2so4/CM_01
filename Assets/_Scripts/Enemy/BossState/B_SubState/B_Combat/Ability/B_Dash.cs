using UnityEngine;
[System.Serializable]
public class B_Dash : BossAbilityState
{
	float dashForce = 20;
    float holdTime = .5f;

	float dashTime = 0.35f;
	float startDash;
	bool isDash,finishDash;

    public B_Dash(Boss boss, FiniteStateMachine stateMachine) : base(boss, stateMachine)
    {

    }
	public override void Enter() {
		base.Enter();
		isDash = false;
		boss.draw_Boss.state = StateEnemy.Hold;
		startDash = startTime + holdTime;
		movement.SetVelocityZero();

		hitBoxBody.ClearObj();
		hitBoxWeapon.ClearObj();

	}

	private void StartDash()
    {
        boss.draw_Boss.state = StateEnemy.Dash;
		movement.CheckIfShouldFlip(xDirPlayer);
        movement.SetVelocityX( dashForce * movement.facingDirection );
		isDash = true;
		finishDash = false;
    }

	public override void LogicUpdate(){
		base.LogicUpdate();
		if(isExitingState) return;
		
		if(isDash && !finishDash) {
			hitBoxBody.OverLapObj();
		}

		if( ( Time.time > startTime + holdTime ) && (isDash == false)){
            StartDash();
            return;
        }

		if( (isDash &&  movement.isVXzero()) || isDashOverTime() ){
			if( finishDash ) {
				return;
			}
			movement.SetDrag(50);
			boss.draw_Boss.OnAttackDone += ExitHandler;
			boss.draw_Boss.OnTakeDamage += TakeDamage;
		 	boss.draw_Boss.state = StateEnemy.Attack2;
			finishDash = true;
		}
	}
	public bool isDashOverTime(){
		return Time.time > startDash + dashTime ;
	}
	public override void Exit() {
		base.Exit();
		movement.SetDrag(0);
		cooldowns.Start( boss.dash, 10 );
		boss.draw_Boss.OnAttackDone -= ExitHandler;
		boss.draw_Boss.OnTakeDamage -= TakeDamage;
	}
	
}