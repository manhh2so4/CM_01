using UnityEngine;

public class E_State : State
{
    #region defaut 
    protected Enemy enemy;
    protected Enemy_SO enemyData;
    protected Cooldown cooldowns;
    protected FiniteStateMachine stateMachine;
    protected float startTime;
    private float frameTimer = 0;
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
        this.cooldowns = enemy.cooldowns;
        core = enemy.core;
        movement = core.GetCoreComponent<Movement>();
        enemyPos = enemy.transform.position;
    }
    public override void Enter(){
        base.Enter();
        startTime = Time.time;
        enemy.CurentState = this.GetType().Name;
    }
    public override void Exit(){
        base.Exit();
        frameTimer = 99f;
    }
    public override void LogicUpdate(){
        base.LogicUpdate();
        isGround = movement.isGround();
        isWall = movement.isWall();
        isLedge = enemy.isledge();
        
        XDirPos = enemy.transform.position.x  - enemyPos.x;
        YDirPos = enemy.transform.position.y  - enemyPos.y;     
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
    
    #endregion
    
}
