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
    
    public override void Enter(){
        base.Enter();
        weapon.Enter();
        movement.SetVelocityX(0);
    }

    public override void LogicUpdate(){
        base.LogicUpdate();
        movement.SetVelocityX(0);
        if(isGrounded ){  
            player.Anim.isFly = false;       
        }else{
            player.Anim.isFly = true; 
        }
    }

    public void ExitHandler()
    {
        isAbilityDone = true;
    }
}
