using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using System.Threading;

public class CharCtrl : MonoBehaviour
{   
	bool canChangeStage = true;
	bool atking = true;
    public int Frame = 25;
	public int State = 0;
	public int skillId = 0;
	public int skillCurrent = -1;
	public int[] FrameSkillStand;
	public int[] FrameSkillFly;
	public static int cf = 0;
	int stagejump = 0;
	int FrameStart = 0;	        
	float frameTimer = 0;
	[SerializeField] float speedAttack = .5f;
	public SkillInfoPaint[] skillStand;
	public SkillInfoPaint[] skillfly;
	private Coroutine loopingCoroutine;
	Test_SpriteFX SpriteSkill;
	[SerializeField] GameObject skill0;
    [SerializeField] GameObject skill1;
    [SerializeField] GameObject skill2;
	SkillInfor eff0;
	int i0;
	SkillInfor eff1;
	int i1;
	SkillInfor eff2;
	int i2;	
	void LoadObjSkill(){
		skill0 = transform.GetChild(1).gameObject.transform.GetChild(0).gameObject;
		skill1 = transform.GetChild(1).gameObject.transform.GetChild(1).gameObject;
		skill2 = transform.GetChild(1).gameObject.transform.GetChild(2).gameObject;
		SpriteSkill = gameObject.GetComponent<Test_SpriteFX>();
	}
	void changeSkill(){
		if(skillId == skillCurrent) return;
		SkillPaint[] a = Read_anim_skill.skillPaints;
		SkillInfoPaint[] temp1 = new SkillInfoPaint[a[skillId].skillStand.Length];
		for (int i = 0; i < a[skillId].skillStand.Length; i++)
		{
			temp1[i] = a[skillId].skillStand[i];			
		}
		skillStand = temp1;
		SkillInfoPaint[] temp2 = new SkillInfoPaint[a[skillId].skillfly.Length];
		for (int i = 0; i < a[skillId].skillfly.Length; i++)
		{
			temp2[i] = a[skillId].skillfly[i];
		}
		skillfly = temp2;
		skillCurrent = skillId;
	}

	private void Awake() {
		LoadObjSkill();
	}
	private void Start() {		
		changeSkill();		
    }
    private void FixedUpdate() {
		changeSkill();
		if(canChangeStage){	
		State = PlayerMovement.indexStage;	
		CharStage(State);
		}	
    }
	        
	void CharStage(int a){
		switch(a){
			case 0:
				charIdle();
				break;
			case 1:
				charRun();
				break;
			case 2:
				charJump();
				break;	
			case 3:
				charFall();
				break;
			case 4:
				loopingCoroutine = StartCoroutine(charAttackStand());
				break;
			case 7:
				//FrameStart = 0;
				charRun2();
				break;
			default:
       		 	charIdle();
        	break;					
		}
	}
	void charRun(){
		frameTimer += Time.fixedDeltaTime;
        if(frameTimer >= (1f/3.2)/Frame){
            frameTimer = 0;
            FrameStart = ((FrameStart + 1)%5) + 2;
			cf = FrameStart;
        }
	}
	void charRun2(){
		int [] run = new int[]{13,14,15,16,16,13};
		
		frameTimer += Time.fixedDeltaTime;
        if(frameTimer >= (float)1/Frame){
            frameTimer = 0;
            FrameStart = (FrameStart + 1) % 6;
			cf = run[FrameStart];
        }
	}
	void charIdle(){
		frameTimer += Time.fixedDeltaTime;
        if(frameTimer >= (float)1/Frame){
            frameTimer = 0; 
			FrameStart = (FrameStart + 1) % 2;  
			cf = FrameStart;      
        }		
	}
	void charJump(){
			stagejump = PlayerMovement.velocityView;
			if 	(stagejump > 7 || stagejump == 0)
			{					
				cf = 7;
			}
			else if ( stagejump >= 5 && stagejump <= 7)
			{
				cf = 8;
			}
			else if (stagejump >= 2 && stagejump <= 4)
			{
				cf = 9;
			}
			else if (stagejump >= -1 && stagejump <= 1)
			{
				cf = 10;
			}
			else if (stagejump >= -4 && stagejump <= -2)
			{
				cf = 11;
			} else			
			{
			cf = 12;
			}
	}
	void charFall(){
		cf = 12;
	}
	IEnumerator charAttackStand(){
		
		canChangeStage = false;
        for (int i = 0; i < skillStand.Length; i++)
		{
			cf = skillStand[i].status;
			if(skillStand[i].effS0Id != 0){
				SpriteSkill.idSkill = skillStand[i].effS0Id - 1;
				SpriteSkill.LoadTexAo_SO();
				Debug.Log(skillStand[i].effS0Id);
				eff0 = Read_FX_Skill.skillInfors[skillStand[i].effS0Id-1];
				i0=0;
			}
			if(skillStand[i].effS1Id != 0){
				
				SkillInfor eff1 = Read_FX_Skill.skillInfors[skillStand[i].effS1Id];
				i1=0;
			}
			if(eff0!=null){
				Debug.Log(i0);
				//Debug.Log("i0["+i0+"] : { "+eff0.info[i0].dx + " ; " + eff0.info[i0].dy + " }");
				skill0.SetActive(true);
				DrawSkill(SpriteSkill.spriteFxs[i0],eff0.info[i0].dx,eff0.info[i0].dy,skill0);
				
				i0++;
				if (i0 >= eff0.info.Length)
					{	
						skill0.SetActive(false);
						eff0 = null;
						i0 = 0;
					}
			}			
			yield return new WaitForSeconds(speedAttack);
		}
		State = 0;
		canChangeStage = true;
		StopCoroutine(loopingCoroutine);
		}
	private void DrawSkill(Sprite sprite, int x, int y,GameObject _gameObject)
    {
		
        float x0 = x*4;
        float y0 = -y*4;
		
        _gameObject.GetComponent<SpriteRenderer>().sprite = sprite;
		Vector3 move = new Vector3(x0/100,y0/100,0);
		_gameObject.transform.localPosition = move;
    } 
}
