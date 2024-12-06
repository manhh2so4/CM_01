using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEditor;
using UnityEngine;

public abstract class EnemyEntity : Entity
{
    //--------- Enemy input Data -----------
    [SerializeField] protected int idEnemy;
    public GameObject projectile;
    public Ease ease,easeEnd;
    public LayerMask LayerCombat;
    public float minAgroDistance = 2f;
    public float AgroDistance = 10f;
    public float maxAgroDetect = 7f;
    public float RangeMove = 3f;
    public float minIdleTime;
    public float maxIdleTime;
    protected Vector3 Epos;
    //----------View_data
    public bool canAttack;
    public bool canChangeState;
    public StateEnemy state;

    #region Setup_Enemy
    public Transform playerCheck;
    protected CapsuleCollider2D PlayerCheck;
    protected BoxCollider2D ledgeCheck;
    protected CapsuleCollider2D wallCheck;
    public FiniteStateMachine stateMachine; 
    #endregion
    
    #region ColCheck
    public bool isledge()
    {
        return ledgeCheck.IsTouchingLayers(LayerMask.GetMask("Ground"));
    }
    public bool isWall()
    {
        return wallCheck.IsTouchingLayers(LayerMask.GetMask("Ground"));
    }
    public bool isGround()
    {
        return mCollider.IsTouchingLayers(LayerMask.GetMask("Ground"));
    }
    #endregion
    
    #region FuncLoad
    protected virtual void LoadCore(){
        stateMachine = new FiniteStateMachine();
    }
    protected virtual void LoadComponent(){
        if(ledgeCheck == null) ledgeCheck = transform.Find("GroundDetected").GetComponent<BoxCollider2D>();
        if(wallCheck == null) wallCheck = transform.Find("WallDetected").GetComponent<CapsuleCollider2D>(); 
        if(PlayerCheck == null) PlayerCheck = transform.Find("PlayerDetected").GetComponent<CapsuleCollider2D>();
    }
    #endregion
    public virtual void OnDrawGizmos(){
         Gizmos.DrawLine(Epos,transform.position);
         if(playerCheck) {
            Gizmos.DrawLine(playerCheck.position,transform.position);
            Handles.color = Color.red;
            Handles.Label((playerCheck.position+transform.position)/2, Vector2.Distance(playerCheck.position,transform.position).ToString());
         }
    }
}

