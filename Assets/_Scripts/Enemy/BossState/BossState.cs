using UnityEngine;

public class BossState : EnemyState
{
    #region defaut 
    protected Boss boss;
    protected EnemyBoss_SO bossData;

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
        isGround = boss.isGround();
        isLedge = boss.isledge();
		isWall = boss.isWall();

        XDirPos = boss.transform.position.x  - BossPos.x;
        YDirPos = boss.transform.position.y  - BossPos.y;     
    }
    public override void PhysicsUpdate(){
        base.PhysicsUpdate();
    }

}
