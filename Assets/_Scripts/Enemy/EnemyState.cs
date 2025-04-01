using UnityEngine;

public class EnemyState : State
{
    #region defaut 

    protected FiniteStateMachine stateMachine;
    protected float startTime;
    
    #endregion
    //--------------------------------------- 
    #region DataEnemy 
    protected float vY = 1,dirY;    
    protected bool isGround,isWall,isLedge;   

    protected float XDisPos;
    protected float YDisPos;
    #endregion
    public override void Enter(){
        base.Enter();
        startTime = Time.time;
        
    }
    public override void Exit(){
        base.Exit();
    }
    public override void LogicUpdate(){
        base.LogicUpdate();

    }
    public override void PhysicsUpdate(){
        base.PhysicsUpdate();
       
    }

}
