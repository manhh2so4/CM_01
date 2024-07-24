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
    public PlayerWallGrabState wallGrabState {get;private set;}
    public PlayerWallJumpState wallJumpState {get;private set;}
    public PlayerDashState dashState {get;private set;}

    #endregion
    
    #region Component
    public Char_anim Anim {get;private set;}
    public inputPlayer inputPlayer {get;private set;}
    public Rigidbody2D mRB {get;private set;}
    [SerializeField] public Transform dashDirImgae;
    [SerializeField] private PlayerData playerData;
    #endregion
    //------------------------------------------
    #region Other Variable
    public Vector2 currentVeclocity;
    private Vector2 workspace;
    [SerializeField] private BoxCollider2D mGroundCheck;
    [SerializeField] private BoxCollider2D mWallCheck;
    [SerializeField] private BoxCollider2D mWallBackCheck;
    public int facingDirection = 1;
    #endregion
    
    #region Unity Callback Function
    private void Awake() {
        facingDirection = 1;
        StateMachine = new PlayerStateMachine();
        idleState = new PlayerIdleState(this,StateMachine,playerData,mState.Idle);
        moveState = new PlayerMoveState(this,StateMachine,playerData,mState.Moving);
        jumpState = new PlayerJumpState(this,StateMachine,playerData,mState.Jump);
        landState = new PlayerLandState(this,StateMachine,playerData,mState.InAir);
        airState = new PlayerAirState(this,StateMachine,playerData,mState.InAir);
        climbState = new PlayerClimbState(this,StateMachine,playerData,mState.Climb);
        wallSlideState = new PlayerWallSlideState(this,StateMachine,playerData,mState.Slide);
        wallGrabState = new PlayerWallGrabState(this,StateMachine,playerData,mState.Grab);    
        wallJumpState = new PlayerWallJumpState(this,StateMachine,playerData,mState.InAir);   
        dashState = new PlayerDashState(this,StateMachine,playerData,mState.InAir);   
    }
    private void Start() {
        Anim = GetComponent<Char_anim>();
        inputPlayer = GetComponent<inputPlayer>();
        mGroundCheck = transform.Find("Ground_check").GetComponent<BoxCollider2D>();
        mWallCheck = transform.Find("Wall_check").GetComponent<BoxCollider2D>();
        mWallBackCheck = transform.Find("Wall_check_Back").GetComponent<BoxCollider2D>();
        dashDirImgae = transform.Find("DashDirectionImg");
        mRB = GetComponent<Rigidbody2D>();
        StateMachine.Initialize(idleState);
    }
    private void Update() {
        currentVeclocity = mRB.velocity;
        StateMachine.CurrentState.LogicUpdate();
    }
    private void FixedUpdate() {
        StateMachine.CurrentState.PhysicsUpdate();
    }
    #endregion
     //------------------------------------------
    #region Set Function
    public void SetVeclocity(float veclocity,Vector2 angle,int dir){
        angle.Normalize();
        workspace.Set(angle.x*veclocity*dir,angle.y*veclocity);
        mRB.velocity = workspace;
        currentVeclocity = workspace;
    }
    public void SetVeclocity(float veclocity,Vector2 dir){
        workspace = dir*veclocity ;
        mRB.velocity = workspace;
        currentVeclocity = workspace;
    }
    public void SetVeclocityX(float vecX){
        workspace.Set(vecX,currentVeclocity.y);
        mRB.velocity = workspace;
        currentVeclocity = workspace;
    }
    public void SetVeclocityY(float vecY){
        workspace.Set(currentVeclocity.x,vecY);
        mRB.velocity = workspace;
        currentVeclocity = workspace;
    }
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
    public void CheckIfShouldFlip(int input){
        if(input != 0 && input != facingDirection){
            Flip();
        }
    }
    #endregion
     //------------------------------------------
    #region Other Function
     private void AnimationTrigger() => StateMachine.CurrentState.AnimationTrigger();
     private void AnimationFinishTrigger() => StateMachine.CurrentState.AnimationFinishTrigger();
    void Flip(){
        facingDirection *= -1;
        transform.Rotate(0f,180f,0f);
    }
    #endregion
}
