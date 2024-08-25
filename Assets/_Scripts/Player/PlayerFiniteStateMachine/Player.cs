using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    #region State Variable
    public PlayerStateMachine StateMachine{get;private set;}

    public PlayerIdleState idleState {get;private set;}
    public PlayerMoveState moveState{get;private set;}
    public PlayerJumpState jumpState{get;private set;}
    public PlayerAirState airState{get;private set;}
    public PlayerWallSlideState wallSlideState {get;private set;}
    public PlayerWallJumpState wallJumpState {get;private set;}
    public PlayerDashState dashState {get;private set;}
    public PlayerAttackState PrimaryAttack {get;private set;}

    #endregion
    #region Component
    public Char_anim Anim {get;private set;}
    public inputPlayer inputPlayer {get;private set;}
    [SerializeField] public Transform dashDirImgae;
    [SerializeField] private PlayerData playerData;

    #endregion
    //------------------------------------------
    #region Other Variable
    private Weapon primaryWeapon;
    private Weapon secondaryWeapon;
    public Core Core { get; private set; }
    #endregion
    
    #region Unity Callback Function
    private void Awake() {

        Core = GetComponentInChildren<Core>();
        StateMachine = new PlayerStateMachine();

        primaryWeapon = transform.Find("PrimaryWeapon").GetComponent<Weapon>();
        //secondaryWeapon = transform.Find("SecondaryWeapon").GetComponent<Weapon>();

        primaryWeapon.SetCore(Core);
        //secondaryWeapon.SetCore(Core);

        idleState = new PlayerIdleState(this,StateMachine,playerData,mState.Idle);
        moveState = new PlayerMoveState(this,StateMachine,playerData,mState.Moving);
        jumpState = new PlayerJumpState(this,StateMachine,playerData,mState.Jump);
        airState = new PlayerAirState(this,StateMachine,playerData,mState.InAir);
        wallSlideState = new PlayerWallSlideState(this,StateMachine,playerData,mState.Slide);  
        wallJumpState = new PlayerWallJumpState(this,StateMachine,playerData,mState.InAir);   
        dashState = new PlayerDashState(this,StateMachine,playerData,mState.InAir);
        PrimaryAttack = new PlayerAttackState(this,StateMachine,playerData,mState.AttackStand,primaryWeapon);   
    }
    private void Start() {
        Anim = GetComponent<Char_anim>();
        inputPlayer = GetComponent<inputPlayer>();
        dashDirImgae = transform.Find("DashDirectionImg");
        StateMachine.Initialize(idleState);
    }
    private void Update() {
        Core.LogicUpdate();
        StateMachine.CurrentState.LogicUpdate();  
    }
    private void FixedUpdate() {
        StateMachine.CurrentState.PhysicsUpdate();
    }
    #endregion
     //-------------------------------------------

     //------------------------------------------
    #region Other Function
    private void AnimationTrigger() => StateMachine.CurrentState.AnimationTrigger();
    public void AnimationFinishTrigger() => StateMachine.CurrentState.AnimationFinishTrigger();
    #endregion
}
