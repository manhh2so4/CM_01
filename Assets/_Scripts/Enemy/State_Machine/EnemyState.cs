using UnityEngine;

public class EnemyState 
{
    #region defaut 
    protected FiniteStateMachine stateMachine;
    protected Enemy enemy;
    protected Core core;
    protected Enemy_SO enemyData;
    protected Movement Movement { get => movement ?? core.GetCoreComponent(ref movement); }
    private Movement movement;

    protected float startTime;
    private float frameTimer = 0,timeCount = 0,timeAction = 0;
    protected int FrameCurrent = 0;
    protected bool isExitingState;
    protected float timeKnockBack; 

    #endregion
    //--------------------------------------- 
    #region DataEnemy 

    protected Vector3 enemyPos;
    protected float vY = 1,dirY;    
    protected bool isGround,isWall,isLedge;   

    protected float XDirPos;
    protected float YDirPos;
    #endregion
    
    
    public EnemyState(Enemy enemy,FiniteStateMachine stateMachine){
        this.enemy = enemy;
        this.stateMachine = stateMachine;
        this.enemyData = enemy.enemy_Data;
        core = enemy.Core;
        enemyPos = enemy.transform.position;
        timeKnockBack = enemy.knockBackReceiver.maxKnockbackTime;
    }
    public virtual void Enter(){
        isExitingState = false;
        startTime = Time.time;
        DoChecks();
    }
    public virtual void Exit(){
        isExitingState = true;
        frameTimer = 99f;
        timeCount = 0f;
    }
    public virtual void LogicUpdate(){
        isGround = enemy.isGround();
        isLedge = enemy.isledge();
		isWall = enemy.isWall();
        
        XDirPos = enemy.transform.position.x  - enemyPos.x;
        YDirPos = enemy.transform.position.y  - enemyPos.y;     
    }
    public virtual void PhysicsUpdate(){
        DoChecks();
    }
    public virtual void DoChecks()
    {

    }
    #region funcTimer
    protected bool TimeRate(float speed){        
        frameTimer += Time.deltaTime;
        if(frameTimer >= speed){
            frameTimer = 0;
            return false;
        }
        return true;
    }
    protected bool CountDown(float timeWait){        
        timeCount += Time.deltaTime;
        if(timeCount >= timeWait){
            timeCount = 0;
            return true;
        }
        return false;
    }
    protected bool TimeAction(float timeWait){        
        timeAction += Time.deltaTime;
        if(timeAction >= timeWait){
            timeAction = 0;
            return true;
        }
        return false;
    }
    #endregion
    
}
