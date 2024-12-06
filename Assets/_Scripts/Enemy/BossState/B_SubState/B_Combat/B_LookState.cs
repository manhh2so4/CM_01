using UnityEngine;

public class B_LookState : BossCombatState
{
    public B_LookState(Boss boss, FiniteStateMachine stateMachine) : base(boss, stateMachine)
    {

    }
	public override void Enter() {        
		base.Enter();
        boss.draw_Boss.state = StateEnemy.Idle;	
        movement.SetVelocityZero();
	}

	public override void Exit() {
		base.Exit();
		
	}
	public override void LogicUpdate() {
		base.LogicUpdate();

        movement.CheckIfShouldFlip(xDirPlayer);
		
		if(canAttack) {
            stateMachine.ChangeState(boss.attack_2);
			Debug.Log("set_attack1");  
            return;
        }

        if(distancePlayer > boss.minAgroDistance){                        
			stateMachine.ChangeState(boss.chargeState);
            return;
		}                
	}

	public override void PhysicsUpdate() {
		base.PhysicsUpdate();
	}
}