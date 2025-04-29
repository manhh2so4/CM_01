using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using HStrong.Saving;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;

public class Player : Entity,ISaveable
{

    #region State Variable
    public FiniteStateMachine StateMachine { get;private set;}
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
    public PlayerKnockState knockState {get;private set;}

    #endregion
    #region Component
    public PaintChar paintChar {get;private set;}
    public PlayerInputHandler inputPlayer {get;private set;}
    [SerializeField] public Transform dashDirImgae;
    [SerializeField] private PlayerData playerData;

    #endregion

    //------------------------------------------

    #region Other Variable
    
    private SKill Skill_1;
    private SKill Skill_2;
    private SKill Skill_3;
    public Cooldown cooldowns = new Cooldown();
    public LayerMask layerDetectable;
    public DetectTarget detectTarget;
    public Collider2D target;
    public SelectObj selectObj;
    int indexTarget;
    #endregion
    
    #region Unity Callback Function
    protected override void Awake(){
        base.Awake();
        detectTarget = new DetectTarget(layerDetectable, 15 );
        StateMachine = new FiniteStateMachine();

        Skill_1 = transform.Find("Skill_1").GetComponent<SKill>();
        Skill_1.SetCore(core);

        Skill_2 = transform.Find("Skill_2").GetComponent<SKill>();
        Skill_2.SetCore(core);
        
        Skill_3 = transform.Find("Skill_3").GetComponent<SKill>();
        Skill_3.SetCore(core);

        inputPlayer = GetComponent<PlayerInputHandler>();
        core.Height = 1.3f;

        idleState = new PlayerIdleState(this,StateMachine, playerData,mState.Idle);
        moveState = new PlayerMoveState(this,StateMachine, playerData,mState.Moving);
        jumpState = new PlayerJumpState(this,StateMachine, playerData,mState.None);
        airState = new PlayerAirState(this,StateMachine, playerData,mState.None);
        wallSlideState = new PlayerWallSlideState(this, StateMachine,playerData,mState.Slide);  
        wallJumpState = new PlayerWallJumpState(this, StateMachine,playerData,mState.InAir);   
        dashState = new PlayerDashState(this, StateMachine,playerData,mState.Dash);

        Attack_1 = new PlayerAttackState(this, StateMachine, playerData, mState.Attack, Skill_1);   
        Attack_2 = new PlayerAttackState(this, StateMachine, playerData, mState.Attack, Skill_2);  
        Attack_3 = new PlayerAttackState(this, StateMachine, playerData, mState.Attack, Skill_3);  
        knockState = new PlayerKnockState(this, StateMachine, playerData, mState.Knockback);
    }
    private void Start(){
        paintChar = core.GetCoreComponent<PaintChar>();
        dashDirImgae = transform.Find("DashDirectionImg");
        StateMachine.Initialize(idleState);
        movement.SetGravity(playerData.GetGravity());
    }
    private void OnEnable() {
        knockBackReceiver.OnKnockBack += KnockBackEnd;
        CharStats.OnDie += Die;
    }
    private void OnDisable() {
        knockBackReceiver.OnKnockBack -= KnockBackEnd;
        CharStats.OnDie -= Die;
    }

    private void KnockBackEnd()
    {
        StateMachine.ChangeState(knockState);
    }

    private void Update(){

        core.LogicUpdate();
        
        cooldowns.Update();
        StateMachine.CurrentState.LogicUpdate();  

    }
    private void FixedUpdate(){

    }
    [Button]
    private void Die(){
        RespawnHome().Forget();
    }
    private async UniTaskVoid RespawnHome()
    {
        await Fader.Instance.FadeOut(0.2f);

        SavingWrapper.Save();
        await SavingWrapper.LoadToScene("Home");
        SavingWrapper.LoadData();

        SavingWrapper.Save();

        await Fader.Instance.FadeIn(0.2f);
    }
    [Button]
    void SetTarget(){

        if( indexTarget >= detectTarget.objDetected.Count ) indexTarget = 0;
        target = detectTarget.objDetected[indexTarget];

        if( target.TryGetComponent<IInteractable>( out IInteractable interactable) ) {
            selectObj.setUp( 0.2f, target.transform);
        }
        indexTarget++;
    }

    private void OnDrawGizmos(){

    }

    #endregion
    public object CaptureState()
    {
        return new SerializableVector3(transform.position);
    }

    public void RestoreState(object state)
    {
        SerializableVector3 position = state as SerializableVector3;
        transform.position = position.ToVector();
    }
    

}
