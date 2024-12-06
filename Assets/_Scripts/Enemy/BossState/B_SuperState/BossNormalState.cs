using UnityEngine;

public class BossNormalState : BossState
{    
    protected float timeChangeState;
    public BossNormalState(Boss boss, FiniteStateMachine stateMachine) : base(boss, stateMachine)
    {

    }
	public override void Enter() {
		base.Enter();
	}
	public override void Exit() {
		base.Exit();
	}

	public override void LogicUpdate() {
		base.LogicUpdate();
		if(boss.playerCheck){
			if(Vector2.Distance(boss.playerCheck.position,boss.transform.position) < boss.AgroDistance)
			stateMachine.ChangeState(boss.lookState);
		}        
	}

	public override void PhysicsUpdate() {
		base.PhysicsUpdate();
	}
	public bool GetRandomBoolean()
    {
        int randomInt = Random.Range(0, 2);
        return randomInt == 0 ? false : true;
    }
}