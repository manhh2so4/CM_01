using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class Draw_Boss : MonoBehaviour
{
    //--------- Boss Data -----------
    public EnemyBoss_SO enemyBoss_SO;
    Texture2D TEXTURE2D;   
    Sprite[] sprites;
    ImageInfor[] imageInfor;
    FrameBoss [] frameBosses;
    public int[] frameBossMove;
    public int[] frameBossAttack;
    [SerializeField] GameObject prefab;
    GameObject[] mSR;
    //--------- Boss Stage -----------
    [SerializeField] bool isSortLayer;
    public int idBoss;
    int idBossCurrent;
    public int currentStage = -1;
    public float speedMove;
    public float speedAtk;     
	public int FrameCurrent = 0;	        
	float frameTimer = 0;
    
    void Load_Enemy(){
        if (idBossCurrent == idBoss ) return;
        string resPath = "Enemy_Load/EnemyBig " + idBoss;
        this.enemyBoss_SO = Resources.Load<EnemyBoss_SO>(resPath);
        TEXTURE2D = enemyBoss_SO.TEXTURE2D;
        imageInfor = enemyBoss_SO.imageInfors;
        frameBosses = enemyBoss_SO.frameBoss;
        frameBossMove = enemyBoss_SO.frameBossMove;
        frameBossAttack = enemyBoss_SO.frameBossAttack;
        LoadSprite();
        isSortLayer = false;
        idBossCurrent = idBoss;
    }
    void StageBoss(int stage){
        if(currentStage != stage){			
			frameTimer = 99f;
			FrameCurrent = 0;
            isSortLayer = false;
			currentStage = stage;
		}
        switch(currentStage){
			case 0:
				BossIde();
				break;
			case 1:
				BossMove();
				break;
			case 2:
				BossAttack();
				break;
            case 10:
				DrawSprite(FrameCurrent);
				break;		
			default:
       		 	BossIde();
        	    break;
        }
    }
    void BossMove(){		
		frameTimer += Time.deltaTime;
        if(frameTimer >= speedMove/frameBossMove.Length){
            frameTimer = 0;
            if(FrameCurrent >= frameBossMove.Length){
                FrameCurrent = 0;               
            }
			DrawSprite(frameBossMove[FrameCurrent]);
			FrameCurrent += 1; 						     
        }		
	}
    void BossAttack(){		
		frameTimer += Time.deltaTime;
        if(frameTimer >= speedAtk/frameBossAttack.Length){
            frameTimer = 0;
            if(FrameCurrent >= frameBossAttack.Length){
                FrameCurrent = 0;               
            }
			DrawSprite(frameBossAttack[FrameCurrent]);
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
        GameObject[] tempSrs  = new GameObject[sprites.Length];
            if(mSR != null) SetFalse();
            for (int i = 0; i < sprites.Length; i++){
            try
            {
                if(mSR[i] != null){              
                mSR[i].GetComponent<SpriteRenderer>().sprite = sprites[i];
                mSR[i].SetActive(false);
                mSR[i].name = "Sp" + i;
                tempSrs[i] = mSR[i];
                }
            }
            catch (System.Exception)
            {
                GameObject subObject = Instantiate(prefab, transform);
                subObject.GetComponent<SpriteRenderer>().sprite = sprites[i];
                subObject.SetActive(false);
                subObject.name = "Sp" + i;
                tempSrs[i] = subObject; 
            } 
            }
            mSR = tempSrs;
        
    }

    private void Update() {
        Load_Enemy();
        StageBoss(currentStage);
    }
    void DrawSprite(int frameBoss){
        SetFalse();
        for (int i = 0; i < frameBosses[frameBoss].dx.Length; i++)
        {
            if(isSortLayer == false){
                mSR[frameBosses[frameBoss].idImg[i]].GetComponent<SpriteRenderer>().sortingOrder = i;
            }
            mSR[frameBosses[frameBoss].idImg[i]].SetActive(true);        
            Vector3 move = new Vector3( (frameBosses[frameBoss].dx[i]*4f)/100, (-frameBosses[frameBoss].dy[i]*4f)/100,0);
            mSR[frameBosses[frameBoss].idImg[i]].transform.localPosition = move;
        }
        isSortLayer = true;
        
              
    }
    void SetFalse(){
        for (int i = 0; i < mSR.Length; i++){
            mSR[i].SetActive(false);
        }
    }
}
