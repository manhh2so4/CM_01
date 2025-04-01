using System;
using UnityEngine;
public class Effect_Instance : MonoBehaviour,IObjectPoolItem
{
    [SpritePreview]
    [SerializeField] Sprite[] sprites;
    [SerializeField] TypeEff type;   
    [SerializeField] float speedAnim;
    public float life = 2;
    public PosEff posEff;
    [SerializeField] Pivot anchor;
    SpriteRenderer mSPR;
    int FrameCurrent = 0;  
    int size =1;
    float startTime = 0,offsetY = 0;
    Vector2 startPos;
    void Awake()
    {
        if(mSPR == null) mSPR = GetComponent<SpriteRenderer>();
    }
    public void SetData(int layerID, int sortingLayerID = 0,float life = 99, int _size = 1){
        
        transform.localScale = new Vector3(_size,_size,1);
        this.life = life;
        startTime = Time.time;

        startPos = this.transform.localPosition;
        size = _size;
        Paint();

        mSPR.sortingLayerID = layerID;
        if(sortingLayerID == 0) return;
        mSPR.sortingOrder = sortingLayerID + mSPR.sortingOrder;
        
    }
    void OnDisable()
    {
        FrameCurrent = 0;
        frameTimer = 99;
        life = 99;
        size = 1;
    }
    private void Update() {
        PlayEffect();
    }
    void PlayEffect(){
        switch (type)
        {
            case TypeEff.Once:
                if(FrameRate(speedAnim)) return;
                if(FrameCurrent >= sprites.Length){
                    remove();
                    return;
                }
                Paint();
                FrameCurrent++;
            break;
            //--------------------------------------------
            case TypeEff.Fist_Once:            
                if (Time.time < startTime + life){           
                    return;
                } 
                if(FrameRate(speedAnim)) return;
                if(FrameCurrent >= (sprites.Length-1)){
                    remove();
                    return;
                }
                FrameCurrent++;
                Paint();
            break;
            //----------------------------------------------
            case TypeEff.Loop:
                if (Time.time >= startTime + life) remove();
                
                if(FrameRate(speedAnim)) return;

                Paint();
                FrameCurrent = ((FrameCurrent + 1)%sprites.Length);
            break;

        }
        
    }
    void remove(){
        ReturnItemToPool();
    }
    private void Paint(){ 
        mSPR.sprite = sprites[FrameCurrent];  
        SetPivot(FrameCurrent);        
    }
    private void SetPivot(int index){
        float h = sprites[index].rect.height/100;
        //float w = sprites[index].rect.width/100;
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

        this.transform.localPosition = startPos + new Vector2( 0 , offsetY*size ); 
    }
    bool FrameRate(float speed){        
        frameTimer += Time.deltaTime;
        if(frameTimer >= speed){
            frameTimer = 0;
            return false;
            
        }
        return true;
    }
    [SerializeField] float frameTimer = 0;
#region CreatPool
    ObjectPool objectPool;
    void ReturnItemToPool()
    {
        if (objectPool != null)
        {
            objectPool.ReturnObject(this);
            this.transform.SetParent(PoolsContainer.Instance.transform);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    public void SetObjectPool(ObjectPool pool)
    {
        objectPool = pool;
    }

    public void Release()
    {
        objectPool = null;
    }
#endregion

}
public enum Pivot
{
    None,
    Top,
    Center,
    Bot
}
public enum TypeEff
{
    Once,
    Fist_Once,
    Loop
}
public enum PosEff
{
    Head,
    Body,
    Foot
}

