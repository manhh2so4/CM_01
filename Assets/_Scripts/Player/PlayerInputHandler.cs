using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    private Camera cam;
    //------------Move---------------
    public int MoveInput {get;private set;}
    public bool[] AttackInputs {get;private set;}
    private float inputHoldTime = 0.2f;

    //------------Jump---------------
    private float JumpInputStartTime;
    public bool jumpInput {get;private set;}
    public float amountJump {get;private set;}

    private float chargeTime = .3f;
    private bool isCharging = false;
    private float chargeStartTime;


    //------------Dashh---------------
    public Vector2 RawDashDirectionInput {get;private set;}
    public Vector2Int DashDirInput {get;private set;}
    public bool dashInputStop {get;private set;}
    public bool dashInput {get;private set;}
    private float dashInputStartTime;
    private Vector2 mousePosition;

    //---------------------------
    private void Start() {
        int Count = Enum.GetValues(typeof(CombatInput)).Length;
        AttackInputs = new bool[Count];
        cam = Camera.main;
    }
    private void Update() {
        CheckChargeJumpHoldTime();
        CheckDashInputHoldTime();
        
    }
    public void OnMoveInput(InputAction.CallbackContext context){
        MoveInput = (int)context.ReadValue<float>();
    }
    public void OnJumpInput(InputAction.CallbackContext context){
        if(context.started){
            isCharging = true;
            chargeStartTime = Time.time;
        }
        if(context.canceled){
            if( isCharging == false ) return; 
            isCharging = false;
            float chargeDuration = Time.time - chargeStartTime;
            amountJump = Mathf.Clamp01(chargeDuration / chargeTime);
            jumpInput = true;
        }
    }
    public void OnDashInput(InputAction.CallbackContext context){
        if(context.started){
            dashInput = true;
            dashInputStop = false;
            dashInputStartTime = Time.time;
        }
        if(context.canceled){
            dashInputStop = true;
        }
    }
    public void OnAttack1(InputAction.CallbackContext context){
        if(context.started){
            AttackInputs[(int)CombatInput.Attack1] = true;
        }
        if(context.canceled){
            AttackInputs[(int)CombatInput.Attack1] = false;
        }
    }
    public void OnAttack2(InputAction.CallbackContext context){
        if(context.started){
            AttackInputs[(int)CombatInput.Attack2] = true;
        }
        if(context.canceled){
            AttackInputs[(int)CombatInput.Attack2] = false;
        }
    }
    public void OnAttack3(InputAction.CallbackContext context){
        if(context.started){
            AttackInputs[(int)CombatInput.Attack3] = true;
        }
        if(context.canceled){
            AttackInputs[(int)CombatInput.Attack3] = false;
        }
    }
    public void OnDashDirectionInput(InputAction.CallbackContext context){
        //Debug.Log("Drass");
        mousePosition = context.ReadValue<Vector2>();
        if(dashInputStop) return;
        RawDashDirectionInput = context.ReadValue<Vector2>();
        if(true){
            RawDashDirectionInput = cam.ScreenToWorldPoint((Vector3)RawDashDirectionInput) - transform.position;
        }
        DashDirInput = Vector2Int.RoundToInt(RawDashDirectionInput.normalized);
    }
    public void OnLeftClick(InputAction.CallbackContext context){
        if(context.started)
        {
            DetecObbject();
        }
    }
    public void OnPress_Q (InputAction.CallbackContext context){
        if(context.started)
        {
            this.GameEvents().inputEvent.Click_Q();
        }
    }
    void DetecObbject(){
        //Debug.Log("Click");
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        RaycastHit2D[] hits2DAllNonAlloc = new RaycastHit2D[1];
        Physics2D.GetRayIntersectionNonAlloc(ray, hits2DAllNonAlloc);
        for (int i = 0; i < hits2DAllNonAlloc.Length; i++)
        {
            if(hits2DAllNonAlloc[i].collider != null){
                IClicker click = hits2DAllNonAlloc[i].collider.GetComponent<IClicker>();
                if(click != null) click.OnClick();
            }
        }
    }
    public void UseJumpInput() => jumpInput = false;
    public void UseDashInpur() => dashInput = false;

    private void CheckChargeJumpHoldTime(){
        if(isCharging == false) return;
        if( Time.time >= chargeStartTime + chargeTime){
            isCharging = false;
            jumpInput = true;
            amountJump = 1f;
        }
    }
    private void CheckDashInputHoldTime(){
        if(Time.time >= dashInputStartTime + inputHoldTime){
            dashInput = false;
        }
    }
}
public enum CombatInput{
    Attack1,
    Attack2,
    Attack3
}
