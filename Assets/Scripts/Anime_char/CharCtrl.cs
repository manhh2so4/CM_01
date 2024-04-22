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
	public static int cf = 0;
	int stagejump = 0;
	int FrameStart = 0;	        
	float frameTimer = 0;
	[SerializeField] float speedAttack = .5f;
	private Coroutine loopingCoroutine;
	SkillInfor eff0;
	int i0;
	SkillInfor eff1;
	int i1;

	[SerializeField] Skill_Ctrl skill_Ctrl;
	private void Awake() {
		skill_Ctrl = transform.GetChild(1).gameObject.GetComponent<Skill_Ctrl>();
	}
    private void FixedUpdate() {
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
				//loopingCoroutine = StartCoroutine(charAttackStand());
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
		skill_Ctrl.changeSkill();
		canChangeStage = false;
        for (int i = 0; i < skill_Ctrl.skillStand.Length; i++)
		{
			cf = skill_Ctrl.skillStand[i].status;
			if(skill_Ctrl.skillStand[i].effS0Id != 0){
				skill_Ctrl.SpriteSkill.idSkill = skill_Ctrl.skillStand[i].effS0Id - 1;
				skill_Ctrl.SpriteSkill.LoadTexAo_SO();
				eff0 = Read_FX_Skill.skillInfors[skill_Ctrl.skillStand[i].effS0Id-1];
				i0=0;
			}
			if(skill_Ctrl.skillStand[i].effS1Id != 0){
				
				SkillInfor eff1 = Read_FX_Skill.skillInfors[skill_Ctrl.skillStand[i].effS1Id];
				i1=0;
			}
			if(eff0!=null){
				Debug.Log(i0);
				//Debug.Log("i0["+i0+"] : { "+eff0.info[i0].dx + " ; " + eff0.info[i0].dy + " }");
				skill_Ctrl.skill0.SetActive(true);
				skill_Ctrl.DrawSkill(skill_Ctrl.SpriteSkill.spriteFxs[i0],eff0.info[i0].dx,eff0.info[i0].dy,skill_Ctrl.skill0);				
				i0++;
				if (i0 >= eff0.info.Length)
					{	
						skill_Ctrl.skill0.SetActive(false);
						eff0 = null;
						i0 = 0;
					}
			}			
			yield return new WaitForSeconds(speedAttack);
		}
		StopCoroutine(loopingCoroutine);
		State = 0;
		canChangeStage = true;		
		}
	
}
