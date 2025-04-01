using UnityEngine;
[System.Serializable]
public class B_Jump : BossAbilityState
{
	public float jumpForce = 20.0f;
    public float buildupTime = .5f;
    bool isjump,finishJunp;
    public B_Jump(Boss boss, FiniteStateMachine stateMachine) : base(boss, stateMachine)
    {

    }

	public override void Enter() {
		base.Enter();
        isjump = finishJunp = false;
        boss.draw_Boss.state = StateEnemy.Hold;
        movement.SetVelocityZero();
	}
	private void StartJump()
    {

        boss.draw_Boss.state = StateEnemy.Jump;  
        movement.CheckIfShouldFlip(xDirPlayer);
        movement.SetVelocityY(jumpForce);
        isjump = true;

    }

	
	public override void LogicUpdate(){
		base.LogicUpdate();
        if(isExitingState) return;
        
        if( ( Time.time > startTime + buildupTime ) && (isjump == false)){
            StartJump();
            return;
        }
        if(isjump && !isGround){

            float horizontalForce = Mathf.Clamp(xDisPlayer, -15 , 15 );
            movement.SetVelocityX( horizontalForce * 0.85f);

        }
        
		if( movement.Velocity.y < -0.01 && isjump){
            boss.draw_Boss.state = StateEnemy.Fall;
            movement.CheckIfShouldFlip(xDirPlayer);
        }

        if(isGround && isjump && (finishJunp == false) ){
            movement.SetVelocityZero();
            finishJunp = true;
            
            boss.draw_Boss.OnAttackDone += ExitHandler;
			boss.draw_Boss.OnTakeDamage += TakeDamage;
		 	boss.draw_Boss.state = StateEnemy.Attack1;
        }
	}

    public override void Exit() {
		base.Exit();
        cooldowns.Start( boss.jump, 2 );
        boss.draw_Boss.OnAttackDone -= ExitHandler;
		boss.draw_Boss.OnTakeDamage -= TakeDamage;
	}
}