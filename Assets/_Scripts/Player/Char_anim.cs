using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Char_anim : MonoBehaviour
{
    [SerializeField] Draw_Char drawChar;
	[SerializeField] Weapon skill;
	[Header("ID_Stage")]
	public mState state;
	mState currentState;
	public bool isFly;
	public bool canChangStage = true;
	public int FrameCurrent = 0;	        
	float frameTimer = 0;
    public float stagejump = 0 ;
	public float speedRun = 5 ;
	float stateVy1, stateVy2, stateVy3, stateVy4;
	public void SetStateVy(float vY){
		float step = (Mathf.Abs(vY)*0.6f)/4;
		stateVy1 = step;
		stateVy2 = step*2;
		stateVy3 = step*3;
		stateVy4 = step*4;
	}
    private void Awake() {
        LoadCompnents();
    }
    private void Reset() {
        LoadCompnents();
    }
    void LoadCompnents(){
        if(drawChar == null) drawChar = transform.Find("Sprite").gameObject.GetComponent<Draw_Char>();
    }
	public void setSkill( Weapon _skill ){
		skill = _skill;
	}

    private void Update() {
        CharStage(state);
    }
    void CharStage(mState mStage){
		if(currentState != mStage){
			if(mStage != mState.None){
			
				if(canChangStage){

					stagejump = 999;
					drawChar.OffDust();
					frameTimer = 99f;
					FrameCurrent = 0;
					currentState = mStage;

				}
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
				drawChar.cf = 30;
				break;
			case mState.Climb:
				charClimb();
				break;
			case mState.Attack:
				charAttack();
				break;
			case mState.Dash:
				drawChar.cf = 7;
				break;
			case mState.Knockback:
				charKnockback();
				break;
			case mState.Dead:
				break;
			default:
       		 	charIdle();
        	break;					
		}
	}

    private void charKnockback()
    {
        drawChar.cf = 21;
    }

    void charRun(){
		frameTimer += Time.deltaTime;
        if(TimeRate((1f)/(3*speedRun/2))){

			drawChar.PainDust(FrameCurrent);
			drawChar.cf = FrameCurrent + 2;
            FrameCurrent = ((FrameCurrent + 1)%5);
			
			if(FrameCurrent == 4) drawChar.OnDust();				
        }		
	}
	void charIdle(){
		
		frameTimer += Time.deltaTime;
        if(TimeRate(1f/(3))){
			drawChar.cf = FrameCurrent; 
			FrameCurrent = (FrameCurrent + 1) % 2; 						     
        }
	}
	void charClimb(){		
		frameTimer += Time.deltaTime;
        if(TimeRate(1f/(5))){
			drawChar.cf = FrameCurrent; 
			FrameCurrent = (FrameCurrent + 1) % 2+30; 						     
        }
	}
	void charJumpMax(){

			if 	(stagejump > stateVy4 )
			{					
				drawChar.cf = 7;
			}
			else if (stagejump > stateVy3 && stagejump <= stateVy4)
			{
				drawChar.cf = 8;
			}
			else if (stagejump > stateVy2 && stagejump <= stateVy3)
			{
				drawChar.cf = 9;
			}
			else if (stagejump > stateVy1 && stagejump <= stateVy2 )
			{
				drawChar.cf = 10;
			}
			else if ( stagejump > 0 && stagejump <= stateVy1)
			{
				drawChar.cf = 11;
			} else if(stagejump < 0 )		
			{
				drawChar.cf = 12;
			}

	}
	void charJumpMin(){
		if(stagejump < 0 )
		{					
			drawChar.cf = 12;
		}else{
			drawChar.cf = 7;
		}
	}
	void charFall(){
		if(stagejump > 0 )
		{					
			drawChar.cf = 33;
		}else{
			drawChar.cf = 12;
		}
	}
	void charAttack(){

		if(skill.hasWeapon == false) return;
		frameTimer += Time.deltaTime;
        if(TimeRate( 0.1f )){			
			skill.AttackWeapon(FrameCurrent);
			drawChar.cf = isFly ? skill.cf + 9 : skill.cf;
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
}
