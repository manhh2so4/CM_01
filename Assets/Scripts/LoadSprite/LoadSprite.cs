using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadSprite : MonoBehaviour
{
    static int HCENTER = 1;
    static int VCENTER = 2;
    protected void CvtSprite(ref Sprite[] sprite,ref Texture2D[] text,int type){
        float anchorX = 0f;
        float anchorY = 1f;
        if ((type & HCENTER) == HCENTER)
		{
			anchorX = 0.5f;
            Debug.Log("okx");
		}
		if ((type & VCENTER) == VCENTER)
		{
            Debug.Log("oky");
			anchorY = 0.5f;
        }
        Sprite[] temp = new Sprite[text.Length];
       for (int i = 0; i < text.Length; i++)
        {            
            temp[i] = Sprite.Create(text[i], new Rect(0, 0, text[i].width, text[i].height), new Vector2(anchorX,anchorY));
            temp[i].name = i.ToString();
        }
        sprite = temp;
    }
}
