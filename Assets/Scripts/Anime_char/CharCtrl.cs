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
	private Coroutine loopingCoroutine;	
	void changeSkill(){
		if(skillId == skillCurrent) return;
		SkillPaint[] a = Read_anim_skill.skillPaints;
		int[] temp1 = new int[a[skillId].skillStand.Length];
		for (int i = 0; i < a[skillId].skillStand.Length; i++)
		{
			temp1[i] = a[skillId].skillStand[i].status;
			
		}
		FrameSkillStand = temp1;
		int[] temp2 = new int[a[skillId].skillfly.Length];
		for (int i = 0; i < a[skillId].skillfly.Length; i++)
		{
			temp2[i] = a[skillId].skillfly[i].status;
		}
		FrameSkillFly = temp2;
		skillCurrent = skillId;
	}
	private void Reset() {
		
	}
	private void Start() {
		changeSkill();
		loopingCoroutine = StartCoroutine(charAttackStand());		
    }
	private void Update() {	
		stagejump = PlayerMovement.velocityView;
	}
	void Attack(){
		if(!atking) return;
        State = 4;
        atking = false;
    }
    private void FixedUpdate() {
		changeSkill();
		if(canChangeStage){	
		//State = PlayerMovement.indexStage;	
		//CharStage(4);
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
			if 	(stagejump > 3 || stagejump ==0)
			{
				cf = 7;
			}
			else if ( stagejump >= 3 && stagejump <= 11)
			{
				cf = 8;
			}
			else if (stagejump >= 1 && stagejump <= 2)
			{
				cf = 9;
			}
			else if (stagejump >= -1 && stagejump <= 0)
			{
				cf = 10;
			}
			else if (stagejump >= -3 && stagejump <= -2)
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
        for (int i = 0; i < FrameSkillStand.Length; i++)
		{
			cf = FrameSkillStand[i];
			yield return new WaitForSeconds(0.1f);
		}
		State = 0;
		canChangeStage = true;
		//StopCoroutine(loopingCoroutine);
	}	
}
