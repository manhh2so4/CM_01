using System;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSprite : MonoBehaviour {
    [SerializeField] GameObject Skill_0;
    [SerializeField] GameObject Skill_1;
    [SerializeField] GameObject Skill_2;
    int eff0id,eff1id,eff2id;
    [SerializeField] EffSkill[] skillsView;
    [SerializeField] Dictionary<int,SpriteInfo[]> effSkills = new Dictionary<int,SpriteInfo[]>();
    Vector3 valueRage;
    public Action<Vector2> setRange;

    //--------- Set Collider-----------    
    public void LoadEff0(int eff0id){
        this.eff0id = eff0id;           
    }
    public void PaintEff0(int i0 , int x, int y){
        PaintSkill(i0,Skill_0, effSkills[this.eff0id], x,y);
    }
    public int GetLenghtEff0(){
        return effSkills[eff0id].Length;
    }

    //------------------------------
    public void LoadEff1(int eff1id){
        this.eff1id = eff1id;          
    }
    public void PaintEff1(int i1,int x, int y){
        PaintSkill(i1,Skill_1,effSkills[this.eff1id], x,y);
    }
    public int GetLenghtEff1(){
        return effSkills[eff1id].Length;
    }

    //------------------------------
    public void LoadEff2(int eff2id){
        this.eff2id = eff2id;           
    }
    public void PaintEff2(int i2, int x, int y){
        
        PaintSkill(i2,Skill_2, effSkills[this.eff2id], x,y);
    }
    public int GetLenghtEff2(){
        return effSkills[eff2id].Length;
    }

    //------------------------------
    private void Awake() {
        Skill_0 = transform.Find("WeaponSprite").GetChild(0).gameObject;
        Skill_1 = transform.Find("WeaponSprite").GetChild(1).gameObject;
        Skill_2 = transform.Find("WeaponSprite").GetChild(2).gameObject;
    }

    public void SetSkillOff(){
        Skill_0.SetActive(false);
        Skill_1.SetActive(false);
        Skill_2.SetActive(false);
    }
    void PaintSkill(int index, GameObject obj, SpriteInfo[] effSkillInfos, int x,int y){
        if(index >= effSkillInfos.Length){
            obj.SetActive(false);
        }
        
        else{     

            mPaint.Paint(obj,effSkillInfos[index].sprite, x + effSkillInfos[index].dx, -y - effSkillInfos[index].dy,3);  

            float h = effSkillInfos[index].sprite.rect.height/100;
            float w = effSkillInfos[index].sprite.rect.width/100;

            valueRage.Set( obj.transform.localPosition.x +  w/2, obj.transform.localPosition.y + h/2, obj.transform.localPosition.y);

            setRange?.Invoke(valueRage);  
                   
        } 
        if(index == 0) obj.SetActive(true);
    }
    public void LoadEffSkill(int[] id)
    {
        effSkills.Clear();
        for (int i = 0; i < id.Length; i++)
        {
            string resPath = "TextLoad/FX_skill/FX_Sprite " +  id[i];
            SpriteInfo[] effSkillInfos = Resources.Load<Sprite_FXSkill_SO>(resPath).effSkillInfo_SO;
            effSkills.Add(id[i], effSkillInfos);    
        }
        skillsView = new EffSkill[effSkills.Count];
        for(int i = 0; i < skillsView.Length; i++){
            EffSkill temp = new EffSkill();
            temp.idEffSkill = id[i];
            temp.effSkillInfos = effSkills[id[i]];
            skillsView[i] = temp;
        }
    }
}