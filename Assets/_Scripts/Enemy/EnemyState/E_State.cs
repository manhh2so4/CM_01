using UnityEngine;

public class E_State : State
{
    #region defaut 
    protected Enemy enemy;
    protected Enemy_SO enemyData;
    protected FiniteStateMachine stateMachine;
    protected float startTime;
    private float frameTimer = 0,timeCount = 0,timeAction = 0;
    protected int FrameCurrent = 0;
    #endregion
    //--------------------------------------- 
    #region DataEnemy 

    protected Vector3 enemyPos;
    protected float vY = 1,dirY;    
    protected bool isGround,isWall,isLedge;   

    protected float XDirPos;
    protected float YDirPos;
    #endregion
    
    public E_State(Enemy enemy,FiniteStateMachine stateMachine){
        this.enemy = enemy;
        this.stateMachine = stateMachine;
        this.enemyData = enemy.enemy_Data;
        core = enemy.core;
        movement = core.GetCoreComponent<Movement>();
        enemyPos = enemy.transform.position;
    }
    public override void Enter(){
        base.Enter();
        startTime = Time.time;
    }
    public override void Exit(){
        base.Exit();
        frameTimer = 99f;
    }
    public override void LogicUpdate(){
        base.LogicUpdate();
        isGround = enemy.isGround();
        isLedge = enemy.isledge();
		isWall = enemy.isWall();
        
        XDirPos = enemy.transform.position.x  - enemyPos.x;
        YDirPos = enemy.transform.position.y  - enemyPos.y;     
    }
    public override void PhysicsUpdate(){
        base.PhysicsUpdate();
       
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
