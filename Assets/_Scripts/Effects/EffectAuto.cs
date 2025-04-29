using NaughtyAttributes;
using UnityEngine;

public class EffectAuto : BaseEffect {

    [Header("Effect Settings---------------\n")]
    [OnValueChanged("OnValueChangedCallback")] 
    [SerializeField]Texture2D TEXTURE2D;
    [SerializeField]bool Reverse;
    [HideInInspector][SerializeField] Sprite[] sprites ;

    //-------------- variable Data --------------
    [HideInInspector][SerializeField] Frames[] Frames;
    [HideInInspector][SerializeField] int[] Running;
    [SerializeField] SpritePool spritePool;
    //----------- Variable draw
    [SortingLayer][SerializeField] int layerID;
    [SerializeField] int sortingLayer;
    

    protected override int Length => Running.Length;
    public override void SetData(int _layerID, int _sortingLayer = 0,float life = -1, int _size = 1){
        base.SetData(_layerID,_sortingLayer,life,_size);
        this.layerID = _layerID;
        this.sortingLayer = _sortingLayer;
        Paint();
    }

    protected override void Paint()
    {
        int frame ;
        if(Reverse) frame = Length-1 - FrameCurrent;
        else frame = Running[FrameCurrent];
        DrawSprite( frame );
    }

    void DrawSprite(int Frame){
        transform.ReturnPoolChilds();
        if(Frame >= Frames.Length) { Common.Log("Frame Max: " + (Frames.Length-1) ); return;}
        for (int i = 0; i < Frames[Frame].imageIDs.Length; i++){

            ImageID image = Frames[Frame].imageIDs[i];
            SpritePool subSPR = Instantiate(spritePool, this.transform);

            subSPR.Sprite = sprites[image.ID];
            subSPR.SortingLayerID = layerID;
            subSPR.SortingOrder = sortingLayer + i;
            subSPR.Position = new Vector2( image.x0*4f/100 , -image.y0*4f/100 );

        }
    }

#if UNITY_EDITOR
    [OnValueChanged("Draw")] 
    [SerializeField] int frameStart;
    void Draw(){
        transform.RemoveImmediateChilds();
        DrawSprite(frameStart);
    }
    void OnValueChangedCallback(){
        int ID = int.Parse(TEXTURE2D.name); 
        mPaint.LoadSpriteRegion(ref sprites, Read_Effect_auto.GetData()[ID].imageInfors, TEXTURE2D, new Vector2(0f,1f));
        Frames = Read_Effect_auto.GetData()[ID].frames;
        Running = Read_Effect_auto.GetData()[ID].Running;
        frameStart = 0;
        Draw();
    }
#endif
}

