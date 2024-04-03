using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class CharCtrl : MonoBehaviour
{
    LoadImage loadImage;
	PlayerMovement player;
    [SerializeField] GameObject Body;
    [SerializeField] GameObject Leg;
    [SerializeField] GameObject Head;
	[SerializeField] GameObject Wp;

    public int cf = 0;
    int nextFrame = -1;
    public int zoomlv = 4;
    public int Frame = 25;
	[SerializeField] int FrameStart = 0;
	public int State = 4;

	int stagejump = 0;
	float frameTimer = 0;
    public static readonly int[][][] CharInfo = new int[30][][]
    
	{
		new int[4][]
		{
			new int[3] { 0, -10, 32 },
			new int[3] { 1, -7, 7 },
			new int[3] { 1, -11, 15 },
			new int[3] { 1, -9, 45 }
		},
		new int[4][]
		{
			new int[3] { 0, -10, 33 },
			new int[3] { 1, -7, 7 },
			new int[3] { 1, -11, 16 },
			new int[3] { 1, -9, 46 }
		},
		new int[4][]
		{
			new int[3] { 1, -10, 33 },
			new int[3] { 2, -10, 11 },
			new int[3] { 2, -9, 16 },
			new int[3] { 1, -12, 49 }
		},
		new int[4][]
		{
			new int[3] { 1, -10, 32 },
			new int[3] { 3, -11, 9 },
			new int[3] { 3, -11, 16 },
			new int[3] { 1, -13, 47 }
		},
		new int[4][]
		{
			new int[3] { 1, -10, 34 },
			new int[3] { 4, -9, 9 },
			new int[3] { 4, -8, 16 },
			new int[3] { 1, -12, 47 }
		},
		new int[4][]
		{
			new int[3] { 1, -10, 34 },
			new int[3] { 5, -11, 11 },
			new int[3] { 5, -10, 17 },
			new int[3] { 1, -13, 49 }
		},
		new int[4][]
		{
			new int[3] { 1, -10, 33 },
			new int[3] { 6, -9, 9 },
			new int[3] { 6, -8, 16 },
			new int[3] { 1, -12, 47 }
		},
		new int[4][]
		{
			new int[3] { 0, -9, 36 },
			new int[3] { 7, -5, 15 },
			new int[3] { 7, -10, 21 },
			new int[3] { 1, -8, 49 }
		},
		new int[4][]
		{
			new int[3] { 4, -13, 26 },
			new int[3],
			new int[3],
			new int[3]
		},
		new int[4][]
		{
			new int[3] { 5, -13, 25 },
			new int[3],
			new int[3],
			new int[3]
		},
		new int[4][]
		{
			new int[3] { 6, -12, 26 },
			new int[3],
			new int[3],
			new int[3]
		},
		new int[4][]
		{
			new int[3] { 7, -13, 25 },
			new int[3],
			new int[3],
			new int[3]
		},
		new int[4][]
		{
			new int[3] { 0, -9, 35 },
			new int[3] { 8, -4, 13 },
			new int[3] { 8, -14, 27 },
			new int[3] { 1, -9, 49 }
		},
		new int[4][]
		{
			new int[3] { 0, -9, 31 },
			new int[3] { 9, -11, 8 },
			new int[3] { 10, -10, 17 },
			new int[3]
		},
		new int[4][]
		{
			new int[3] { 2, -7, 33 },
			new int[3] { 9, -11, 8 },
			new int[3] { 11, -8, 15 },
			new int[3]
		},
		new int[4][]
		{
			new int[3] { 2, -8, 32 },
			new int[3] { 9, -11, 8 },
			new int[3] { 12, -8, 14 },
			new int[3]
		},
		new int[4][]
		{
			new int[3] { 2, -7, 32 },
			new int[3] { 9, -11, 8 },
			new int[3] { 13, -12, 15 },
			new int[3]
		},
		new int[4][]
		{
			new int[3] { 0, -11, 31 },
			new int[3] { 9, -11, 8 },
			new int[3] { 14, -15, 18 },
			new int[3]
		},
		new int[4][]
		{
			new int[3] { 2, -9, 32 },
			new int[3] { 9, -11, 8 },
			new int[3] { 15, -13, 19 },
			new int[3]
		},
		new int[4][]
		{
			new int[3] { 2, -9, 31 },
			new int[3] { 9, -11, 8 },
			new int[3] { 16, -7, 22 },
			new int[3]
		},
		new int[4][]
		{
			new int[3] { 2, -9, 32 },
			new int[3] { 9, -11, 8 },
			new int[3] { 17, -11, 18 },
			new int[3]
		},
		new int[4][]
		{
			new int[3] { 3, -12, 34 },
			new int[3] { 8, -4, 13 },
			new int[3] { 8, -15, 25 },
			new int[3] { 1, -10, 46 }
		},
		new int[4][]
		{
			new int[3] { 0, -9, 32 },
			new int[3] { 8, -4, 9 },
			new int[3] { 10, -10, 18 },
			new int[3]
		},
		new int[4][]
		{
			new int[3] { 2, -7, 34 },
			new int[3] { 8, -4, 9 },
			new int[3] { 11, -8, 16 },
			new int[3]
		},
		new int[4][]
		{
			new int[3] { 2, -8, 33 },
			new int[3] { 8, -4, 9 },
			new int[3] { 12, -8, 15 },
			new int[3]
		},
		new int[4][]
		{
			new int[3] { 2, -7, 33 },
			new int[3] { 8, -4, 9 },
			new int[3] { 13, -12, 16 },
			new int[3]
		},
		new int[4][]
		{
			new int[3] { 0, -11, 32 },
			new int[3] { 7, -5, 9 },
			new int[3] { 14, -15, 19 },
			new int[3]
		},
		new int[4][]
		{
			new int[3] { 2, -9, 33 },
			new int[3] { 7, -5, 9 },
			new int[3] { 15, -13, 20 },
			new int[3]
		},
		new int[4][]
		{
			new int[3] { 2, -9, 32 },
			new int[3] { 7, -5, 9 },
			new int[3] { 16, -7, 23 },
			new int[3]
		},
		new int[4][]
		{
			new int[3] { 2, -9, 33 },
			new int[3] { 7, -5, 9 },
			new int[3] { 17, -11, 19 },
			new int[3]
		}
	};
	
	private void Start() {   
        loadImage = GetComponent<LoadImage>();
		player = GetComponent<PlayerMovement>();		
    }
	
	private void Update() {
		stagejump = player.velocityView;
	}
    private void FixedUpdate() {
		
		//CharStage(player.indexStage);
		CharStage(State);
        LoadImage2();
    }
    void LoadImage2(){
        if(nextFrame == cf) return;
        nextFrame = cf;
        DrawImage(loadImage.spriteBody[CharInfo[cf][2][0]],CharInfo[cf][2][1],CharInfo[cf][2][2],Body);
        DrawImage(loadImage.spriteLeg[CharInfo[cf][1][0]],CharInfo[cf][1][1],CharInfo[cf][1][2],Leg);
        DrawImage(loadImage.spriteHead[CharInfo[cf][0][0]],CharInfo[cf][0][1],CharInfo[cf][0][2],Head);
		DrawImage(loadImage.spriteWepon[CharInfo[cf][3][0]],CharInfo[cf][3][1]-4,CharInfo[cf][3][2]-28,Wp);
    }
    private void DrawImage(Sprite sprite, int x, int y,GameObject gameObject)
    {
        float x0 = x*zoomlv;
        float y0 = y*zoomlv;
        gameObject.GetComponent<SpriteRenderer>().sprite = sprite;
		Vector3 move = new Vector3(x0/sprite.pixelsPerUnit,y0/sprite.pixelsPerUnit,0);
		gameObject.transform.localPosition = move;
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
		frameTimer += Time.fixedDeltaTime;
        if(frameTimer >= (float)1/Frame){
            frameTimer = 0; 
			FrameStart = (FrameStart + 1) % 4;  
			cf = FrameStart+13;      
        }	
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
