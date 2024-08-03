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
    public PlayerLandState landState {get;private set;}
    public PlayerClimbState climbState {get;private set;}
    public PlayerWallSlideState wallSlideState {get;private set;}
    public PlayerWallJumpState wallJumpState {get;private set;}
    public PlayerDashState dashState {get;private set;}
    public PlayerAttackState Attack1 {get;private set;}
    public PlayerAttackState Attack2 {get;private set;}

    #endregion
    
    #region Component
    public Char_anim Anim {get;private set;}
    public inputPlayer inputPlayer {get;private set;}
    [SerializeField] public Transform dashDirImgae;
    [SerializeField] private PlayerData playerData;
    #endregion
    //------------------------------------------
    #region Other Variable
    public Core Core { get; private set; }
    [SerializeField] private BoxCollider2D mGroundCheck;
    [SerializeField] private BoxCollider2D mWallCheck;
    [SerializeField] private BoxCollider2D mWallBackCheck;
    #endregion
    
    #region Unity Callback Function
    private void Awake() {
        Core = GetComponentInChildren<Core>();
        StateMachine = new PlayerStateMachine();
        idleState = new PlayerIdleState(this,StateMachine,playerData,mState.Idle);
        moveState = new PlayerMoveState(this,StateMachine,playerData,mState.Moving);
        jumpState = new PlayerJumpState(this,StateMachine,playerData,mState.Jump);
        landState = new PlayerLandState(this,StateMachine,playerData,mState.InAir);
        airState = new PlayerAirState(this,StateMachine,playerData,mState.InAir);
        climbState = new PlayerClimbState(this,StateMachine,playerData,mState.Climb);
        wallSlideState = new PlayerWallSlideState(this,StateMachine,playerData,mState.Slide);  
        wallJumpState = new PlayerWallJumpState(this,StateMachine,playerData,mState.InAir);   
        dashState = new PlayerDashState(this,StateMachine,playerData,mState.InAir);
        Attack1 = new PlayerAttackState(this,StateMachine,playerData,mState.Attack); 
        Attack2 = new PlayerAttackState(this,StateMachine,playerData,mState.Attack);   
    }
    private void Start() {
        Anim = GetComponent<Char_anim>();
        inputPlayer = GetComponent<inputPlayer>();
        mGroundCheck = transform.Find("Ground_check").GetComponent<BoxCollider2D>();
        mWallCheck = transform.Find("Wall_check").GetComponent<BoxCollider2D>();
        mWallBackCheck = transform.Find("Wall_check_Back").GetComponent<BoxCollider2D>();
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
     //------------------------------------------
    #region Set Function

    #endregion
    //------------------------------------------
    #region Check Function
    public bool CheckTouchingWall(){
        return  mWallCheck.IsTouchingLayers(LayerMask.GetMask("Ground"));
    }
    public bool CheckTouchingGround(){
        return  mGroundCheck.IsTouchingLayers(LayerMask.GetMask("Ground"));
    }
    public bool CheckTouchingGroundBack(){
        return  mWallBackCheck.IsTouchingLayers(LayerMask.GetMask("Ground"));
    }
    #endregion
     //------------------------------------------
    #region Other Function
    private void AnimationTrigger() => StateMachine.CurrentState.AnimationTrigger();
    public void AnimationFinishTrigger() => StateMachine.CurrentState.AnimationFinishTrigger();
    #endregion
}
