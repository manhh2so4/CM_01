using UnityEngine;

public class Draw_Enemy : MonoBehaviour
{
    [Header("Enemy Data")]
    //--------- Enemy Data -----------

    [SerializeField] Enemy_SO enemy_SO;
    Sprite[] sprites;
    [SerializeField] GameObject mSPR;
    [SerializeField] GameObject fxSPR;
    int idEnemyCurrent = -1;
    [SerializeField] protected int Hp = 0;
    protected Vector3 enemyPos;
    protected float enemyBotton;
    protected Vector3 centure;

    //--------- Enemy input Data -----------

    [SerializeField] protected int idEnemy;
    [SerializeField] protected int type;    
    protected int HpMax = 100;
    protected float minAgroDistance = 3f;
    protected float maxAgroDistance = 7f;
    [SerializeField] public float RangeAction = 1f;
    [SerializeField] public float RangeMove = 3f;

    
    //--------- Enemy Stage -----------

    public float speedMove,speedAtk;

    //--------- Enemy Combat -----------

    [SerializeField] protected CapsuleCollider2D mCollider;
    [SerializeField] protected Rigidbody2D mRB;
    
    //--------- Enemy Movement -----------

    [SerializeField] protected CapsuleCollider2D PlayerCheck;
    [SerializeField] protected BoxCollider2D AttackCheck;
    [SerializeField] protected BoxCollider2D groundCheck;
    [SerializeField] protected CapsuleCollider2D wallCheck;   

    protected void Load_Enemy(){
        if (idEnemyCurrent == idEnemy ) return;
        string resPath = "Enemy_Load/Enemy/Enemy " + idEnemy;
        this.enemy_SO = Resources.Load<Enemy_SO>(resPath);
        mPaint.LoadSprite(ref sprites,enemy_SO.textures, mPaint.BOTTOM | mPaint.HCENTER );
        mSPR.GetComponent<SpriteRenderer>().sprite = sprites[0];
        int w = sprites[0].texture.width;
        float height = sprites[0].texture.height/100f;
        centure.Set(0,height/2,0);
        enemyBotton = sprites[2].texture.height/2;

        //--------------------
        type = enemy_SO.type;

        speedMove = enemy_SO.speedMove;

        speedAtk = enemy_SO.speedAtk;

        HpMax = enemy_SO.Hp;

        Hp = HpMax;
        
        enemyPos = gameObject.transform.localPosition;

        idEnemyCurrent = idEnemy;

        //--------- Set Collider-----------
        mCollider.size = new Vector2((float)(w - w/5)/100,(float)(height));
        mCollider.offset = new Vector2 (0 ,(float) height/2);
        
        if(mCollider.size.x > mCollider.size.y) mCollider.direction = CapsuleDirection2D.Horizontal;
        else mCollider.direction = CapsuleDirection2D.Vertical;
        
        groundCheck.size = new Vector2(0.1f,0.2f);
        groundCheck.offset = new Vector2((float)(w - w/5)/200,-0.1f);

        wallCheck.size = new Vector2(0.3f,0.1f);
        wallCheck.offset = new Vector2 ((float)(w - w/5)/200,0.2f);
        switch (type)
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
    
    protected void LoadComponent(){
        if(mSPR == null) mSPR = transform.Find("Draw_Enemy").gameObject;
        if(fxSPR == null) fxSPR = transform.Find("Draw_FX").gameObject;
        if(mRB == null) mRB = GetComponent<Rigidbody2D>();
        if(mCollider == null) mCollider = GetComponent<CapsuleCollider2D>();
        if(groundCheck == null) groundCheck = transform.Find("GroundDetected").GetComponent<BoxCollider2D>();
        if(AttackCheck == null) AttackCheck = transform.Find("PlayerDetected").GetComponent<BoxCollider2D>();
        if(wallCheck == null) wallCheck = transform.Find("WallDetected").GetComponent<CapsuleCollider2D>(); 
        if(PlayerCheck == null) PlayerCheck = transform.Find("PlayerDetected").GetComponent<CapsuleCollider2D>();        
    }
    void SetRangePlayerCheck(float _maxAgroDistance){
        
    }

    public void Paint(int frameCurrent){
        mSPR.GetComponent<SpriteRenderer>().sprite = sprites[frameCurrent];
    }
}
