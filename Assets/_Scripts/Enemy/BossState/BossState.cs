using UnityEngine;

public class BossState : EnemyState
{
    #region defaut 
    protected Boss boss;
    protected EnemyBoss_SO bossData;
    protected int xDirPlayer;
	protected int yDirPlayer;

    protected float xDisPlayer;
    protected float distancePlayer;

    #endregion
    //--------------------------------------- 
    #region DataEnemy 
    protected Vector3 BossPos;
    
    #endregion
    public BossState(Boss boss,FiniteStateMachine stateMachine){
        this.boss = boss;
        this.stateMachine = stateMachine;
        this.bossData = boss.bossData;
        core = boss.core;
        movement = core.GetCoreComponent<Movement>();
        BossPos = boss.transform.position;
    }
    public override void Enter(){
        base.Enter();
        startTime = Time.time;
    }
    public override void Exit(){
        base.Exit();
    }
    public override void LogicUpdate(){
        base.LogicUpdate();
        isGround = movement.isGround();
        isLedge = boss.isledge();
		isWall = movement.isWall();

        XDirPos = boss.transform.position.x  - BossPos.x;
        YDirPos = boss.transform.position.y  - BossPos.y;

        distancePlayer = Vector2.Distance(boss.playerCheck.position,boss.transform.position);
        xDisPlayer = boss.playerCheck.position.x - boss.transform.position.x;

		xDirPlayer = (boss.playerCheck.position.x > boss.transform.position.x) ? 1 : -1;
		yDirPlayer = (boss.playerCheck.position.y > boss.transform.position.y) ? 1 : -1;     
    }
    public override void PhysicsUpdate(){
        base.PhysicsUpdate();
    }

}
