using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class mPaint
{
    public static int HCENTER = 1;
    public static int VCENTER = 2;
	public static int LEFT = 4;
	public static int RIGHT = 8;
	public static int TOP = 16;
	public static int BOTTOM = 32;
    static float offsetX = 0;
    static float offsetY = 0;
    static Vector3 newPosition;
    
    public static void Paint(GameObject gameObject,Texture2D text,float x, float y,int anchor){
        x *= 4;
        y *= 4;
		gameObject.transform.localPosition = new Vector3(x/100,y/100,0);
        gameObject.GetComponent<SpriteRenderer>().sprite = Draw_prite(text,anchor);
    }
    public static void Paint(GameObject gameObject,Sprite sprite,int anchor){
        Paint(gameObject,sprite,0, 0, anchor);
    }
    public static void Paint(GameObject gameObject,Sprite sprite,float x, float y,int anchor){

        x *= 4;
        y *= 4;
        float h = sprite.rect.height/100;
        float w = sprite.rect.width/100;
        
        switch (anchor)
        {               
            case 0:
                offsetX += w/2;
                offsetY -= h/2;
            break;
            case 2:
                offsetY += h/2;
            break;
        }
        newPosition.x = x/100 + offsetX;
        newPosition.y = y/100 + offsetY;
        newPosition.z = 0;
		gameObject.transform.localPosition = newPosition;
        offsetX = 0;
        offsetY = 0;
        gameObject.GetComponent<SpriteRenderer>().sprite = sprite;
    }
    public static void LoadSprite(ref Sprite[] sprites,Texture2D[] texs,int anchor){
        Sprite[] temp = new Sprite[texs.Length];
        for (int i = 0; i < texs.Length; i++)
        {
            temp[i] = Draw_prite(texs[i],anchor);
        }
        sprites = temp;
    }

    public static void LoadSpriteRegion(ref Sprite[] sprites,ImageInfor[] infor,Texture2D texs,int anchor){
        Sprite[] temp = new Sprite[infor.Length];
        for (int i = 0; i < infor.Length; i++)
        {
            int x0 = infor[i].x0*4;
            int y0 = infor[i].y0*4;
            int w0 = infor[i].w*4;
            int h0 = infor[i].h*4;
            int w = texs.width;
            int h = texs.height;
            temp[i] = Sprite.Create(texs, new Rect(x0, h - y0 - h0 , w0, h0),   
            new Vector2(0,1));         
        }
        sprites = temp;
    }
    static Sprite Draw_prite(Texture2D text,int anchor){
        float anchorX = 0f;
        float anchorY = 1f;

        if ((anchor & HCENTER) == HCENTER) anchorX = 0.5f;
		if ((anchor & VCENTER) == VCENTER) anchorY = 0.5f;
        if ((anchor & RIGHT) == RIGHT) anchorX = 1f;
        if ((anchor & LEFT) == LEFT) anchorX = 0f;
        if ((anchor & TOP) == TOP) anchorY = 1f;
		if ((anchor & BOTTOM) == BOTTOM) anchorY = 0f;

        return Sprite.Create(text, new Rect(0, 0, text.width, text.height), new Vector2(anchorX,anchorY));
    }
            
}

