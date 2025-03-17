using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using HStrong.ProjectileSystem;
using UnityEditor;
using UnityEngine;

public abstract class EnemyEntity : Entity
{
    //--------- Enemy input Data -----------
    [SerializeField] protected int idEnemy;
    public Projectile projectile;
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
    public Transform ledgeCheck;
    public Vector2 playerCheckOffset;
    public Vector2 playerCheckSize;


    public FiniteStateMachine stateMachine; 
    #endregion
    
    #region ColCheck
    public bool isledge()
    {
        return Physics2D.Raycast( ledgeCheck.position , Vector2.down, .05f, LayerMask.GetMask("Ground") );
    }
    

    #endregion
    
    #region FuncLoad
    protected virtual void LoadCore(){
        stateMachine = new FiniteStateMachine();
    }
    protected virtual void LoadComponent(){
        if(ledgeCheck == null) ledgeCheck = transform.Find("LedgeDetected").GetComponent<Transform>();
    }
    #endregion
    public virtual void OnDrawGizmos(){
        Gizmos.DrawLine(Epos,transform.position);

        if(playerCheck) {
            Gizmos.DrawLine(playerCheck.position,transform.position);
            Handles.color = Color.red;
            Handles.Label((playerCheck.position+transform.position)/2, Vector2.Distance(playerCheck.position,transform.position).ToString());
        }

        Gizmos.color = Color.green;
        Gizmos.DrawLine( ledgeCheck.position , ledgeCheck.position + Vector3.down * .05f);

        Gizmos.color = Color.red;
        Gizmos.DrawRay( (Vector2)transform.position + playerCheckOffset , Vector2.right * playerCheckSize.x/2);
        Gizmos.DrawRay( (Vector2)transform.position + playerCheckOffset , Vector2.left * playerCheckSize.x/2);
        Gizmos.DrawRay( (Vector2)transform.position + playerCheckOffset , Vector2.up * playerCheckSize.y/2);
        Gizmos.DrawRay( (Vector2)transform.position + playerCheckOffset , Vector2.down * playerCheckSize.y/2);



    }
}

