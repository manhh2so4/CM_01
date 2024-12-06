using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class Draw_boss : MonoBehaviour
{
    public Action OnAttackDone;
    //--------- Boss Data -----------
    [Expandable]
    public EnemyBoss_SO DataBoss_SO;
    Texture2D TEXTURE2D;   
    Sprite[] sprites;
    ImageInfor[] imageInfor;
    [SerializeField] FrameBoss[] frameBosses;
    public int[] frameBossMove;
    public BossAttack[] bossAttacks;
    [SerializeField] GameObject prefab;
    [SerializeField] SpriteRenderer[] mSR;
    [SortingLayer]
    public string layerID;
    public LayerMask LayerCombat;
    public Transform playerCheck;

    //--------- Boss Stage -----------
    [SerializeField] bool isSortLayer;
    public int idBoss;
    public StateEnemy currentStage = StateEnemy.None;
    public StateEnemy state;
    public float speedImgMove;
    public float speedImgAtk;     
	public int FrameCurrent = 0;	        
	float frameTimer = 0;
    [Button("Draw_boss")] 
	private void drawboss(){
        mSR = null;
        Load_Enemy();
        state = StateEnemy.Idle;
        StageBoss();
    }
    [Button("paint")] 
    void drawBoss(){
        DrawSprite(FrameCurrent);
    }
    
    void Load_Enemy(){
        TEXTURE2D = DataBoss_SO.TEXTURE2D;
        imageInfor = DataBoss_SO.imageInfors;
        frameBosses = DataBoss_SO.frameBoss;
        frameBossMove = DataBoss_SO.frameBossMove;
        bossAttacks = DataBoss_SO.BossAttacks;
        LoadSprite();
        isSortLayer = false;
    }
    private void Awake() {
        Load_Enemy();
    }
    void StageBoss(){
        if(currentStage != state){			
			frameTimer = 99f;
			FrameCurrent = 0;
            isSortLayer = false;
			currentStage = state;
		}
        switch(currentStage){
			case StateEnemy.Idle:
				BossIde();
				break;
			case StateEnemy.Moving:
				BossMove();
				break;
            case StateEnemy.Jump:
				DrawSprite(2);
				break;
            case StateEnemy.Hold:
				DrawSprite(4);
				break;
            case StateEnemy.Dash:
				DrawSprite(5);
				break;
            case StateEnemy.Fall:
				DrawSprite(9);
				break;
            case StateEnemy.Attack1:
				BossAttack(2);
				break;
            case StateEnemy.Attack2:
				BossAttack(3);
				break;
			case StateEnemy.Skill_1:
				BossAttack(0);
				break;
            case StateEnemy.Skill_2:
				BossAttack(1);
				break;	
            case StateEnemy.Respawn:
				DrawSprite(FrameCurrent);
				break;
			default:
       		 	BossIde();
        	    break;
        }
    }
    void BossMove(){		
		frameTimer += Time.deltaTime;
        if(frameTimer >= 1f/ (speedImgMove*4) ){
            frameTimer = 0;
            if(FrameCurrent >= frameBossMove.Length){
                FrameCurrent = 0;               
            }
			DrawSprite(frameBossMove[FrameCurrent]);
			FrameCurrent += 1; 						     
        }		
	}
    void BossAttack(int index){		
		frameTimer += Time.deltaTime;
        if(frameTimer >= speedImgAtk/bossAttacks[index].attack.Length ){
            frameTimer = 0;
            if(FrameCurrent >= bossAttacks[index].attack.Length){
                OnAttackDone?.Invoke();
                Debug.Log("donw attack");
                state = StateEnemy.Idle; 
                return;              
            }
			DrawSprite(bossAttacks[index].attack[FrameCurrent]);
			FrameCurrent += 1; 						     
        }		
	}
    void BossIde(){
        frameTimer += Time.deltaTime;
        if(frameTimer >= (float)1/5){
            frameTimer = 0;             
			DrawSprite(FrameCurrent);
            FrameCurrent = (FrameCurrent+1) % 2; 
        }
    }
    void LoadSprite(){
        mPaint.LoadSpriteRegion(ref sprites,imageInfor,TEXTURE2D, mPaint.TOP|mPaint.LEFT);

            SpriteRenderer[] tempSrs  = new SpriteRenderer[sprites.Length];
            
            for (int i = 0; i < sprites.Length; i++){
            try
            {
                if(mSR[i] != null){              
                mSR[i].GetComponent<SpriteRenderer>().sprite = sprites[i];
                
                mSR[i].gameObject.SetActive(false);
                mSR[i].name = "Sp" + i;
                tempSrs[i] = mSR[i];
                }
            }
            catch (System.Exception)
            {
                UnityEngine.GameObject subObject = Instantiate(prefab, transform.Find("Sprites"));
                SpriteRenderer subSPR = subObject.GetComponent<SpriteRenderer>();
                subSPR.sprite = sprites[i];
                subSPR.sortingLayerName = layerID ;
                subObject.SetActive(false);
                subObject.name = "Sp" + i;
                tempSrs[i] = subSPR; 
            } 
            }
            mSR = tempSrs;
    }

    private void Update() {
        StageBoss();
    }
    void DrawSprite(int frameBoss){
        SetFalse();
        for (int i = 0; i < frameBosses[frameBoss].dx.Length; i++)
        {
            if(isSortLayer == false){
                mSR[frameBosses[frameBoss].idImg[i]].GetComponent<SpriteRenderer>().sortingOrder = i;
            }
            mSR[frameBosses[frameBoss].idImg[i]].gameObject.SetActive(true);        
            Vector3 move = new Vector3( (frameBosses[frameBoss].dx[i]*4f)/100, (-frameBosses[frameBoss].dy[i]*4f)/100,0);
            mSR[frameBosses[frameBoss].idImg[i]].transform.localPosition = move;
        }
        isSortLayer = true;
        
    }
    void SetFalse(){
        for (int i = 0; i < mSR.Length; i++){
            mSR[i].gameObject.SetActive(false);
        }
    }
    private void OnTriggerEnter2D(Collider2D other) {
        
        if ( (LayerCombat.value & (1 << other.gameObject.layer)) != 0)
        {
            if (other.tag == "Enemy") return;
            playerCheck = other.transform;
        }
    }
}
