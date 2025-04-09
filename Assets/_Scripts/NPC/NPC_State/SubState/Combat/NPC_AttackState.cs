using System.Collections;
using System.Collections.Generic; 
using UnityEngine;

public class NPC_AttackState : NPCAbillityState
{
    private SKill skill;
    public float rangeAttack;
    float mGravity;
    public NPC_AttackState(NPC npc, FiniteStateMachine stateMachine, SKill _skill) : base(npc, stateMachine)
    {
        this.skill = _skill;
        this.skill.OnExit += ExitHandler;
        this.skill.GetComponent< WeaponGenerator>().GenerateWeapon( npcData.skill_1);
        rangeAttack = this.skill.rangeAttack;
    }
    public SKill GetSkill(){
        return skill;
    }
    
    public override void Enter(){
        base.Enter();
        paintChar.state = mState.Attack;

        cooldowns.Start( this, skill.cooldown );
        paintChar.SetSkill( skill );
        
        skill.Enter();

        movement.SetVelocityX(0);
        mGravity = movement.GetPhysic2D().Gravity;
        movement.SetGravity(-5);

    }
    
    public override void LogicUpdate(){
        base.LogicUpdate();
        if(isExitingState) return;

        if(isGrounded ){  
            paintChar.isFly = false;       
        }else{
            paintChar.isFly = true; 
        }
    }

    public override void Exit(){
        base.Exit();
        movement.SetGravity(mGravity);

        paintChar.ResetAnim();

        if(isAbilityDone) return;
        skill.Exit();
    }
}
