using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class anim_sprite
{
    public static int HCENTER = 1;
    public static int VCENTER = 2;
    public static void Paint(ref GameObject gameObject,Texture2D text,float x, float y,int type){
        x *= 4;
        y *= 4;
        Vector3 move = new Vector3(x/100,y/100,0);
		gameObject.transform.localPosition = move;
        gameObject.GetComponent<SpriteRenderer>().sprite = Draw_prite(text,type);
    }

    public static Sprite Draw_prite(Texture2D text,int type){
        float anchorX = 0f;
        float anchorY = 1f;
        if ((type & HCENTER) == HCENTER)
		{
			anchorX = 0.5f;
            //Debug.Log("okx");
		}
		if ((type & VCENTER) == VCENTER)
		{
           // Debug.Log("oky");

			anchorY = 0.5f;
        }     
        return Sprite.Create(text, new Rect(0, 0, text.width, text.height), new Vector2(anchorX,anchorY));
    }        
}

