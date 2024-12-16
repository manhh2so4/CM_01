using System.Collections;
using System.Collections.Generic;
using HStrong.Saving;
using UnityEngine;

public class Player : Entity,ISaveable
{

    #region State Variable
    public FiniteStateMachine StateMachine{get;private set;}
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
    
    #endregion
    
    #region Unity Callback Function
    protected override void Awake(){
        base.Awake();
        StateMachine = new FiniteStateMachine();
        core.layerID = SortingLayer.NameToID("Player");
        primaryWeapon = transform.Find("PrimaryWeapon").GetComponent<Weapon>();
        primaryWeapon.SetCore(core);
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
        core.LogicUpdate();
        StateMachine.CurrentState.LogicUpdate();  
    }
    private void FixedUpdate() {
        StateMachine.CurrentState.PhysicsUpdate();
    }

    public object CaptureState()
    {
        return new SerializableVector3(transform.position);
    }

    public void RestoreState(object state)
    {
        SerializableVector3 position = state as SerializableVector3;
        transform.position = position.ToVector();
    }
    #endregion
}
