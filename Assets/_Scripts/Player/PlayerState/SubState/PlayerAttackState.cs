using System.Collections;
using System.Collections.Generic; 
using UnityEngine;

public class PlayerAttackState : PlayerAbilityState
{
    private SKill skill;
    float mGravity;
    public PlayerAttackState(Player player, FiniteStateMachine stateMachine, PlayerData playerData, mState state, SKill _skill)
    : base(player, stateMachine, playerData, state)
    {
        this.skill = _skill;
        this.skill.OnExit += ExitHandler;
    }
    public SKill GetSkill(){
        return skill;
    }
    
    public override void Enter(){
        base.Enter();
        cooldowns.Start( this, skill.cooldown );
        player.paintChar.SetSkill( skill );
        skill.Enter();

        movement.SetVelocityX(0);
        mGravity = movement.GetPhysic2D().Gravity;
        movement.SetGravity(-5);

    }

    public override void LogicUpdate(){
        base.LogicUpdate();
        if(isExitingState) return;

        //movement.SetVelocityZero();
        if(isGrounded ){  
            player.paintChar.isFly = false;       
        }else{
            player.paintChar.isFly = true; 
        }
    }
    public override void Exit(){
        base.Exit();
        movement.SetGravity(mGravity);
        player.paintChar.ResetAnim();
        if(isAbilityDone) return;
        skill.Exit();
    }

    public void ExitHandler() => isAbilityDone = true;
}
