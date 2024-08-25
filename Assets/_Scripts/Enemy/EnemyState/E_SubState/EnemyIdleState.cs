using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : EnemyState
{
    private Movement Movement { get => movement ?? core.GetCoreComponent(ref movement); }
    private Movement movement;
    protected bool flipAfterIdle;
	protected bool isPlayerInMinAgroRange;
	protected float idleTime;
    public EnemyIdleState(Enemy enemy,FiniteStateMachine stateMachine,Enemy_SO enemyData) : base(enemy, stateMachine, enemyData) {
		
	}
    public override void DoChecks() {
		base.DoChecks();
		isPlayerInMinAgroRange = enemy.CheckPlayerInMaxAgroRange();
	}

	public override void Enter() {
		base.Enter();
		Movement?.SetVelocityX(0f);
		SetRandomIdleTime();

	}

	public override void Exit() {
		base.Exit();
		if (flipAfterIdle) {
			Movement?.Flip();
		}
		
	}

	public override void LogicUpdate() {
		base.LogicUpdate();
		Movement?.SetVelocityX(0f);

		if (Time.time >= startTime + idleTime) {
			stateMachine.changeStage(enemy.moveState);
		}
        
	}

	public override void PhysicsUpdate() {
		base.PhysicsUpdate();
	}
	private void SetRandomIdleTime() {
		idleTime = Random.Range(enemy.minIdleTime, enemy.maxIdleTime);
	}
	public void SetFlipAfterIdle(bool flip) {
		flipAfterIdle = flip;
	}
}
