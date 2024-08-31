using UnityEngine;

public class EnemyAbilityState : EnemyState
{
    protected bool isAbilityDone;
    public EnemyAbilityState(Enemy enemy, FiniteStateMachine stateMachine) : base(enemy, stateMachine)
    {

    }
    public override void DoChecks(){
   
    }
    public override void Enter(){
        base.Enter();
        isAbilityDone = false;
    }
    public override void Exit(){
        base.Exit();
    }
    public override void LogicUpdate(){
        base.LogicUpdate();
        if(isAbilityDone){          
            stateMachine.changeStage(enemy.lookState);
        }

    }
    public override void PhysicsUpdate(){
        base.PhysicsUpdate();

    }
}