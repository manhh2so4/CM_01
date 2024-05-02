using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draw_skill : MonoBehaviour
{    
    [SerializeField] GameObject Skill_0;
    [SerializeField] GameObject Skill_1;
    [SerializeField] GameObject Skill_2;

    [SerializeField]  EffSkill effSkill_1;
    [SerializeField]  EffSkill effSkill_2;
    [SerializeField]  EffSkill effSkill_3;
    List<EffSkill> eff0s;
    public void LoadEff0(int eff0id){
        effSkill_1.idEffSkill = eff0id;
        LoadTexAo_SO(eff0id,ref effSkill_1.effSkillInfos);              
    }
    public int GetLenghtEff0(){
        return effSkill_1.effSkillInfos.Length;
    }
    private void Reset() {
        LoadEff0(0);
    }
    public void PaintSkill(int index){
        if(index >= effSkill_1.effSkillInfos.Length){
            Skill_0.SetActive(false);
        }
        else{
            anim_sprite.Paint(ref Skill_0,effSkill_1.effSkillInfos[index].texture2D,effSkill_1.effSkillInfos[index].dx,-effSkill_1.effSkillInfos[index].dy,3);
        } 
        if(index == 0) Skill_0.SetActive(true);
    }

    public void LoadTexAo_SO(int idSkill,ref EffSkillInfo[] effSkillInfo)
    {
        int lenth = Read_FX_Skill.skillInfors[idSkill].info.Length;
        effSkillInfo = new EffSkillInfo[lenth];
        string resPath = "TextLoad/FX_skill/FX_text " +  idSkill;
        TextFXSkill_SO skillSO = Resources.Load<TextFXSkill_SO>(resPath);
        for (int i = 0; i < lenth; i++)
        {
            //effSkillInfo[i].texture2D = skillSO.fxTex[i];
            effSkillInfo[i].dx = Read_FX_Skill.skillInfors[idSkill].info[i].dx;
            effSkillInfo[i].dy = Read_FX_Skill.skillInfors[idSkill].info[i].dy;
        }        
        Debug.Log(" Char_texture idSkill : " + idSkill);
    }
}
