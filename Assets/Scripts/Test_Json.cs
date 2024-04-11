using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_Json : MonoBehaviour
{
    public Texture2D[] tex2D;
    Sprite spritea;
    SpriteRenderer SR;
    public int cf = 0;
    public int id = 0;
    float frameTimer = 0;
    public int Frame = 10;
    public int FrameStart = 0;
    public static readonly int[][][] SkillInfo = new int[6][][]
    
	{
		new int[1][]
		{
			new int[3] { 0, 0, 0 },
		},
        new int[1][]
		{
			new int[3] { 1, 6, -6 },
		},
        new int[1][]
		{
			new int[3] { 2, 14, -10 },
		},
        new int[1][]
		{
			new int[3] { 3, 2, -17 },
		},
        new int[1][]
		{
			new int[3] { 4, -15, -16 },
		},
        new int[1][]
		{
			new int[3] { 5, 9, -12 },
		},
    };
    private void Start() {
        SR = GetComponent<SpriteRenderer>();
    }
    private void FixedUpdate() {
        frameTimer += Time.fixedDeltaTime;
        if(frameTimer >= (float)1/Frame){
            frameTimer = 0; 
			FrameStart = (FrameStart + 1) % 6;  
			cf = FrameStart;      
        }
        id = SkillInfo[cf][0][0];
        DrawImage(tex2D[SkillInfo[cf][0][0]],SkillInfo[cf][0][1],SkillInfo[cf][0][2]);
    }
    private void DrawImage(Texture2D tex, int x, int y)
    {
        float x0 = x*4;
        float y0 = -y*4;
        SR.sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(.5f,0.5f));;
		Vector3 move = new Vector3(x0/SR.sprite.pixelsPerUnit,y0/SR.sprite.pixelsPerUnit,0);
		transform.localPosition = move;
    } 
}
