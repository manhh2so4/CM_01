using System;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSprite : MonoBehaviour {
    [SerializeField] GameObject Skill_0;
    [SerializeField] GameObject Skill_1;
    [SerializeField] GameObject Skill_2;

    [SerializeField] SpriteInfo[] effSkill_0 ;
    [SerializeField] SpriteInfo[] effSkill_1 ;
    [SerializeField] SpriteInfo[] effSkill_2 ;

     [SerializeField] Dictionary<int,SpriteInfo[]> effSkills = new Dictionary<int,SpriteInfo[]>();
    Vector3 valueRage;
    int currentId;
    public Action<Vector3> setRange;

    //--------- Set Collider-----------    
    public void LoadEff0(int eff0id){
        effSkill_0 = effSkills[eff0id];            
    }
    public void PaintEff0(int i0 , int x, int y){
        PaintSkill(i0,Skill_0,effSkill_0, x,y);
    }
    public int GetLenghtEff0(){
        return effSkill_0.Length;
    }

    //------------------------------
    public void LoadEff1(int eff1id){
        effSkill_1 = effSkills[eff1id];           
    }
    public void PaintEff1(int i1,int x, int y){
        PaintSkill(i1,Skill_1,effSkill_1, x,y);
    }
    public int GetLenghtEff1(){
        return effSkill_1.Length;
    }

    //------------------------------
    public void LoadEff2(int eff2id){
        effSkill_2 = effSkills[eff2id];           
    }
    public void PaintEff2(int i2, int x, int y){
        
        PaintSkill(i2,Skill_2,effSkill_2, x,y);
    }
    public int GetLenghtEff2(){
        return effSkill_2.Length;
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
            effSkillInfos = null;
        }
        else{                        
            mPaint.Paint(obj,effSkillInfos[index].sprite, x + effSkillInfos[index].dx, -y - effSkillInfos[index].dy,3);  
            

            float h = effSkillInfos[index].sprite.rect.height/100;
            float w = effSkillInfos[index].sprite.rect.width/100;
            valueRage.Set( obj.transform.localPosition.x +  w/2, h, obj.transform.localPosition.y);
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
    }
    void LoadEffSkill(int idSkill,EffSkill effSkill)
    {
        if(idSkill == currentId) return;
        string resPath = "TextLoad/FX_skill/FX_Sprite " +  idSkill;
        effSkill.effSkillInfos = Resources.Load<Sprite_FXSkill_SO>(resPath).effSkillInfo_SO; 
        currentId = idSkill;
    }
}