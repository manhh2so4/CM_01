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
    private Player_input playerInput;
    //------------Move---------------
    [field : SerializeField]public int MoveInput {get;private set;}
    public bool[] AttackInputs {get;private set;}
    private float inputHoldTime = 0.1f;

    //------------Jump---------------
    private float JumpInputStartTime;
    public bool jumpInput {get;private set;}
    public float amountJump {get;private set;}

    private float chargeTime = .3f;
    private bool isCharging = false;
    private float chargeStartTime;


    //------------Dashh---------------
    public Vector2 DashDirInput {get;private set;}
    public bool dashInputStop {get;private set;}
    public bool dashInput {get;private set;}
    private float dashInputStartTime;
    private Vector2 mousePos;
    public Action<Vector2i> OnClickPos;
   //---------------------------
   void Awake()
   {
        playerInput = new Player_input();
   }
    void OnEnable(){
        playerInput.Enable();
        playerInput.Player.Move.performed += OnMoveInput;
        playerInput.Player.Move.canceled += OnMoveInput;

        playerInput.Player.Jump.started += OnJumpInput;
        playerInput.Player.Jump.canceled += OnJumpInput;

        playerInput.Player.Dash.started += OnDashInput;
        playerInput.Player.Dash.performed += OnDashInput;
        playerInput.Player.Dash.canceled += OnDashInput;

        playerInput.Player.Attack1.started += OnAttack1;
        playerInput.Player.Attack1.canceled += OnAttack1;

        playerInput.Player.Attack2.started += OnAttack2;
        playerInput.Player.Attack2.canceled += OnAttack2;

        playerInput.Player.Attack3.started += OnAttack3;
        playerInput.Player.Attack3.canceled += OnAttack3;

        playerInput.Player.Save.started += OnSave;
        playerInput.Player.Load.started += OnLoad;

        playerInput.Player.LeftClick.started += OnLeftClick;
        playerInput.Player.MousePos.performed += OnMousePosition;
        
    }
    void OnDisable(){
        playerInput.Player.Move.performed -= OnMoveInput;
        playerInput.Player.Move.canceled -= OnMoveInput;

        playerInput.Player.Jump.started -= OnJumpInput;
        playerInput.Player.Jump.canceled -= OnJumpInput;

        playerInput.Player.Dash.started -= OnDashInput;
        playerInput.Player.Dash.performed -= OnDashInput;
        playerInput.Player.Dash.canceled -= OnDashInput;


        playerInput.Player.Attack1.started -= OnAttack1;
        playerInput.Player.Attack1.canceled -= OnAttack1;

        playerInput.Player.Attack2.started -= OnAttack2;
        playerInput.Player.Attack2.canceled -= OnAttack2;

        playerInput.Player.Attack3.started -= OnAttack3;
        playerInput.Player.Attack3.canceled -= OnAttack3;

        playerInput.Player.Save.started -= OnSave;
        playerInput.Player.Load.started -= OnLoad;

        playerInput.Player.LeftClick.started -= OnLeftClick;
        playerInput.Player.MousePos.performed -= OnMousePosition;

        playerInput.Disable();
    }
    private void Start() {
        int Count = Enum.GetValues(typeof(CombatInput)).Length;
        AttackInputs = new bool[Count];
        cam = Camera.main;
    }
    private void Update() {
        CheckChargeJumpHoldTime();
        CheckDashInputHoldTime();

    }
    public void OnMoveLeft(TypeButton typeButton){
        if(typeButton == TypeButton.Press){
            MoveInput = -1;
        }else{
            MoveInput = 0;
        }
    }
    public void OnMoveRight(TypeButton typeButton){
        if(typeButton == TypeButton.Press){
            MoveInput = 1;
        }else{
            MoveInput = 0;
        }
    }
    void OnMoveInput(InputAction.CallbackContext context){
        MoveInput = (int)context.ReadValue<float>();
    }
    public void OnJump(TypeButton typeButton){
        if(typeButton == TypeButton.Press){
            isCharging = true;
            chargeStartTime = Time.time;
        }
        if(typeButton == TypeButton.Release){
            if( isCharging == false ) return; 
            isCharging = false;
            float chargeDuration = Time.time - chargeStartTime;
            amountJump = Mathf.Clamp01(chargeDuration / chargeTime);
            jumpInput = true;
            JumpInputStartTime = Time.time;
        }
    }
    void OnJumpInput(InputAction.CallbackContext context){

        if(context.started){
            OnJump(TypeButton.Press);
        }

        if(context.canceled){
            OnJump(TypeButton.Release);
        }
    }

    public void OnDashInput(InputAction.CallbackContext context){
        if(context.started){
        }
        if(context.performed){

            DashDirInput = context.ReadValue<Vector2>();
            //DashDirInput = Vector2Int.RoundToInt(RawDir.normalized);
        }
        if(context.canceled){
            dashInputStop = true;
        }
    }
    void OnMousePosition(InputAction.CallbackContext context){
        mousePos = context.ReadValue<Vector2>();
    }
    void OnAttack1(InputAction.CallbackContext context){
        if(context.started){
            AttackInputs[ (int)CombatInput.Attack1 ] = true;
        }
        if(context.canceled){
            AttackInputs[ (int)CombatInput.Attack1 ] = false;
        }
    }
    void OnAttack2(InputAction.CallbackContext context){
        if(context.started){
            AttackInputs[(int)CombatInput.Attack2] = true;
        }
        if(context.canceled){
            AttackInputs[(int)CombatInput.Attack2] = false;
        }
    }
    void OnAttack3(InputAction.CallbackContext context){
        if(context.started){
            AttackInputs[(int)CombatInput.Attack3] = true;
        }
        if(context.canceled){
            AttackInputs[(int)CombatInput.Attack3] = false;
        }
    }
    void OnLeftClick(InputAction.CallbackContext context){
        if(context.started)
        {
            Vector2 mousePos2 = cam.ScreenToWorldPoint(mousePos);
            OnClickPos?.Invoke( new Vector2i(  Mathf.FloorToInt(mousePos2.x), Mathf.FloorToInt(mousePos2.y)));
        }
    }

    void OnSave (InputAction.CallbackContext context){
        if(context.started)
        {
            SavingWrapper.Instance.Save();
        }
    }
    void OnLoad(InputAction.CallbackContext context){
        if(context.started)
        {
            SavingWrapper.Instance.Load();
        }
    }
    void DetecObbject(){
        Ray ray = Camera.main.ScreenPointToRay(mousePos);
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

        if(Time.time >= JumpInputStartTime + chargeTime){
            jumpInput = false;
        }
        if(isCharging == false) return;
        if( Time.time >= chargeStartTime + chargeTime){
            isCharging = false;
            jumpInput = true;
            JumpInputStartTime = Time.time;
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
