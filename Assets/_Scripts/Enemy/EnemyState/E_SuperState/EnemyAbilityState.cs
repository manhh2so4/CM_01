using UnityEngine;

public class EnemyAbilityState : E_State
{
    protected CharacterStats stats;
    protected bool isAbilityDone;
    public EnemyAbilityState(Enemy enemy, FiniteStateMachine stateMachine) : base(enemy, stateMachine)
    {
        stats = enemy.CharStats;
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
        if(isExitingState) return;

        if(isAbilityDone){          
            stateMachine.ChangeState(enemy.lookState);
        }
    }
}