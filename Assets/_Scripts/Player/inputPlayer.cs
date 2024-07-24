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
     public Vector2Int DashDirInput {get;private set;}
    public int MoveInput {get;private set;}
    public bool jumpInput {get;private set;}
    public bool grabInput {get;private set;}
    public bool dashInput {get;private set;}
    public bool dashInputStop {get;private set;}
    [SerializeField]
    private float inputHoldTime = 0.2f;
    private float JumpInputStartTime;
    private float dashInputStartTime;
    private void Start() {
        playerInput = GetComponent<PlayerInput>();
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
    public void OnGranInput(InputAction.CallbackContext context){
        if(context.started){
            grabInput = true;
        }
        if(context.canceled){
            grabInput = false;
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
    public void OnFireInput(InputAction.CallbackContext context){
        
    }
    public void OnDashDirectionInput(InputAction.CallbackContext context){
        RawDashDirectionInput = context.ReadValue<Vector2>();
        if(playerInput.currentControlScheme == "Keyboard"){
            RawDashDirectionInput = cam.ScreenToViewportPoint((Vector3)RawDashDirectionInput - transform.position);
        }
        DashDirInput = Vector2Int.RoundToInt(RawDashDirectionInput.normalized);
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
