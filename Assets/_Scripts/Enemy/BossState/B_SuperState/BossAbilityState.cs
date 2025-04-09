using UnityEngine;
[System.Serializable]
public class BossAbilityState : BossState
{
    protected bool isAbilityDone;
    protected HitBox hitBoxBody;
    protected HitBox hitBoxWeapon;
    public BossAbilityState(Boss boss, FiniteStateMachine stateMachine) : base(boss, stateMachine)
    {
        hitBoxBody = boss.hitBoxBody;
        hitBoxWeapon = boss.hitBoxWeapon;
    }

    public override void Enter(){
        base.Enter();
        isAbilityDone = false;
    }
    public override void Exit(){
        base.Exit();
        hitBoxWeapon.ClearObj();
        hitBoxBody.ClearObj();
        cooldowns.Start( boss.attack_1, bossData.speed_attack );
    }

    public override void LogicUpdate(){
        base.LogicUpdate();
        if(isExitingState) return;
        
        if(isAbilityDone){          
            stateMachine.ChangeState( boss.lookState );
            return;
        }
    }
    protected void ExitHandler() {
        isAbilityDone = true;
    }
    protected void TakeDamage(){
		hitBoxWeapon.OverLapObj();
	}
}