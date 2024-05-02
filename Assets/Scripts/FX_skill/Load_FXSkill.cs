using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Load_FXSkill : MonoBehaviour
{
    public Texture2D[] tex2D;
    [SerializeField] TextFXSkill_SO mFXskill;
    public int idSkill = 0;
    int idSkillCurren = -1;
    public void LoadTexAo_SO()
    {
        //if (idSkillCurren == idSkill ) return;
        string resPath = "TextLoad/FX_skill/FX_text " +  idSkill;
        this.mFXskill = Resources.Load<TextFXSkill_SO>(resPath);
        Debug.Log(": Char_texture " + resPath);
       // this.tex2D = mFXskill.fxTex;
        idSkillCurren = idSkill;
    }
    private void Reset() {
        LoadTexAo_SO();
    }
}


