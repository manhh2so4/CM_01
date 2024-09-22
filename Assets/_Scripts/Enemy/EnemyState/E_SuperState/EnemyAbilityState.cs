using UnityEngine;

public class EnemyAbilityState : EnemyState
{
    protected CharacterStats stats;
    protected bool isAbilityDone;
    public EnemyAbilityState(Enemy enemy, FiniteStateMachine stateMachine) : base(enemy, stateMachine)
    {
        stats = core.GetCoreComponent<CharacterStats>();
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
            stateMachine.ChangeState(enemy.lookState);
        }

    }
    public override void PhysicsUpdate(){
        base.PhysicsUpdate();

    }
}