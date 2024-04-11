using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_SpriteFX : LoadSprite
{
    public Texture2D[] tex2D;
    public Sprite[] spriteFxs;
    [SerializeField] protected SpriteFX_SO mFXskill;
    public int idSkill = 0;
    int idSkillCurren = -1;
    public void LoadTexAo_SO()
    {
        if (idSkillCurren == idSkill ) return;
        string resPath = "TextLoad/FX_skill/FX_text " + idSkill;
        this.mFXskill = Resources.Load<SpriteFX_SO>(resPath);
        Debug.Log(": Char_texture " + resPath);
        this.tex2D = mFXskill.fxTex;
        CvtSprite(ref spriteFxs,ref tex2D,3);
        idSkillCurren = idSkill;
    }
    private void Reset() {
        LoadTexAo_SO();
    }
}


