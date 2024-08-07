using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : PlayerAbilityState
{  
    public bool canDash{get;private set;}
    bool isHolding;
    bool dashInputStop;
    Vector2 dashDirection;
    Vector2 dashDirectionInput;
    private float lastDashTime;
    
    public PlayerDashState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, mState state) : base(player, stateMachine, playerData, state)
    {

    }
    public override void Enter(){
        base.Enter();
        canDash = false;
        player.inputPlayer.UseDashInpur();
        isHolding =true;
        dashDirection = Vector2.right*core.Movement.facingDirection;
        Time.timeScale = playerData.holdTimeScale;
        startTime = Time.unscaledTime;
        player.dashDirImgae.gameObject.SetActive(true);
    }
    public override void Exit(){
        base.Exit();
        if(core.Movement.CurrentVelocity.y >0){
           core.Movement.SetVelocityY(core.Movement.CurrentVelocity.y*playerData.dashEndYMultiplier);
        }
        if(core.Movement.CurrentVelocity.x >0){
           core.Movement.SetVelocityY(core.Movement.CurrentVelocity.x*playerData.dashEndYMultiplier);
        }
    }
    public override void LogicUpdate(){
        base.LogicUpdate();
        if(!isExitingState){
            if(isHolding){
                dashDirectionInput = player.inputPlayer.DashDirInput;
                dashInputStop = player.inputPlayer.dashInputStop;
                if(dashDirectionInput != Vector2.zero){
                    dashDirection = dashDirectionInput;
                    dashDirection.Normalize();
                }
                float angle = Vector2.SignedAngle(Vector2.right,dashDirection);
                player.dashDirImgae.rotation = Quaternion.Euler(0f,0f,angle);
                if(dashInputStop || Time.unscaledTime >= startTime + playerData.maxHoldTime){
                    isHolding = false;
                    Time.timeScale =1f;
                    startTime = Time.time;
                    core.Movement.CheckIfShouldFlip(Mathf.RoundToInt(dashDirection.x));
                    core.Movement.mRB.drag = playerData.drag;
                    core.Movement.SetVelocity(playerData.dashVelocity,dashDirection);
                    player.dashDirImgae.gameObject.SetActive(false);
                }
            }else{
                core.Movement.SetVelocity(playerData.dashVelocity,dashDirection);
                if(Time.time >= startTime + playerData.dashTime){
                    core.Movement.mRB.drag = 0f;
                    isAbilityDone = true;
                    lastDashTime = Time.time;
                }
            }

        }
    }
    public bool CheckIfCanDash(){
        return canDash && Time.time >= lastDashTime + playerData.dashCooldown;
    }
    public void ResetCanDash() => canDash =true;
}
