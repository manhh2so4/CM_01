using UnityEngine;

public class BossAbilityState : BossState
{
    protected CharacterStats stats;
    protected bool isAbilityDone;
    public BossAbilityState(Boss boss, FiniteStateMachine stateMachine) : base(boss, stateMachine)
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
            stateMachine.ChangeState(boss.lookState);
            return;
        }
    }
    public override void PhysicsUpdate(){
        base.PhysicsUpdate();

    }
}