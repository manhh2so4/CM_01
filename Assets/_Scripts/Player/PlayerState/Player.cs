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
    public PlayerAttackState Attack_1 {get;private set;}
    public PlayerAttackState Attack_2 {get;private set;}
    public PlayerAttackState Attack_3 {get;private set;}

    #endregion
    #region Component
    public Char_anim Anim {get;private set;}
    public PlayerInputHandler inputPlayer {get;private set;}
    [SerializeField] public Transform dashDirImgae;
    [SerializeField] private PlayerData playerData;

    #endregion
    //------------------------------------------
    #region Other Variable
    private Weapon Skill_1;
    private Weapon Skill_2;
    private Weapon Skill_3;
    
    #endregion
    
    #region Unity Callback Function
    protected override void Awake(){
        base.Awake();
        StateMachine = new FiniteStateMachine();
        core.layerID = SortingLayer.NameToID("Player");

        Skill_1 = transform.Find("Skill_1").GetComponent<Weapon>();
        Skill_1.SetCore(core);

        Skill_2 = transform.Find("Skill_2").GetComponent<Weapon>();
        Skill_2.SetCore(core);
        
        Skill_3 = transform.Find("Skill_3").GetComponent<Weapon>();
        Skill_3.SetCore(core);

        idleState = new PlayerIdleState(this,StateMachine,playerData,mState.Idle);
        moveState = new PlayerMoveState(this,StateMachine,playerData,mState.Moving);
        jumpState = new PlayerJumpState(this,StateMachine,playerData,mState.None);
        airState = new PlayerAirState(this,StateMachine,playerData,mState.None);
        wallSlideState = new PlayerWallSlideState(this,StateMachine,playerData,mState.Slide);  
        wallJumpState = new PlayerWallJumpState(this,StateMachine,playerData,mState.InAir);   
        dashState = new PlayerDashState(this,StateMachine,playerData,mState.InAir);

        Attack_1 = new PlayerAttackState(this, StateMachine, playerData, mState.AttackStand, Skill_1);   
        Attack_2 = new PlayerAttackState(this, StateMachine, playerData, mState.AttackStand, Skill_2);  
        Attack_3 = new PlayerAttackState(this, StateMachine, playerData, mState.AttackStand, Skill_3);  
    }
    private void Start(){
        Anim = GetComponent<Char_anim>();
        inputPlayer = GetComponent<PlayerInputHandler>();
        dashDirImgae = transform.Find("DashDirectionImg");
        StateMachine.Initialize(idleState);
        core.GetCoreComponent<Movement>().SetGravity(playerData.GetGravity());
        
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
