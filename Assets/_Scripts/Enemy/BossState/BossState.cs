using UnityEngine;

public class BossState : EnemyState
{
    #region defaut 
    protected Boss boss;
    protected EnemyBoss_SO bossData;
    protected Cooldown cooldowns;
    protected int xDirPlayer;
	protected int yDirPlayer;

    protected float xDisPlayer;
    protected float distancePlayer;

    #endregion
    //--------------------------------------- 
    #region DataEnemy 
    protected Vector3 StartPos;
    
    #endregion
    public BossState(Boss boss,FiniteStateMachine stateMachine){
        this.boss = boss;
        this.stateMachine = stateMachine;
        this.cooldowns = boss.cooldowns;
        this.bossData = boss.bossData;
        core = boss.core;
        movement = boss.movement;
        StartPos = boss.transform.position;
    }
    public override void Enter(){
        base.Enter();
        startTime = Time.time;
        boss.CurentState = this.GetType().Name;
        CheckPlayer();
    }
    public override void Exit(){
        base.Exit();
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        isGround = movement.isGround();
        isLedge = boss.isledge();
        isWall = movement.isWall();

        XDisPos = boss.transform.position.x - StartPos.x;
        YDisPos = boss.transform.position.y - StartPos.y;
        CheckPlayer();
    }

    private void CheckPlayer()
    {
        if (boss.playerCheck != null)
        {
            distancePlayer = Vector2.Distance(boss.playerCheck.position, boss.transform.position);
            xDisPlayer = boss.playerCheck.position.x - boss.transform.position.x;
            xDirPlayer = (boss.playerCheck.position.x > boss.transform.position.x) ? 1 : -1;
            yDirPlayer = (boss.playerCheck.position.y > boss.transform.position.y) ? 1 : -1;

        }
    }

    public override void PhysicsUpdate(){
        base.PhysicsUpdate();
    }

}
