using NaughtyAttributes;
using UnityEngine;

public class PaintChar : CoreComponent {
#region Components PaintChar
	GameObject LegGO,HeadGO,BodyGO,DustGO,WpGO;
	SpriteInfo[] partBody = new SpriteInfo[18];
	SpriteInfo[] partHead = new SpriteInfo[8];
	SpriteInfo[] partLeg = new SpriteInfo[10];
	SpriteInfo[] partWP = new SpriteInfo[2];
	
	[SerializeField] Tex_NjPart_SO mTexAo;
   	[SerializeField] Tex_NjPart_SO mQuan;
   	[SerializeField] Tex_NjPart_SO mHead;
	[SerializeField] Tex_NjPart_SO mWp;
   	[SerializeField] int Cf_view;
   
   //--------- effect char --------
	Sprite[] spriteDust;
	[SerializeField] SmallEffect_SO mDust;
	int x = 0;
#endregion

   //--------- Data Char draw --------
#region Variable_anim
   	SKill skill;
   	public mState state;
	mState currentState;
	public bool isFly;
	public int FrameCurrent = 0;	        
	float frameTimer = 0;
  	public float stagejump = 0;
	[SerializeField]float stateVy1, stateVy2, stateVy3, stateVy4;
#endregion
   protected override void Awake()
   {
      base.Awake();
      LoadCompnents();
   }
   public override void LogicUpdate(){
		CharStage(state);
   }

#region Char_anim
   //---------- Firt setting ---------
   public void SetSkill(SKill _skill){
		skill = _skill;
   }
   public void SetStateVy(float vY){
		float step = (Mathf.Abs(vY)*0.6f)/4;
		stateVy1 = step;
		stateVy2 = step*2;
		stateVy3 = step*3;
		stateVy4 = step*4;
	}
   public void ResetAnim(){
		stagejump = 999;
		OffDust();
		frameTimer = 99f;
		FrameCurrent = 0;
		currentState = mState.None;	
	}
   //-------------------------------
   void CharStage(mState mStage){
		if(currentState != mStage){
			if(mStage != mState.None){
				ResetAnim();
				currentState = mStage;
			}
		}
		switch(currentState){
			case mState.Idle:
				charIdle();
				break;
			case mState.Moving:
				charRun();
				break;
			case mState.JumpMin:
				charJumpMin();
				break;
			case mState.JumpMax:
				charJumpMax();
				break;	
			case mState.InAir:
				charFall();
				break;
			case mState.Slide:
				Paint(30);
				break;
			case mState.Climb:
				charClimb();
				break;
			case mState.Attack:
				charAttack();
				break;
			case mState.Dash:
				Paint(7);
				break;
			case mState.Knockback:
				charKnockback();
				break;
			case mState.Dead:
				break;
			case mState.None:
				break;
			default:
       		 	
        	break;					
		}
	}
   private void charKnockback()
   {
      Paint(21);
   }
    void charRun(){
		frameTimer += Time.deltaTime;
        if(TimeRate((1f)/(3*5/2))){

			PainDust(FrameCurrent);
			Paint(FrameCurrent + 2);
            FrameCurrent = ((FrameCurrent + 1)%5);
			
			if(FrameCurrent == 4) OnDust();				
        }		
	}
	void charIdle(){
		
		frameTimer += Time.deltaTime;
        if(TimeRate(1f/(3))){
			Paint(FrameCurrent); 
			FrameCurrent = (FrameCurrent + 1) % 2; 						     
        }
	}
	void charClimb(){		
		frameTimer += Time.deltaTime;
        if(TimeRate(1f/(5))){
			Paint(FrameCurrent); 
			FrameCurrent = (FrameCurrent + 1) % 2+30; 						     
        }
	}
	void charJumpMax(){

			if 	(stagejump > stateVy4 )
			{					
				Paint(7);
			}
			else if (stagejump > stateVy3 && stagejump <= stateVy4)
			{
				Paint(8);
			}
			else if (stagejump > stateVy2 && stagejump <= stateVy3)
			{
				Paint(9);
			}
			else if (stagejump > stateVy1 && stagejump <= stateVy2 )
			{
				Paint(10);
			}
			else if ( stagejump > 0 && stagejump <= stateVy1)
			{
				Paint(11);
			} else if(stagejump < 0 )		
			{
				Paint(12);
			}

	}
	void charJumpMin(){
		if(stagejump < 0 )
		{					
			Paint(12);
		}else{
			Paint(7);
		}
	}
	void charFall(){
		if(stagejump > 0 )
		{					
			Paint(33);
		}else{
			Paint(12);
		}
	}
	void charAttack(){

		if(skill.hasWeapon == false) return;
			frameTimer += Time.deltaTime;
			if( TimeRate(0.08f) ){		
				skill.AttackWeapon(FrameCurrent);
				Paint(isFly ? skill.cf + 9 : skill.cf);
				FrameCurrent++;	
			}		
	}
   bool TimeRate(float timeWait){        
      frameTimer += Time.deltaTime;
      if(frameTimer >= timeWait){
         frameTimer = 0;
         return true;
         }
         return false;
   }
#endregion
#region PaintChar
   void LoadCompnents(){
		LegGO = transform.Find("Leg").gameObject;
		HeadGO = transform.Find("Head").gameObject;
		BodyGO = transform.Find("Body").gameObject;		
		WpGO = transform.Find("Wp").gameObject;
      	DustGO = transform.Find("Dust").gameObject;

		LegGO.GetComponent<SpriteRenderer>().sortingLayerID = core.SortingLayerID;
		HeadGO.GetComponent<SpriteRenderer>().sortingLayerID = core.SortingLayerID;
		BodyGO.GetComponent<SpriteRenderer>().sortingLayerID = core.SortingLayerID;
		WpGO.GetComponent<SpriteRenderer>().sortingLayerID = core.SortingLayerID;
		DustGO.GetComponent<SpriteRenderer>().sortingLayerID = core.SortingLayerID;

		partLeg = mQuan.spriteInfos;
		partBody = mTexAo.spriteInfos;
		partHead = mHead.spriteInfos;
		partWP = mWp.spriteInfos;

		LoadEffect_Trigger();
	}
   [Button("PaintChar")]
	void PaintCharTest(){
		LoadCompnents();		        
		Paint(Cf_view);
	}

   	public void SetBody( SpriteInfo[] spriteInfos){
		if(spriteInfos != null){
      		partBody = spriteInfos;
		}else{
			partBody = mTexAo.spriteInfos;
		}
   	}
   	public void SetLeg( SpriteInfo[] spriteInfos ){
		if(spriteInfos != null){
      		partLeg = spriteInfos;
		}else{
			partLeg = mQuan.spriteInfos;
		}
   	}
   	public void SetWeapon( SpriteInfo[] spriteInfos){
		if(spriteInfos != null){
			partWP = spriteInfos;
		}else{
			partWP = mWp.spriteInfos;
		}
   	}
	public void SetHead( SpriteInfo[] spriteInfos){
		if(spriteInfos != null){
			partHead = spriteInfos;
		}else{
			partHead = mHead.spriteInfos;
		}
	}

   void Paint(int cf){
		mPaint.Paint(BodyGO, partBody[StaticValue.CharInfo[cf][2][0]].sprite, StaticValue.CharInfo[cf][2][1] + partBody[StaticValue.CharInfo[cf][2][0]].dx, StaticValue.CharInfo[cf][2][2] - partBody[StaticValue.CharInfo[cf][2][0]].dy, 0);
		mPaint.Paint(HeadGO, partHead[StaticValue.CharInfo[cf][0][0]].sprite, StaticValue.CharInfo[cf][0][1] + partHead[StaticValue.CharInfo[cf][0][0]].dx, StaticValue.CharInfo[cf][0][2] - partHead[StaticValue.CharInfo[cf][0][0]].dy, 0);
		mPaint.Paint(LegGO,  partLeg[StaticValue.CharInfo[cf][1][0]].sprite,  StaticValue.CharInfo[cf][1][1] + partLeg[StaticValue.CharInfo[cf][1][0]].dx,  StaticValue.CharInfo[cf][1][2] - partLeg[StaticValue.CharInfo[cf][1][0]].dy, 0);
		if(partWP != null){
			mPaint.Paint(WpGO, partWP[StaticValue.CharInfo[cf][3][0]].sprite,   StaticValue.CharInfo[cf][3][1] + partWP[StaticValue.CharInfo[cf][3][0]].dx,  StaticValue.CharInfo[cf][3][2] - partWP[StaticValue.CharInfo[cf][3][0]].dy, 0);
		}
	}
   public void PainDust(int index){
		if(index == 0){
            x = 0;
        }
		x +=3;
		mPaint.Paint(DustGO , spriteDust[index],-5-x,0,3);
	}
   	public void OffDust(){
		DustGO.SetActive(false);
	}
	public void OnDust(){
		DustGO.SetActive(true);
	}
	void LoadEffect_Trigger(){
        mPaint.LoadSprite(ref spriteDust, mDust.textures, mPaint.BOTTOM|mPaint.HCENTER);
    }
#endregion
}