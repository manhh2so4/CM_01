using System;
using UnityEngine;
public class Effect_Sprite : BaseEffect
{
    [Header("Effect Settings")]
    [SpritePreview]
    [SerializeField] Sprite[] sprites;
    public PosEff posEff;
    [SerializeField] Pivot anchor;
    [SerializeField] bool isFixPivot = true;

    //----------- Variable draw
    protected SpriteRenderer mSPR;
    float offsetY = 0;
    protected int size => (int)transform.localScale.x;
    
    protected Vector2 startPos;
    protected override int Length => sprites.Length;

    protected override void Awake() {
        if(mSPR == null) mSPR = GetComponent<SpriteRenderer>();
    }

    public override void SetData(int layerID, int sortingLayerID = 0,float life = -1, int _size = 1){
        base.SetData(layerID,sortingLayerID,life,_size);

        startPos = this.transform.localPosition;
        Paint();
        SetPivot(0);

        mSPR.sortingLayerID = layerID;
        if(sortingLayerID == 0) return;
        mSPR.sortingOrder = sortingLayerID + mSPR.sortingOrder;
    }

    protected override void Paint(){ 
        mSPR.sprite = sprites[FrameCurrent];
        if(isFixPivot) SetPivot(FrameCurrent);        
    }
    private void SetPivot(int index){
        float h = sprites[index].rect.height/100;
        switch (anchor)
        {  
            case Pivot.None:
            return;

            case Pivot.Top:
                offsetY = -h/2;
            break;

            case Pivot.Bot:
                offsetY = h/2;
            break;

            case Pivot.Center:

            return;
        }
        transform.localPosition = startPos + new Vector2( 0 , offsetY*size ); 
    }

}

