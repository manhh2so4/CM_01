using UnityEngine;
public abstract class BaseEffect : MonoBehaviour, IObjectPoolItem
{
    [SerializeField] float speedAnim;
    [SerializeField] TypeEff type;
    [SerializeField] float life = 2;

    //----------- Variable draw
    
    protected abstract int Length { get; }
    float startTime = 0;
    protected int FrameCurrent = 0; 
    
    protected virtual void Awake()
    {
        
    }
    public virtual void SetData(int layerID, int sortingLayerID = 0,float life = -1, int _size = -1){

        FrameCurrent = 0;
        frameTimer = 99;

        if(_size != -1) transform.localScale = new Vector3(_size,_size,1);
        if(life != -1) this.life = life;
        startTime = Time.time;
    }
    void OnDisable()
    {
        FrameCurrent = 0;
        frameTimer = 99;
    }

    public void Trigger(){
        type = TypeEff.Once;    
    }
    
    protected virtual void Update() {
        PlayEffect();
    }
    protected virtual void PlayEffect(){
        
        switch (type)
        {
            case TypeEff.Once:
                if(FrameRate(speedAnim)) return;
                if(FrameCurrent >= Length){
                    remove();
                    return;
                }
                Paint();
                FrameCurrent++;
            break;
            //--------------------------------------------
            case TypeEff.Fist_Once:
            
                if(life < 0) return;
                if (Time.time < startTime + life){           
                    return;
                }

                if(FrameRate(speedAnim)) return;
                if(FrameCurrent >= (Length-1)){
                    remove();
                    return;
                }
                FrameCurrent++;
                Paint();
            break;
            //----------------------------------------------
            case TypeEff.Loop:

                if(life > 0) if(Time.time >= startTime + life) remove();
                
                if(FrameRate(speedAnim)) return;

                Paint();
                FrameCurrent = ((FrameCurrent + 1)%Length);
            break;

        }
    }
    protected abstract void Paint();
    protected virtual void remove(){
        ReturnToPool();
    }
    bool FrameRate(float speed){        
        frameTimer += Time.deltaTime;
        if(frameTimer >= speed){
            frameTimer = 0;
            return false;
            
        }
        return true;
    }
    float frameTimer = 0;

#region CreatPool
    ObjectPool objectPool;
    
    public void SetObjectPool(ObjectPool pool)
    {
        objectPool = pool;
    }

    public void Release()
    {
        objectPool = null;
    }

    public void ReturnToPool()
    {
        if (objectPool != null)
        {
            objectPool.ReturnObject(this);
            this.transform.SetParent(PoolsContainer.Instance.transform);
        }else
        {
            Destroy(gameObject);
        }
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
