using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Char_anim : MonoBehaviour
{
    [SerializeField] Draw_Char drawChar;
	[SerializeField] Draw_skill draw_Skill;
	[Header("ID_Stage")]
	public int stage;
	int currentStage = -1;
	public bool canChangStage = true;
	public int Frame = 5;
	public int FrameCurrent = 0;	        
	float frameTimer = 0;
    public int stagejump =0 ;
	public float speedRun = 5 ;
	
	[Header("ID_Skill")]
	public int idSkill = 0;
	int curIdSkill = -1;
	int i0,dx0,dy0,eff0Lenth;
	int i1,dx1,dy1,eff1Lenth;
	int i2,dx2,dy2,eff2Lenth;
	public bool isAtacking;
	public float speedAttack1;
	[SerializeField] public Skill[] skills;
    [SerializeField] SkillInfo[] currentSkill;
    private void Awake() {
        LoadCompnents();
    }
    private void Reset() {
        LoadCompnents();
    }
    void LoadCompnents(){
		if(draw_Skill == null) draw_Skill = transform.GetChild(1).gameObject.GetComponent<Draw_skill>();
        if(drawChar == null) drawChar = transform.GetChild(0).gameObject.GetComponent<Draw_Char>();
    }
    private void Update() {
        CharStage(stage);
    }
    void CharStage(int stage){
		if(currentStage != stage){
			if( canChangStage ){
			frameTimer = 99f;
			FrameCurrent = 0;
			currentStage = stage;
			}
		}
		switch(currentStage){
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
				canChangStage = false;
				isAtacking= true;
				charAttack();
				break;
			case 7:
				break;
			default:
       		 	charIdle();
        	break;					
		}
	}
	void charRun(){
		frameTimer += Time.deltaTime;
        if(frameTimer >= (1f)/(Frame*speedRun/2)){
            frameTimer = 0;
			drawChar.cf = FrameCurrent + 2;
            FrameCurrent = ((FrameCurrent + 1)%5);		
        }		
	}
	void charIdle(){
		
		frameTimer += Time.deltaTime;
        if(frameTimer >= (float)1/(Frame)){
            frameTimer = 0;
			drawChar.cf = FrameCurrent; 
			FrameCurrent = (FrameCurrent + 1) % 2; 						     
        }
		
	}
	void charJump(){
			if 	(stagejump > 5 )
			{					
				drawChar.cf = 7;
			}
			else if ( stagejump >= 2 && stagejump <= 3)
			{
				drawChar.cf = 8;
			}
			else if (stagejump >= 0 && stagejump <= 1)
			{
				drawChar.cf = 9;
			}
			else if (stagejump >= -2 && stagejump <= -1)
			{
				drawChar.cf = 10;
			}
			else if (stagejump >= -4 && stagejump <= -3)
			{
				drawChar.cf = 11;
			} else			
			{
			drawChar.cf = 12;
			}
	}
	void charFall(){
		drawChar.cf = 12;
	}
	void charAttack(){
		if(curIdSkill != idSkill){
				currentSkill = skills[idSkill].skillStand;
				curIdSkill = idSkill;					
		}
		frameTimer += Time.deltaTime;
        if(frameTimer >= (speedAttack1 / currentSkill.Length )){
            frameTimer = 0;			
			if(FrameCurrent >= currentSkill.Length){
				draw_Skill.OffHitBox();
				draw_Skill.SetSkillOff();
				canChangStage = true;
				isAtacking = false;
				currentStage = -1;
				return;
			}
			if(FrameCurrent == 0){
				draw_Skill.OnHitBox(10);
			}
			
			drawChar.cf = currentSkill[FrameCurrent].status;
			// ----------------- effS0Id -------------
			if(currentSkill[FrameCurrent].effS0Id != 0){
				draw_Skill.LoadEff0(currentSkill[FrameCurrent].effS0Id);
				eff0Lenth = draw_Skill.GetLenghtEff0();
				i0 = (dx0 = (dy0 = 0));
			}

			if(i0 <= eff0Lenth) {
				if (dx0 == 0) dx0 = currentSkill[FrameCurrent].e0dx;
				if (dy0 == 0) dy0 = currentSkill[FrameCurrent].e0dy;									
				draw_Skill.PaintEff0(i0,dx0,dy0);
				i0++;
			}else eff0Lenth =-1;
			// ----------------- effS1Id -------------
			if(currentSkill[FrameCurrent].effS1Id != 0){
				draw_Skill.LoadEff1(currentSkill[FrameCurrent].effS1Id);
				eff1Lenth = draw_Skill.GetLenghtEff1();
				i1 = (dx1 = (dy1 = 0));
			}
			if(i1 <= eff1Lenth) {
				if (dx1 == 0) dx1 = currentSkill[FrameCurrent].e1dx;
				if (dy1 == 0) dy1 = currentSkill[FrameCurrent].e1dy;									
				draw_Skill.PaintEff1(i1,dx1,dy1);
				i1++;
			}else eff1Lenth =-1;
			// ----------------- effS2Id -------------
			if(currentSkill[FrameCurrent].effS2Id != 0){
				draw_Skill.LoadEff2(currentSkill[FrameCurrent].effS2Id);
				eff2Lenth = draw_Skill.GetLenghtEff2();
				i2 = (dx2 = (dy2 = 0));
			}

			if(i2 <= eff2Lenth) {
				if (dx2 == 0) dx2 = currentSkill[FrameCurrent].e2dx;
				if (dy2 == 0) dy2 = currentSkill[FrameCurrent].e2dy;									
				draw_Skill.PaintEff2(i2,dx2,dy2);
				i2++;
			}else eff2Lenth =-1;
			// ----------------------------------------
            FrameCurrent ++;
			
        }		
	}
}
