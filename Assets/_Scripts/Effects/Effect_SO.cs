using System;
using UnityEngine;
using NaughtyAttributes;
public class Effect_SO : BaseEffect
{
    [Header("Effect Settings")]
    [Expandable]
    [SerializeField] Sprite_FXSkill_SO sprites; 
    [SerializeField] float ratio = 1;
    
    //----------- Variable draw
    protected SpriteRenderer mSPR;
    protected override int Length => sprites.effSkillInfo.Length;

    protected override void Awake() {
        if(mSPR == null) mSPR = GetComponent<SpriteRenderer>();
    }
    public override void SetData(int layerID, int sortingLayerID = 0,float life = -1, int _size = 1){
        base.SetData(layerID,sortingLayerID,life,_size);
        Paint();
    }
    
    protected override void Paint(){ 
        Sprite sprite = sprites.effSkillInfo[FrameCurrent].sprite;
        float dx = 0.01f*ratio*sprites.effSkillInfo[FrameCurrent].dx;
        float dy = -0.01f*ratio*sprites.effSkillInfo[FrameCurrent].dy;

        mSPR.sprite = sprite;
        mSPR.transform.localPosition = new Vector2(dx,dy);
    }
}

