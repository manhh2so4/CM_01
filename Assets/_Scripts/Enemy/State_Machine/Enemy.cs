using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Movement Movement { get => movement ?? Core.GetCoreComponent(ref movement); }

	private Movement movement;
    public Core Core { get; private set; }
    [SerializeField] Enemy_SO enemy_Data;
    Sprite[] sprites;
    SpriteRenderer mSPR;
    SpriteRenderer fxSPR;
    int idEnemyCurrent = -1;
    [SerializeField] protected int idEnemy;
    [SerializeField] protected CapsuleCollider2D mCollider;
    [SerializeField] protected Rigidbody2D mRB;


    //--------- Enemy input Data -----------
    protected float minAgroDistance = 3f;
    protected float maxAgroDistance = 7f;
    [SerializeField] public float RangeAction = 1f;
    [SerializeField] public float RangeMove = 3f;
    public Vector3 enemyPos;
    public float vY = 1,dirY;
    public float minIdleTime;
    public float maxIdleTime;
    
    //--------- Enemy Movement -----------

    [SerializeField] protected CapsuleCollider2D PlayerCheck;
    [SerializeField] protected BoxCollider2D AttackCheck;
    [SerializeField] protected BoxCollider2D ledgeCheck;
    [SerializeField] protected CapsuleCollider2D wallCheck;  
    
    public EnemyIdleState idleState { get; private set;}
    public EnemyMoveState moveState { get; private set;}


    public FiniteStateMachine stateMachine;
    private void Awake() {
        Core = GetComponentInChildren<Core>();
        stateMachine = new FiniteStateMachine();
        LoadComponent();
        Load_Enemy();

        moveState = new EnemyMoveState(this, stateMachine, enemy_Data);
        idleState = new EnemyIdleState(this, stateMachine, enemy_Data);

    }
    private void Start() {
        stateMachine.Initilize(moveState);  
    }
    public virtual void Update()
    {
        stateMachine.currentStage.LogicUpdate();
    }

    public virtual void FixedUpdate()
    {
        stateMachine.currentStage.PhysicsUpdate();
    }


    public bool CheckPlayerInMaxAgroRange(){
        return PlayerCheck.IsTouchingLayers(LayerMask.GetMask("Player"));
    }
    public bool CheckPlayerInCloseRangeAction()
    {
        return AttackCheck.IsTouchingLayers(LayerMask.GetMask("Player"));
    }
    public bool isledge()
    {
        return ledgeCheck.IsTouchingLayers(LayerMask.GetMask("Ground"));
    }
    public bool isWall()
    {
        return wallCheck.IsTouchingLayers(LayerMask.GetMask("Ground"));
    }

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
        if(AttackCheck == null) AttackCheck = transform.Find("PlayerDetected").GetComponent<BoxCollider2D>();
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

        enemyPos = gameObject.transform.localPosition;


        //--------- Set Collider-----------
        mCollider.size = new Vector2((float)(w - w/5)/100,(float)(height));
        mCollider.offset = new Vector2 (0 ,(float) height/2);
        
        if(mCollider.size.x > mCollider.size.y) mCollider.direction = CapsuleDirection2D.Horizontal;
        else mCollider.direction = CapsuleDirection2D.Vertical;
        
        ledgeCheck.size = new Vector2(0.1f,0.2f);
        ledgeCheck.offset = new Vector2((float)(w - w/5)/200,-0.1f);

        wallCheck.size = new Vector2(0.3f,0.1f);
        wallCheck.offset = new Vector2 ((float)(w - w/5)/200,0.2f);
        switch (enemy_Data.type)
        {
            case 1:
                mRB.gravityScale = 2;
                PlayerCheck.size = new Vector2(maxAgroDistance,maxAgroDistance/2f);
                PlayerCheck.offset = new Vector2(0f,(PlayerCheck.size.y-2f)/2f);

                AttackCheck.size = new Vector2(RangeAction,height);
                AttackCheck.offset = new Vector2(RangeAction/2,height/2f);
			break;
            case 4:
                mRB.gravityScale = 0;
                PlayerCheck.size = new Vector2(maxAgroDistance,maxAgroDistance);
                AttackCheck.size = new Vector2(RangeAction*2,RangeAction*2);
			break;

            default:
                PlayerCheck.size = new Vector2(maxAgroDistance,maxAgroDistance/2f);
                PlayerCheck.offset = new Vector2(0f,(PlayerCheck.size.y-2f)/2f);
                AttackCheck.size = new Vector2(RangeAction,height);
                AttackCheck.offset = new Vector2(RangeAction/2,height/2f);
            break;

        }



    }
    public void Paint(int frameCurrent){
        mSPR.GetComponent<SpriteRenderer>().sprite = sprites[frameCurrent];
    }
    
}
