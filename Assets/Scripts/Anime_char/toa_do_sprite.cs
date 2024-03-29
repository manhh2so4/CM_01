using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class toa_do_sprite : MonoBehaviour
{
    [Header("toa_do")]
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
    public int cf = 0;
    public float cx = Screen.width/2;
    public float cy = Screen.height/2;
    public int zoomlv = 1;
	public Texture2D textTest;
	public Sprite sprite_leg = null; 
	public SpriteRenderer spriteRen;
     void OnGUI()
    {          
        Rect buttonRect = new Rect(10, 10, 100, 50);
        Rect buttonRect2 = new Rect(10, 80, 100, 50);
          //__drawRegion(imgHead[CharInfo[cf][0][0]],CharInfo[cf][0][1],CharInfo[cf][0][2]);
          //__drawRegion(imgLeg[CharInfo[cf][1][0]],CharInfo[cf][1][1],CharInfo[cf][1][2]);
		  //__drawRegion(imgBody[CharInfo[cf][2][0]],CharInfo[cf][2][1],CharInfo[cf][2][2]);
		 //LoadSprite(imgLeg[CharInfo[cf][1][0]],CharInfo[cf][1][1],CharInfo[cf][1][2]);
                    
    }

	public  void Start() {
		spriteRen = GetComponent<SpriteRenderer>();

	}

	private void Update() {
		//LoadSprite(imgBody[CharInfo[cf][2][0]],CharInfo[cf][2][1],CharInfo[cf][2][2]);
	}

    private void LoadSprite(Texture2D texture2D, int x, int y)
    {

        if(sprite_leg != null && sprite_leg.texture == texture2D)  return;
		__DrawSprite2(texture2D,x,y);
    }

    void __drawRegion(Texture2D image, float x, float y){
        int w = 0;
        int h = 0;
        if(image == null){
            w = 0;
            h = 0;

        }else{
            w = image.width;
            h = image.height;
        }
            float x0 = cx+x*zoomlv;
            float y0 = cy-y*zoomlv;
        GUI.DrawTexture(new Rect(x0, y0, w, h), image);
    }	
	void __DrawSprite2(Texture2D image, float x, float y){
		float x0 = x*zoomlv;
        float y0 = -y*zoomlv;
		sprite_leg = Sprite.Create(image, new Rect(0, 0, image.width, image.height), new Vector2(0.5f,0.5f));
		Vector3 move = new Vector3(x0/sprite_leg.pixelsPerUnit,y0/sprite_leg.pixelsPerUnit,0);
		transform.position = move;
		spriteRen.sprite = sprite_leg;
	}
}
