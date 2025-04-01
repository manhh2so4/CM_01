using System.Collections;
using System.Collections.Generic; 
using UnityEngine;

public class PlayerAttackState : PlayerAbilityState
{
    private Weapon weapon;
    public PlayerAttackState(Player player,
                            FiniteStateMachine stateMachine,
                            PlayerData playerData,
                            mState state,
                            Weapon weapon)
    : base(player, stateMachine, playerData, state)
    {
        this.weapon = weapon;

        weapon.OnExit += ExitHandler;
    }
    public Weapon GetSkill(){
        return weapon;
    }
    
    public override void Enter(){
        base.Enter();
        weapon.Enter();
        movement.SetVelocityX(0);

    }

    public override void LogicUpdate(){
        base.LogicUpdate();
        if(isExitingState) return;

        movement.SetVelocityX(0);
        if(isGrounded ){  
            player.Anim.isFly = false;       
        }else{
            player.Anim.isFly = true; 
        }
    }
    public override void Exit(){
        base.Exit();
        if(isAbilityDone) return;
        weapon.Exit();
    }

    public void ExitHandler() => isAbilityDone = true;
}
