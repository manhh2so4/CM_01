using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.InputSystem;

public class inputPlayer : MonoBehaviour
{
    private PlayerInput playerInput;
    private Camera cam;
    public Vector2 RawDashDirectionInput {get;private set;}
    [SerializeField] public Vector2Int DashDirInput {get;private set;}
    public int MoveInput {get;private set;}
    public bool jumpInput {get;private set;}
    public bool dashInput {get;private set;}
    public bool dashInputStop {get;private set;}
    public bool[] AttackInputs {get;private set;}
    [SerializeField]
    private float inputHoldTime = 0.2f;
    private float JumpInputStartTime;
    private float dashInputStartTime;
    public Vector3 direction ;
    private Vector2 mousePosition;
    private void Start() {
        playerInput = GetComponent<PlayerInput>();
        int Count = Enum.GetValues(typeof(CombatInput)).Length;
        AttackInputs = new bool[Count];
        cam = Camera.main;
    }
    private void Update() {
        CheckJumpInputHoldTime();
        CheckDashInputHoldTime();
        
    }
    public void OnMoveInput(InputAction.CallbackContext context){
        MoveInput = (int)context.ReadValue<float>();
    }
    public void OnJumpInput(InputAction.CallbackContext context){
        if(context.started){
            jumpInput = true;
            JumpInputStartTime = Time.time;
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
    public void OnDashDirectionInput(InputAction.CallbackContext context){
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
    void DetecObbject(){
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

    private void CheckJumpInputHoldTime(){
        if(Time.time >= JumpInputStartTime + inputHoldTime){
            jumpInput = false;
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
    Attack2
}
