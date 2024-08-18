using UnityEngine;

public class WeaponSprite : MonoBehaviour {
    [SerializeField] GameObject Skill_0;
    [SerializeField] GameObject Skill_1;
    [SerializeField] GameObject Skill_2;

    [SerializeField]  EffSkill effSkill_0;
    [SerializeField]  EffSkill effSkill_1;
    [SerializeField]  EffSkill effSkill_2;

    //--------- Set Collider-----------
    
    public void LoadEff0(int eff0id){
        effSkill_0.idEffSkill = eff0id;
        LoadEffSkill(eff0id,effSkill_0);              
    }
    public void PaintEff0(int i0 , int x, int y){
        PaintSkill(i0,Skill_0,effSkill_0,x,y);
    }
    public int GetLenghtEff0(){
        return effSkill_0.effSkillInfos.Length;
    }

    //------------------------------
    public void LoadEff1(int eff1id){
        effSkill_1.idEffSkill = eff1id;
        LoadEffSkill(eff1id,effSkill_1);              
    }
    public void PaintEff1(int i1,int x, int y){
        PaintSkill(i1,Skill_1,effSkill_1,x,y);
    }
    public int GetLenghtEff1(){
        return effSkill_1.effSkillInfos.Length;
    }

    //------------------------------
    public void LoadEff2(int eff2id){
        effSkill_2.idEffSkill = eff2id;
        LoadEffSkill(eff2id,effSkill_2);              
    }
    public void PaintEff2(int i2, int x, int y){
        PaintSkill(i2,Skill_2,effSkill_2,x,y);
    }
    public int GetLenghtEff2(){
        return effSkill_2.effSkillInfos.Length;
    }

    //------------------------------
    
    private void Reset() {
        
    }
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
    void PaintSkill(int index, GameObject obj,EffSkill EffSkills, int x,int y){
        if(index >= EffSkills.effSkillInfos.Length){
            obj.SetActive(false);
            EffSkills = null;
        }
        else{                        
            mPaint.Paint(obj,EffSkills.effSkillInfos[index].sprite, x + EffSkills.effSkillInfos[index].dx,- y -EffSkills.effSkillInfos[index].dy,3);  

            float h = EffSkills.effSkillInfos[index].sprite.rect.height;
            float w = EffSkills.effSkillInfos[index].sprite.rect.width;            
        } 
        if(index == 0) obj.SetActive(true);
    }

    void LoadEffSkill(int idSkill,EffSkill effSkill)
    {
        string resPath = "TextLoad/FX_skill/FX_Sprite " +  idSkill;
        effSkill.effSkillInfos = Resources.Load<Sprite_FXSkill_SO>(resPath).effSkillInfo_SO;

    }
}