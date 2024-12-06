using UnityEngine;
using DG.Tweening;
public class B_Jump : BossAbilityState
{
	public float jumpForce = 20.0f;
    public float buildupTime = .5f;
    bool isjump;
	private Tween buildupTween;
    public B_Jump(Boss boss, FiniteStateMachine stateMachine) : base(boss, stateMachine)
    {

    }

	public override void Enter() {
		base.Enter();
        isjump = false;
		buildupTween = DOVirtual.DelayedCall(buildupTime, StartJump, false);
		
	}
	private void StartJump()
    {
        boss.draw_Boss.state = StateEnemy.Jump;  
        float horizontalForce = Mathf.Clamp(xDisPlayer, -6,6);

        movement.CheckIfShouldFlip(xDirPlayer);
        movement.AddForce(new Vector2( horizontalForce*0.85f , jumpForce));

    }

	public override void Exit() {
		base.Exit();
		
		
	}
	public override void LogicUpdate(){
		base.LogicUpdate();
		if(movement.CurrentVelocity.y < -0.01){
                boss.draw_Boss.state = StateEnemy.Fall;
                isjump = true;
                movement.CheckIfShouldFlip(xDirPlayer);
        }
        if(isGround && isjump ){
                movement.SetVelocityZero();
				isAbilityDone = true;
        }
	}
}