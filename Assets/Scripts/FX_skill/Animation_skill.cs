using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation_skill : LoadSprite
{
    int skilId = 0;
    int skilIdCurrent = -1;
    Sprite[] spriteFXs;
    
    [SerializeField] SpriteFX_SO mTexFX;
    void IputText(){
        Texture2D[] impTexFX = mTexFX.fxTex;
        if(skilId == skilIdCurrent) return;

        //Texture2D[] impTexFX = new Texture2D[Read_FX_Skill.skillInfors[1].];
                
    }

}
