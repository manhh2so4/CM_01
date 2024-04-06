using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using System.Threading;

public class CharCtrl : MonoBehaviour
{

    public static int cf = 0;
    public int Frame = 25;
	[SerializeField] int FrameStart = 0;
	public int State = 0;
    private bool atking = true;
    int stagejump = 0;
	float frameTimer = 0;
	bool canChangeStage = true;	
	private void Start() { 	
    }
	private void Update() {
		stagejump = PlayerMovement.velocityView;
		if(Input.GetButtonDown("Fire1")){
            Attack(); 
        }
	}
	void Attack(){
		if(!atking) return;
        State = 4;
        atking = false;
    }
    private void FixedUpdate() {
		if(canChangeStage){	
		//State = PlayerMovement.indexStage;	
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
				//FrameStart = 0;
				Debug.Log("attack");
				charAttack1();
				break;
			case 5:
				//FrameStart = 0;
				charAttack2();
				break;
			case 6:
				//FrameStart = 0;
				charAttack3();
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
	void charAttack1(){
		canChangeStage = false;
		int [] run = new int[]{13,14,15,16,16,13};
        for (int i = 0; i < run.Length; i++)
		{
			cf = run[i];
			Thread.Sleep(5000);
		}
		State = 0;
		atking = true;
		canChangeStage = true;
	}
	void charAttack2(){		
		frameTimer += Time.fixedDeltaTime;
        if(frameTimer >= (float)1/Frame){
            frameTimer = 0; 
			FrameStart = ((FrameStart+1)%4);  
			cf = FrameStart+17;      
        }		
	}
	void charAttack3(){		
		frameTimer += Time.fixedDeltaTime;
        if(frameTimer >= (float)1/Frame){
            frameTimer = 0; 
			FrameStart = ((FrameStart+1)%2);  
			cf = FrameStart+17;      
        }		
	}
}
