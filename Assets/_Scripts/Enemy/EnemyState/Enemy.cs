using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using HStrong.ProjectileSystem;
using NaughtyAttributes;
using UnityEditor;
using UnityEngine;

public class Enemy : EnemyEntity
{

    [Header("Enemy Setting")]
    #region Set_component
    [OnValueChanged("OnValueChangedCallback")] 
    [SerializeField] protected int idEnemy;
    public Ease ease,easeEnd;
    [Expandable] public Enemy_SO enemy_Data;
    Sprite[] sprites;
    SpriteRenderer mSPR;
    CapsuleCollider2D CapsunCheckPlayer;
    #endregion
    //----------View_combat
    [SerializeField] bool isComBat;
    public Projectile prefabProjectile;
    public Cooldown cooldowns = new Cooldown();

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
    static int nextID = 0;  
    protected override void Awake() {
        base.Awake();
        core.uniqueID = nextID;
        nextID++;
        LoadComponent();
        mSPR.sortingOrder = core.uniqueID;
        Load_Enemy();
        moveState = new E_MoveState(this, stateMachine);
        idleState = new E_IdleState(this, stateMachine);

        chargeState = new E_ChargeState(this, stateMachine);
        lookState = new E_LookState(this, stateMachine);
        attackState = new E_AttackState(this, stateMachine);
        stuneState = new E_StuneState(this, stateMachine);
        knockBack = new E_KnockBack(this,stateMachine);
        deadState = new E_DeadState(this, stateMachine);
    }
    private void Start(){
        stateMachine.Initialize(moveState);
        core.GetCoreComponent<ItemDrop>().SetPossibleDrop( enemy_Data.dropInfo , enemy_Data.countDrop );
        Epos = transform.position;
        playerCheck = null;  
    }
    public virtual void Update()
    {
        canChangeState = stateMachine.canChange;
        core.LogicUpdate();
        cooldowns.Update();
        stateMachine.CurrentState.LogicUpdate();
        CheckPlayer();
        CheckReceiver();
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
    }

    #region ortherCheck
    void CheckPlayer(){
        if(!isComBat) return;
        Collider2D hit = Physics2D.OverlapCapsule(
            (Vector2)transform.position + playerCheckOffset,
            playerCheckSize,
            CapsuleDirection2D.Horizontal,
            0f,
            LayerMask.GetMask("Player")
        );

        if(hit)
        {
            playerCheck = hit.transform;
        }
        
    }
    private void CheckReceiver(){
        
        if(poiseReceiver.IsPoise()){
            
            stateMachine.ChangeState(stuneState);

        }else if(knockBackReceiver.isKnockBack){

            stateMachine.ChangeState(knockBack);
        }
    }

    private void CheckDead(){
        stateMachine.ChangeState(deadState);
    }

    #endregion

    #region FuncLoad
    protected override void LoadComponent(){
        base.LoadComponent();
        if(mCollider == null) mCollider = GetComponent<BoxCollider2D>();
        if(mSPR == null) mSPR = GetComponent<SpriteRenderer>(); 
        
    }

    private void Load_Enemy(){
        //if(enemy_Data == null) return;
        string resPath = "Enemy_Load/Enemy/Enemy " + idEnemy;
        this.enemy_Data = Resources.Load<Enemy_SO>(resPath);
        if(enemy_Data == null) {
            Debug.Log("Load Enemy Data Failed");
            return;
        }
        mPaint.LoadSprite(ref sprites,enemy_Data.textures, mPaint.BOTTOM | mPaint.HCENTER);
        Paint(0);

        float w = sprites[0].texture.width;
        float height = sprites[0].texture.height/100f;

        //--------- Set Collider-----------
        mCollider.size = new Vector2((w - w/5)/100, height );
        mCollider.offset = new Vector2 (0, height/2);

        if(core != null) core.height = height;
        
        ledgeCheck.position = new Vector3((w+20)/200 , 0.02f) + transform.position;
        if(mPhysic2D == null) return;
        switch (enemy_Data.type)
        {
            case 1:
                mPhysic2D.Gravity = (-9.8f *2f);
                playerCheckSize = new Vector2(maxAgroDetect,maxAgroDetect/2f);
                playerCheckOffset = new Vector2(0f,(playerCheckSize.y-2f)/2f);

			break;
            case 4:
                mPhysic2D.Gravity = (0);
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
    private void OnValueChangedCallback()
	{
		LoadComponent();
        Load_Enemy();
	}
    #endregion
}
