using UnityEngine;

public class EnemyState 
{
    protected FiniteStateMachine stateMachine;
    protected Enemy enemy;
    protected Core core;
    protected Enemy_SO enemyData; 
    protected float startTime;
    private float frameTimer = 0,timeCount = 0,timeAction = 0;
    protected int FrameCurrent = 0; 
    public EnemyState(Enemy enemy,FiniteStateMachine stateMachine,Enemy_SO enemyData){
        this.enemy = enemy;
        this.stateMachine = stateMachine;
        this.enemyData = enemyData;
        core = enemy.Core;
    }
    public virtual void Enter(){
        Debug.Log("Enter :" + this.GetType().Name);
        startTime = Time.time;
        DoChecks();
    }
    public virtual void Exit(){
        frameTimer = 99f;
        timeCount = 0f;
    }
    public virtual void LogicUpdate(){
        
    }
    public virtual void PhysicsUpdate(){
        DoChecks();
    }
    public virtual void DoChecks()
    {

    }
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

}
