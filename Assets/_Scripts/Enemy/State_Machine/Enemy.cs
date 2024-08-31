using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEditor;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    #region Set_component
    public Core Core { get; private set;}
    public KnockBackReceiver knockBackReceiver;
    public Enemy_SO enemy_Data;
    Sprite[] sprites;
    SpriteRenderer mSPR;
    SpriteRenderer fxSPR;
    protected CapsuleCollider2D mCollider;
    protected Rigidbody2D mRB;
    #endregion
    
    //--------- Enemy input Data -----------
    int idEnemyCurrent = -1;
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
    Vector3 Epos;
    //----------View_data
    public bool canAttack;
    public StateEnemy state;
    #region Setup_Enemy

    public Transform playerCheck;
    protected CapsuleCollider2D PlayerCheck;
    protected BoxCollider2D ledgeCheck;
    protected CapsuleCollider2D wallCheck;
    public FiniteStateMachine stateMachine;  
    //---------------- Set State----------------------------
    public E_IdleState idleState { get; private set;}
    public E_MoveState moveState { get; private set;}

    public E_AttackState attackState { get; private set;}
    public E_LookState lookState { get; private set;}
    public E_ChargeState chargeState { get; private set;}
    public E_StuneState stuneState { get; private set;}
    public E_KnockBack knockBack { get; private set;}

    #endregion
    private void Awake() {
        Core = GetComponentInChildren<Core>();
        knockBackReceiver = Core.GetCoreComponent<KnockBackReceiver>();
        stateMachine = new FiniteStateMachine();
        LoadComponent();
        Load_Enemy();
        moveState = new E_MoveState(this, stateMachine);
        idleState = new E_IdleState(this, stateMachine);

        chargeState = new E_ChargeState(this, stateMachine);
        lookState = new E_LookState(this, stateMachine);
        attackState = new E_AttackState(this, stateMachine);
        stuneState = new E_StuneState(this, stateMachine);
        knockBack = new E_KnockBack(this,stateMachine);
    }
    private void Start() {
        stateMachine.Initilize(moveState);
        Epos = transform.position;
        playerCheck = null;  
    }
    public virtual void Update()
    {
        stateMachine.currentStage.LogicUpdate();
        Core.LogicUpdate();
        CheckKnockBack();
    }

    public virtual void FixedUpdate()
    {
        stateMachine.currentStage.PhysicsUpdate();
    }
    #region ortherCheck
    void CheckKnockBack(){
        if (knockBackReceiver.isKnockBackActive){
            stateMachine.changeStage(knockBack);
        }
    }
    #endregion
    #region ColCheck
    public bool CheckPlayerInMaxAgroRange(){
        return PlayerCheck.IsTouchingLayers(LayerMask.GetMask("Player"));
    }
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
    private void OnTriggerEnter2D(Collider2D other) {
        
        if ( (LayerCombat.value & (1 << other.gameObject.layer)) != 0)
        {
            if (other.tag == "Enemy") return;
            Debug.Log("Chạm vào layer!" + other.gameObject.layer);
            playerCheck = other.transform;
        }
    }
    #endregion
    private void Reset() {
        LoadComponent();
        Load_Enemy();
    }
    protected void LoadComponent(){

        if(mSPR == null) mSPR = transform.Find("Draw_Enemy").GetComponent<SpriteRenderer>();
        if(fxSPR == null) fxSPR = transform.Find("Draw_FX").GetComponent<SpriteRenderer>();
        if(mRB == null) mRB = GetComponent<Rigidbody2D>();
        if(mCollider == null) mCollider = GetComponent<CapsuleCollider2D>();
        if(ledgeCheck == null) ledgeCheck = transform.Find("GroundDetected").GetComponent<BoxCollider2D>();
        if(wallCheck == null) wallCheck = transform.Find("WallDetected").GetComponent<CapsuleCollider2D>(); 
        if(PlayerCheck == null) PlayerCheck = transform.Find("PlayerDetected").GetComponent<CapsuleCollider2D>();

    }
    protected void Load_Enemy(){
        Debug.Log("load enemy");
        //if (idEnemyCurrent == idEnemy ) return;
        //idEnemyCurrent = idEnemy;
        string resPath = "Enemy_Load/Enemy/Enemy " + idEnemy;
        Debug.Log("load enemy");
        this.enemy_Data = Resources.Load<Enemy_SO>(resPath);
        mPaint.LoadSprite(ref sprites,enemy_Data.textures, mPaint.BOTTOM | mPaint.HCENTER);
        Paint(0);

        int w = sprites[0].texture.width;
        float height = sprites[0].texture.height/100f;

        //--------- Set Collider-----------
        mCollider.size = new Vector2((float)(w - w/5)/100,(float)(height));
        mCollider.offset = new Vector2 (0 ,(float) height/2);
        
        if(mCollider.size.x > mCollider.size.y) mCollider.direction = CapsuleDirection2D.Horizontal;
        else mCollider.direction = CapsuleDirection2D.Vertical;
        
        ledgeCheck.size = new Vector2(0.1f,0.2f);
        ledgeCheck.offset = new Vector2((float)(w + 20)/200,-0.1f);

        wallCheck.size = new Vector2(0.3f,0.1f);
        wallCheck.offset = new Vector2 ((float)(w - w/5)/200,0.2f);

        switch (enemy_Data.type)
        {
            case 1:
                mRB.gravityScale = 2;
                PlayerCheck.size = new Vector2(maxAgroDetect,maxAgroDetect/2f);
                PlayerCheck.offset = new Vector2(0f,(PlayerCheck.size.y-2f)/2f);

			break;
            case 4:
                mRB.gravityScale = 0;
                PlayerCheck.size = new Vector2(maxAgroDetect,maxAgroDetect);

			break;

            default:
                PlayerCheck.size = new Vector2(maxAgroDetect,maxAgroDetect/2f);
                PlayerCheck.offset = new Vector2(0f,(PlayerCheck.size.y-2f)/2f);

            break;

        }



    }
    public void Paint(int frameCurrent){
        mSPR.GetComponent<SpriteRenderer>().sprite = sprites[frameCurrent];
    }
    public virtual void OnDrawGizmos(){
         Gizmos.DrawLine(Epos,transform.position);
         if(playerCheck) {
            Gizmos.DrawLine(playerCheck.position,transform.position);
            Handles.color = Color.red;
            Handles.Label((playerCheck.position+transform.position)/2, Vector2.Distance(playerCheck.position,transform.position).ToString());
         }
    }
}
