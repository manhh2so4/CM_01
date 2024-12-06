using UnityEngine;

public class B_Attack_2 : BossAbilityState
{
    public B_Attack_2(Boss boss, FiniteStateMachine stateMachine) : base(boss, stateMachine)
    {

    }
	public override void Enter() {
		base.Enter();
		Debug.Log("set_attack1");
		boss.draw_Boss.state = StateEnemy.Skill_2;
		boss.draw_Boss.OnAttackDone += ExitHandler;
		movement.SetVelocityZero();

	}

	public override void Exit() {
		base.Exit();
		boss.draw_Boss.OnAttackDone -= ExitHandler;
		
	}
	public override void LogicUpdate(){
		base.LogicUpdate();
	}

	public void ExitHandler()
    {
        isAbilityDone = true;
    }
}