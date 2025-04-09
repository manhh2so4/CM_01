using System;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEditor;
using UnityEngine;
public class NPC : Entity {
#region State Variable
    public FiniteStateMachine StateMachine { get;private set;}
    public NPC_IdleState idle {get;private set;}
    public NPC_MoveState move {get;private set;}
    public NPC_JumpState jump {get;private set;}
    public NPC_ChargeState charge {get;private set;}
    public NPC_LookState look {get;private set;}
    public NPC_AirState air {get;private set;}
    public NPC_AttackState attack1 {get;private set;}
    public NPC_AttackState attack2 {get;private set;}
    public NPC_AttackState attack3 {get;private set;}
#endregion

#region Data
    public string CurentState;
    public float RangeMove;


#endregion


#region Component
    public PaintChar paintChar {get;private set;}
    [Expandable]public DataNPC_SO dataNPC;

    SKill Skill_1,Skill_2,Skill_3;
    public Cooldown cooldowns = new Cooldown();
    
#endregion
    //--------Detect---------
    public Transform Target;
    [SerializeField] float radiusDetect = 10;
    LayerMask layerDetect;
    [SerializeField] bool canDetect;
    //------------------------

    
    //--------Input Action--------
    public float mSpeed = 5;
    public FindPathHandle findPathHandle;

#region UnityCallback
    protected override void Awake(){
        base.Awake();
        findPathHandle = GetComponent<FindPathHandle>();
        findPathHandle.SetMovement(movement);
        
        Skill_1 = transform.Find("Skill_1").GetComponent<SKill>();
        Skill_1.SetCore(core);

        // Skill_2 = transform.Find("Skill_2").GetComponent<SKill>();
        // Skill_2.SetCore(core);
        
        // Skill_3 = transform.Find("Skill_3").GetComponent<SKill>();
        // Skill_3.SetCore(core);
        core.height = 1.3f;

        StateMachine = new FiniteStateMachine();
        idle = new NPC_IdleState(this, StateMachine );
        move = new NPC_MoveState(this, StateMachine );
        jump = new NPC_JumpState(this, StateMachine );
        charge = new NPC_ChargeState(this, StateMachine );
        look = new NPC_LookState(this, StateMachine );
        air = new NPC_AirState(this, StateMachine );
        attack1 = new NPC_AttackState(this, StateMachine, Skill_1);
        // attack2 = new NPC_AttackState(this, StateMachine, Skill_2);
        // attack3 = new NPC_AttackState(this, StateMachine, Skill_3);
    }
    private void Start(){

        paintChar = core.GetCoreComponent<PaintChar>();
        movement.SetGravity( dataNPC.GetGravity() );
        StateMachine.Initialize( idle );
    }

    [Button]
    void Attack(){
        
    }


    private void Update(){
        OverLapObj();
        core.LogicUpdate();
        cooldowns.Update();
        StateMachine.CurrentState.LogicUpdate(); 
    }
    
#endregion
#region Check
    void OverLapObj(){
        if( canDetect == false ) return;
        Collider2D collider = Physics2D.OverlapCircle( transform.position + transform.up * core.height , radiusDetect , layerDetect );
        if( collider != null ){
            Target = collider.transform;
        }
    }
    public bool isledge()
    {
        return Physics2D.Raycast( transform.position + transform.right * 0.4f , Vector2.down , .1f, LayerMask.GetMask("Ground") );
    }
    
#endregion

#if UNITY_EDITOR
    void OnDrawGizmos()
    {
        
    }

    void DrawDetect(){
        Gizmos.DrawRay( transform.position + transform.right * 0.4f , Vector2.down *.1f);
        if(Target){
            
            Gizmos.DrawLine(Target.position, transform.position);
            GUIStyle style = new GUIStyle();
            style.fontSize = 20;  // Đặt kích thước font chữ
            style.normal.textColor = Color.blue;
            Handles.Label( (Target.position + transform.position)/2, Vector2.Distance(Target.position,transform.position).ToString(), style);
            
        }
        Gizmos.DrawWireSphere( transform.position + transform.up * 1.3f , radiusDetect );
    }

#endif
}