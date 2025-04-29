using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using HStrong.ProjectileSystem;
using NaughtyAttributes;
using UnityEditor;
using UnityEngine;

public abstract class EnemyEntity : Entity
{
    //--------- Enemy input Data -----------
    
    [Foldout("Data Enemy")] public float minAgroDistance = 2f;
    [Foldout("Data Enemy")] public float AgroDistance = 40f;
    [Foldout("Data Enemy")] public float maxAgroDetect = 7f;
    [Foldout("Data Enemy")] public float RangeMove = 3f;
    [Foldout("Data Enemy")] public float minIdleTime;
    [Foldout("Data Enemy")] public float maxIdleTime;
    protected Vector3 Epos;
    //----------View_data
    [OnValueChanged("OnValueChangedCallback")] 
    public int idEnemy;
    public string CurentState;
    public bool canAttack;

    #region Setup_Enemy
    public Transform playerCheck;
    [SerializeField] bool ShowLog;
    protected Transform ledgeCheck;
    protected Vector2 playerCheckOffset;
    protected Vector2 playerCheckSize;


    public FiniteStateMachine stateMachine; 
    protected BoxCollider2D mCollider;
    #endregion
    
    #region ColCheck
    public bool isledge()
    {
        return Physics2D.Raycast( ledgeCheck.position , Vector2.down, .1f, LayerMask.GetMask("Ground") );
    }
    #endregion
    
    #region FuncLoad
    protected override void Awake(){
        base.Awake();
        if(mCollider == null) mCollider = GetComponent<BoxCollider2D>();
        stateMachine = new FiniteStateMachine();
    }
    protected virtual void FistSetUp(){}
    protected virtual void LoadComponent(){
        if(ledgeCheck == null) ledgeCheck = transform.Find("LedgeDetected");
    }
    #endregion
#if UNITY_EDITOR
    public void OnDrawGizmos(){
        
        if(ShowLog == false) return;
        
        Gizmos.DrawLine(Epos,transform.position);
        if(playerCheck) {
            Gizmos.DrawLine(playerCheck.position, transform.position);
            GUIStyle style = new GUIStyle();
            style.fontSize = 20;  // Đặt kích thước font chữ
            style.normal.textColor = Color.blue;
            Handles.Label( (playerCheck.position + transform.position)/2, Vector2.Distance(playerCheck.position,transform.position).ToString(), style);
        }
        Gizmos.color = Color.green;
        Gizmos.DrawLine( ledgeCheck.position , ledgeCheck.position + Vector3.down * .1f);

        Gizmos.color = Color.red;
        Gizmos.DrawRay( (Vector2)transform.position + playerCheckOffset , Vector2.right * playerCheckSize.x/2);
        Gizmos.DrawRay( (Vector2)transform.position + playerCheckOffset , Vector2.left * playerCheckSize.x/2);
        Gizmos.DrawRay( (Vector2)transform.position + playerCheckOffset , Vector2.up * playerCheckSize.y/2);
        Gizmos.DrawRay( (Vector2)transform.position + playerCheckOffset , Vector2.down * playerCheckSize.y/2);

    }
    protected virtual void OnValueChangedCallback(){}
#endif
}

