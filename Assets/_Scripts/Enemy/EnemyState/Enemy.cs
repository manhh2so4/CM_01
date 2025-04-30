using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using HStrong.ProjectileSystem;
using NaughtyAttributes;
using UnityEditor;
using UnityEngine;

public class Enemy : EnemyEntity,IInteractable
{

    [Header("Enemy Setting")]
    #region Set_component
    public Ease ease,easeEnd;
    [Expandable] public Enemy_SO enemy_Data;
    Sprite[] sprites;
    SpriteRenderer mSPR;
    //CapsuleCollider2D CapsunCheckPlayer;
    #endregion
    //----------View_combat
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
    //-------------------------------------------
    #endregion
    static int nextID = 0;  
    protected override void Awake() {
        base.Awake();
        playerCheck = null;
        core.uniqueID = nextID;
        nextID++;
        LoadComponent();
        Load_Enemy(idEnemy);
        mSPR.sortingOrder = core.uniqueID;

    }
    void SetState(){

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
    }
    public virtual void Update()
    {
        core.LogicUpdate();
        cooldowns.Update();
        stateMachine.CurrentState.LogicUpdate();
        CheckPlayer();
        CheckReceiver();
    }
    private void OnEnable()
    {
        CharStats.OnDie += CheckDead;
    }
    void ShowImage(){
        mSPR.enabled = true;
    }
    private void OnDisable()
    {
        mSPR.enabled = false;
        CharStats.OnDie -= CheckDead;
    }

    #region ortherCheck
    void CheckPlayer(){
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
        this.GameEvents().miscEvents.KillEnemy(this);
        stateMachine.ChangeState(deadState);
    }

    #endregion

    #region FuncLoad
    protected override void LoadComponent(){
        base.LoadComponent();
        if(mCollider == null) mCollider = GetComponent<BoxCollider2D>();
        if(mSPR == null) mSPR = GetComponent<SpriteRenderer>(); 
    }

    public void Load_Enemy(int _idEnemy){

        idEnemy = _idEnemy;
        string resPath = "Enemy_Load/Enemy/Enemy " + _idEnemy;
        this.enemy_Data = Resources.Load<Enemy_SO>(resPath);

        if(enemy_Data == null) {
            Debug.Log("Load Enemy Data Failed");
            return;
        }

        sprites = enemy_Data.Sprites;
        //mPaint.LoadSprite(ref sprites,enemy_Data.textures, mPaint.BOTTOM | mPaint.HCENTER);
        Paint(0);

        float w = sprites[0].texture.width;
        float height = sprites[0].texture.height/100f;

        //--------- Set Collider-----------
        mCollider.size = new Vector2( (w - w/5)/100 , height );
        mCollider.offset = new Vector2 (0, height/2);

        if(core != null){
            core.Height = height;
            CharStats.Health.SetDefaultValue(enemy_Data.Hp);
            core.GetCoreComponent<ItemDrop>().SetPossibleDrop( enemy_Data.dropInfo , enemy_Data.countDrop );
            core.SetData();
        }

        ledgeCheck.position = new Vector3((w+20)/200 , 0.02f) + transform.position;
        if(mPhysic2D == null) return;

        switch (enemy_Data.type)
        {
            case 1:
            case 2:
            case 3:
                mPhysic2D.isColision = true;
                mPhysic2D.Gravity = (-9.8f *2f);
                playerCheckSize = new Vector2(maxAgroDetect,maxAgroDetect/2f);
                playerCheckOffset = new Vector2(0f,(playerCheckSize.y-2f)/2f);
			    break;

            case 4:
            case 5:
                mPhysic2D.isColision = false;
                mPhysic2D.Gravity = (0);
                transform.position = new Vector2(transform.position.x, transform.position.y + 2f);
                playerCheckOffset = Vector2.zero;
                playerCheckSize = new Vector2(maxAgroDetect,maxAgroDetect);

			    break;

            default:
                playerCheckSize = new Vector2(maxAgroDetect,maxAgroDetect/2f);
                playerCheckOffset = new Vector2(0f,(playerCheckSize.y-2f)/2f);
                break;
        }
        
        Epos = transform.position;
        SetState();
    }
    public void Paint(int frameCurrent){
        mSPR.sprite = sprites[frameCurrent];
    }
    public float Height => core.Height;
    public void Interact()
    {
        
    }
    protected override void OnValueChangedCallback()
	{
		LoadComponent();
        Load_Enemy(idEnemy);
	}
    
    #endregion
}
