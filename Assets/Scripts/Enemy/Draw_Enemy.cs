using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class Draw_Enemy : MonoBehaviour
{
    //--------- Enemy Data -----------
    mSystem mSystem = new mSystem();
    public bool a;
    [SerializeField] Enemy_SO enemy_SO;
    Sprite[] sprites;
    [SerializeField] GameObject mSPR;
    [SerializeField] GameObject fxSPR;
    public int idEnemy = 0;
    [SerializeField] int type;
    int idEnemyCurrent = -1;
    int HpMax = 100;
    int Hp = 0;
    Vector3 enemyPos;
    //--------- Enemy Stage -----------
    public int stage,currentStage=-1;
    public float speedMove,speedAtk;   
	public int FrameCurrent = 0;	        
	float frameTimer = 0;    
    bool change;
    //--------- Enemy Combat -----------
    [SerializeField] CapsuleCollider2D mCollider;
    [SerializeField] Rigidbody2D mRB;
    int dir;
    bool canKnock;
    //--------- Enemy Movement -----------
    [SerializeField] BoxCollider2D groundCheck;
    [SerializeField] CapsuleCollider2D wallCheck;

    void Load_Enemy(){
        if (idEnemyCurrent == idEnemy ) return;
        string resPath = "Enemy_Load/Enemy/Enemy " + idEnemy;
        this.enemy_SO = Resources.Load<Enemy_SO>(resPath);
        mPaint.LoadSprite(ref sprites,enemy_SO.textures,mPaint.BOTTOM|mPaint.HCENTER);
        mSPR.GetComponent<SpriteRenderer>().sprite = sprites[0];
        int w = sprites[0].texture.width;
        int h = sprites[0].texture.height;
        //--------- Set Collider-----------
        mCollider.size = new Vector2((float)(w - w/5)/100,(float)(h)/100);
        mCollider.offset = new Vector2 (0 ,(float) h/200);
        
        groundCheck.size = new Vector2(0.1f,0.2f);
        groundCheck.offset = new Vector2((float)(w - w/5)/200,-0.1f);

        wallCheck.size = new Vector2(0.3f,0.1f);
        wallCheck.offset = new Vector2 ((float)(w - w/5)/200,0.2f);

        //--------------------
        type = enemy_SO.type;
        speedMove = enemy_SO.speedMove;
        speedAtk = enemy_SO.speedAtk;
        HpMax = enemy_SO.Hp;
        Hp = HpMax;
        enemyPos = gameObject.transform.localPosition;
        idEnemyCurrent = idEnemy;
    }
    private void OnTriggerEnter2D(Collider2D other) { 
        if(other.tag != "Dame") return;               
        int a =  other.transform.parent.GetComponent<PlayerCombat>().Dame;
        Debug.Log("dame" + gameObject.name + "||  by: " + other.name);
        if(other.transform.parent.localPosition.x > transform.localPosition.x) dir = -1;
        else dir = 1;
        TakeDame(a);        
    }
    void LoadComponent(){
        if(mSPR == null) mSPR = transform.Find("Draw_Enemy").gameObject;
        if(fxSPR == null) fxSPR = transform.Find("Draw_FX").gameObject;
        if(mCollider == null) mCollider = GetComponent<CapsuleCollider2D>();
        if(groundCheck == null) groundCheck = transform.Find("GroundDetected").GetComponent<BoxCollider2D>();
        if(wallCheck == null) wallCheck = transform.Find("WallDetected").GetComponent<CapsuleCollider2D>();
        if(mRB == null) mRB = GetComponent<Rigidbody2D>();
    }
    private void Reset() {
        LoadComponent();
        Load_Enemy();
    }
    private void Awake() {
        LoadComponent();
        Load_Enemy();
    }
    private void Update() {
        Load_Enemy();
        StageEnemy(stage);
    }
    void TakeDame(int HP){
        if(HpMax > 0){
            HpMax -= HP;
            stage = 3;
        }else{
            stage = 4;
        }
    }
    private void Paint(int frameCurrent){
        mSPR.GetComponent<SpriteRenderer>().sprite = sprites[frameCurrent];
    }
    void StageEnemy(int stage){
        if(currentStage != stage){			
			frameTimer = 99f;
			FrameCurrent = 0;
			currentStage = stage;
		}
        switch(currentStage){
			
			case 0:
				EnemyIdle();
				break;
			case 1:
                EnemyMove();
                break;	
			case 2:
                EnemyAttack();
                break;					
            case 3:
				EnemyInjure();
				break;
            case 4:
				EnemyDie();
				break;
            case 5:
				EnemyAppear();
				break;			
            default:
        	    break;        
        }
    }    
    private void EnemyIdle()
    {
        if(FrameRate(1/speedMove)) return;
        Paint(0);  
    }    
    private void EnemyAttack(){
        if(FrameRate(0.6f/speedMove)) return;
        if(sprites.Length > 3) {            
            switch (FrameCurrent)
            {
                case 0:
                    Paint(0);
                    break;
                case 1:
                    Paint(3);
                    break;
                case 2:
                    Paint(1);
                    break;
                default:
                    stage = 1;
                    break;
            } 
            FrameCurrent++;
        }
    }
    private void EnemyMove(){
        if(FrameRate(0.6f/speedMove)) return;
        switch(type){			
			case 0:
				Paint(0);
				break;
            case 1:
                Paint(FrameCurrent);
                FrameCurrent = ( FrameCurrent + 1)%2;                
                break;
            case 2:
                break;
            default:
                Paint(0);
				break;           
        }
    }
    void EnemyInjure(){
        if(FrameRate(0.3f)) return;           
           if(FrameCurrent == 0) {
            Paint(2);
            mRB.velocity = new Vector2(dir,2f);
            }
           else {
            stage = 1;
            mRB.velocity = new Vector2(0,0);
            return;
            }
            FrameCurrent++;    
    }
    void EnemyDie(){
        if(FrameRate(5f)) return;
            if(FrameCurrent == 0){
                Paint(2);
                mRB.velocity = new Vector2(2*dir,4f);
                mCollider.enabled = false;
            }else{
                stage = 5;
                return;
            }
            FrameCurrent++;
    }
    void EnemyAppear(){
            if(FrameCurrent == 0){
                gameObject.SetActive(true);
                mRB.velocity = new Vector2(0,0);
                mCollider.enabled = true;
                gameObject.transform.localPosition = enemyPos;                
                Hp = HpMax;
                stage = 1;
            }
    }
    private void OnBecameInvisible()
    {
        if(stage == 4) gameObject.SetActive(false);       
    }
    bool FrameRate(float speed){        
        frameTimer += Time.deltaTime;
        if(frameTimer >= speed){
            frameTimer = 0;
            return false;
        }
        return true;
    }
}
