using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : PlayerAbilityState
{  
    public bool canDash{get;private set;}
    bool isHolding;
    bool dashInputStop;
    public float cooldownDash;
    Vector2 dashDirectionInput;
    Vector2 dashDirection;
    public PlayerDashState(Player player, FiniteStateMachine stateMachine, PlayerData playerData, mState state) : base(player, stateMachine, playerData, state)
    {
        cooldownDash = playerData.dashCooldown;
    }
    public override void Enter(){
        base.Enter();
        canDash = false;
        player.inputPlayer.UseDashInpur();
        isHolding =true;
        Time.timeScale = playerData.holdTimeScale;
        startTime = Time.unscaledTime;
        player.dashDirImgae.gameObject.SetActive(true);

    }
    public override void Exit(){

        base.Exit();
        Time.timeScale =1f;
        player.dashDirImgae.gameObject.SetActive(false);
        cooldowns.Start(this,cooldownDash);

        if(movement.Velocity.y >0){
           movement.SetVelocityY(movement.Velocity.y*playerData.dashEndYMultiplier);
        }
        if(movement.Velocity.x >0){
           movement.SetVelocityY(movement.Velocity.x*playerData.dashEndYMultiplier);
        }

    }
    public override void LogicUpdate(){
        base.LogicUpdate();
        if(isExitingState) return;

        if(isHolding){
            dashDirectionInput = player.inputPlayer.DashDirInput;
            dashInputStop = player.inputPlayer.dashInputStop;
            if(dashDirectionInput != Vector2.zero){
                dashDirection = dashDirectionInput;
                dashDirection.Normalize();
            }else{
                dashDirection.Set( movement.facingDirection, 0);
            }
            float angle = Vector2.SignedAngle(Vector2.right,dashDirection);
            player.dashDirImgae.rotation = Quaternion.Euler(0f,0f,angle);

            if(dashInputStop || Time.unscaledTime >= startTime + playerData.maxHoldTime){
                isHolding = false;
                Time.timeScale =1f;
                startTime = Time.time;
                movement.CheckIfShouldFlip( Mathf.RoundToInt(dashDirection.x));
                movement.SetDrag( playerData.drag );
                player.dashDirImgae.gameObject.SetActive(false);
            }
        }else{
            movement.SetVelocity(playerData.dashVelocity,dashDirection);
            if(Time.time >= startTime + playerData.dashTime){
                movement.SetDrag(0);
                isAbilityDone = true;
            }
        }

    }
    public bool CheckIfCanDash(){
        return canDash && cooldowns.IsDone(this) ;
    }
    public void ResetCanDash() => canDash =true;
}

