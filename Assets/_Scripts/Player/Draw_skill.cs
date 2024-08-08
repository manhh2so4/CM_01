using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Comparers;

public class Draw_skill : MonoBehaviour
{    
    [SerializeField] GameObject Skill_0;
    [SerializeField] GameObject Skill_1;
    [SerializeField] GameObject Skill_2;

    [SerializeField]  EffSkill effSkill_0;
    [SerializeField]  EffSkill effSkill_1;
    [SerializeField]  EffSkill effSkill_2;
    [SerializeField]  CapsuleCollider2D mHitBox;
    [SerializeField] int SenderDame;
    //--------- Set Collider-----------
    Vector2 rangeSet,mSize,mOffset;
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
        Skill_0 = transform.GetChild(0).gameObject;
        Skill_1 = transform.GetChild(1).gameObject;
        Skill_2 = transform.GetChild(2).gameObject;

        LoadEff0(161);
        PaintEff0(0,0,0);
    }
    private void Awake() {
        OffHitBox();
    }
    public void RangeHitBox(Vector2 vec,float y){
        if (vec.x > mHitBox.size.x){
            if(vec.x > 2) vec.Set(vec.x-vec.x/10,0);
            mSize.Set(vec.x,mHitBox.size.y);
            mOffset.Set(vec.x/2,y);
            ChangeHitBox();
        }
        if (vec.y > mHitBox.size.y) {
            if(vec.y > 1) vec.Set(0,vec.y-vec.y/10);
            mSize.Set(mHitBox.size.x,vec.y);
            mOffset.Set(mHitBox.size.x/2,y);
            ChangeHitBox();           
        }              
    }
    void ChangeHitBox(){
        mHitBox.size = mSize;
        mHitBox.offset = mOffset;
        if(mHitBox.size.x > mHitBox.size.y) mHitBox.direction = CapsuleDirection2D.Horizontal;
        else mHitBox.direction = CapsuleDirection2D.Vertical;
    }
    public void OnHitBox(int _SenderDame){
        SenderDame = _SenderDame;
        mHitBox.enabled = true;
    }
    private void OnTriggerEnter2D(Collider2D other){
        
        
            IDamageable damageable = other.GetComponent<IDamageable>();
            if(damageable != null){
                damageable.Damage(SenderDame);
            
        } 
         
    }
    public void OffHitBox(){
        mHitBox.size = new(0,0);
        mHitBox.offset =new Vector2(0,0);
        mHitBox.enabled = false;
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
            rangeSet.Set(obj.transform.localPosition.x + w/200, h/100);           
            RangeHitBox(rangeSet,obj.transform.localPosition.y);            
        } 
        if(index == 0) obj.SetActive(true);
    }

    void LoadEffSkill(int idSkill,EffSkill effSkill)
    {
        string resPath = "TextLoad/FX_skill/FX_Sprite " +  idSkill;
        effSkill.effSkillInfos = Resources.Load<Sprite_FXSkill_SO>(resPath).effSkillInfo_SO;

    }
}
