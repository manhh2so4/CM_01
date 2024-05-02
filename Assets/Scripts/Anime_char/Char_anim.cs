using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Char_anim : MonoBehaviour
{
    [SerializeField] Draw_Char drawChar;
	[SerializeField] Draw_skill draw_Skill;
	[SerializeField] PlayerMoment playerMoment;
	[Header("ID_Stage")]
	public int stage;
	int currentStage = -1;
	public bool canChangStage = true;
	public int Frame = 5;
	public int FrameStart = 0;	        
	float frameTimer = 0;
    public int stagejump =0 ;
	public float speedRun = 5 ;
	
	[Header("ID_Skill")]
	public int idSkill = 0;
	int curIdSkill = -1;
	int i0;
	int eff0Lenth;
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
		if(playerMoment == null) playerMoment = GetComponent<PlayerMoment>();
    }
    private void Update() {
        CharStage(stage);
    }
    void CharStage(int stage){
		if(currentStage != stage){
			if( canChangStage ){
			frameTimer = 99f;
			FrameStart = 0;
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
			drawChar.cf = FrameStart + 2;
            FrameStart = ((FrameStart + 1)%5);		
        }		
	}
	void charIdle(){
		
		frameTimer += Time.deltaTime;
        if(frameTimer >= (float)1/(Frame)){
            frameTimer = 0;
			drawChar.cf = FrameStart; 
			FrameStart = (FrameStart + 1) % 2; 						     
        }
		
	}
	void charJump(){
			if 	(stagejump > 5 )
			{					
				drawChar.cf = 7;
			}
			else if ( stagejump >= 3 && stagejump <= 5)
			{
				drawChar.cf = 8;
			}
			else if (stagejump >= 0 && stagejump <= 2)
			{
				drawChar.cf = 9;
			}
			else if (stagejump >= -3 && stagejump <= -1)
			{
				drawChar.cf = 10;
			}
			else if (stagejump >= -5 && stagejump <= -4)
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
			drawChar.cf = currentSkill[FrameStart].status;
			if(currentSkill[FrameStart].effS0Id !=0){
				eff0Lenth = draw_Skill.GetLenghtEff0();
				i0 = 0;
			}
			if(i0 <= eff0Lenth+1) {
				draw_Skill.PaintSkill(i0);
				i0++;
			}		
			
            FrameStart ++;
			if(FrameStart >= currentSkill.Length){
				draw_Skill.PaintSkill(i0);
				canChangStage = true;
				isAtacking = false;
				currentStage = -1;
				return;
			}
        }		
	}
}
