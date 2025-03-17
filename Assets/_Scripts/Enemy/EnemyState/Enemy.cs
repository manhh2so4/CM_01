using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEditor;
using UnityEngine;

public class Enemy : EnemyEntity
{
    #region Set_component
    public Enemy_SO enemy_Data;
    Sprite[] sprites;
    SpriteRenderer mSPR;
    
    #endregion
    //----------View_data

    #region Setup_Enemy
    //---------------- Set State----------------------------
    public E_IdleState idleState { get; private set;}
    public E_MoveState moveState { get; private set;}

    public E_AttackState attackState { get; private set;}
    public E_LookState lookState { get; private set;}
    public E_ChargeState chargeState { get; private set;}
    public E_StuneState stuneState { get; private set;}
    public E_KnockBack knockBack { get; private set;}
    public E_DeadState deadState { get; private set;}

    #endregion
    protected override void Awake() {
        base.Awake();
        LoadComponent();
        Load_Enemy();
        LoadCore();
        moveState = new E_MoveState(this, stateMachine);
        idleState = new E_IdleState(this, stateMachine);

        chargeState = new E_ChargeState(this, stateMachine);
        lookState = new E_LookState(this, stateMachine);
        attackState = new E_AttackState(this, stateMachine);
        stuneState = new E_StuneState(this, stateMachine);
        knockBack = new E_KnockBack(this,stateMachine);
        deadState = new E_DeadState(this, stateMachine);
    }
    private void Start() {
        stateMachine.Initialize(moveState);
        Epos = transform.position;
        playerCheck = null;  
    }
    public virtual void Update()
    {
        canChangeState = stateMachine.canChange;
        stateMachine.CurrentState.LogicUpdate();
        core.LogicUpdate();
        CheckReceiver();
        CheckPlayer();
    }
    void CheckPlayer(){

        RaycastHit2D hit = Physics2D.CapsuleCast(
            (Vector2)transform.position + playerCheckOffset,
            playerCheckSize,
            CapsuleDirection2D.Horizontal,
            0f,
            Vector2.right,
            0f,
            LayerMask.GetMask("Player")
        );

        if(hit)
        {
            playerCheck = hit.transform;
        }
        
    }

    public virtual void FixedUpdate()
    {
        stateMachine.CurrentState.PhysicsUpdate();
    }
    private void OnEnable()
    {
        CharStats.onHealthZero += CheckDead;
    }
    private void OnDisable()
    {
        CharStats.onHealthZero -= CheckDead;
    }
    private void Reset() {
        LoadComponent();
        Load_Enemy();
        LoadCore();
    }
    #region ortherCheck
    private void CheckReceiver(){
        
        if(poiseReceiver.IsPoise()){
            
            stateMachine.ChangeState(stuneState);
            return;
        }
        if(knockBackReceiver.isKnockBackActive){
            stateMachine.ChangeState(knockBack);
        }
    }
    private void CheckDead(){
        stateMachine.ChangeState(deadState);
    }

    #endregion
    #region ColCheck
    // private void OnTriggerEnter2D(Collider2D other) {
        
    //     if ( (LayerCombat.value & (1 << other.gameObject.layer)) != 0)
    //     {
    //         if (other.tag == "Enemy") return;
    //         playerCheck = other.transform;
    //     }
    // }
    #endregion

    #region FuncLoad
    protected override void LoadCore(){
        base.LoadCore();
    }
    protected override void LoadComponent(){
        base.LoadComponent();
        if(mSPR == null) mSPR = transform.Find("Draw_Enemy").GetComponent<SpriteRenderer>(); 
    }
    private void Load_Enemy(){
        string resPath = "Enemy_Load/Enemy/Enemy " + idEnemy;
        this.enemy_Data = Resources.Load<Enemy_SO>(resPath);
        mPaint.LoadSprite(ref sprites,enemy_Data.textures, mPaint.BOTTOM | mPaint.HCENTER);
        Paint(0);

        float w = sprites[0].texture.width;
        float height = sprites[0].texture.height/100f;

        //--------- Set Collider-----------
        mCollider.size = new Vector2((w - w/5)/100, height );
        mCollider.offset = new Vector2 (0, height/2);
        
        ledgeCheck.position = new Vector3((w+20)/200 , 0.02f) + transform.position;

        switch (enemy_Data.type)
        {
            case 1:
                mRB.Gravity = (-9.8f *2f);
                playerCheckSize = new Vector2(maxAgroDetect,maxAgroDetect/2f);
                playerCheckOffset = new Vector2(0f,(playerCheckSize.y-2f)/2f);

			break;
            case 4:
                mRB.Gravity = (0);
                playerCheckOffset = Vector2.zero;
                playerCheckSize = new Vector2(maxAgroDetect,maxAgroDetect);

			break;

            default:
                playerCheckSize = new Vector2(maxAgroDetect,maxAgroDetect/2f);
                playerCheckOffset = new Vector2(0f,(playerCheckSize.y-2f)/2f);

            break;
        }
    }
    public void Paint(int frameCurrent){
        mSPR.sprite = sprites[frameCurrent];
    }
    #endregion
}
