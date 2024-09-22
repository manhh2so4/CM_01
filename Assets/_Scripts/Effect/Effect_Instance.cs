using System;
using Unity.VisualScripting;
using UnityEngine;
public class Effect_Instance : MonoBehaviour
{
    [SerializeField] Sprite[] sprites;
    [SerializeField] TypeEff type;   
    [SerializeField] float speedAnim;
    public float life = 2;
    public PosEff posEff;
    [SerializeField] Pivot anchor;
    SpriteRenderer mSPR;
    int FrameCurrent = 0;   
    float startTime = 0,offsetY = 0,offsetX = 0;
    Vector3 newPosition = Vector3.zero;
    public void SetData(float life,int layerID){
        if(mSPR == null) mSPR = GetComponent<SpriteRenderer>();
        this.life = life;
        mSPR.sortingLayerID = layerID;
    }

    private void OnEnable() {
        if(mSPR == null) mSPR = GetComponent<SpriteRenderer>();
        FrameCurrent = 0;
        frameTimer = 99;
        startTime = Time.time;
        Paint();
        SetPivot();              
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
                Destroy(gameObject);
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
                Destroy(gameObject);
                return;
            }
            FrameCurrent++;
            Paint();
            break;
            //----------------------------------------------
            case TypeEff.Loop:
            if (Time.time >= startTime + life) Destroy(gameObject);
            
            if(FrameRate(speedAnim)) return;

            Paint();
            FrameCurrent = ((FrameCurrent + 1)%sprites.Length);
            break;

        }
        
    }
    private void OnDisable() {
        
    }
    private void Paint(){ 
        if(sprites == null) return;
        mSPR.sprite = sprites[FrameCurrent];          
    }
    private void SetPivot(){
        float h = sprites[0].rect.height/100;
        float w = sprites[0].rect.width/100;
        switch (anchor)
        {               
            case Pivot.Top:
                offsetY -= h/2;
            break;
            case Pivot.Bot:
                offsetY += h/2 - 0.02f;
            break;
            case Pivot.Center:
            break;
        }
        newPosition.Set(0,offsetY,0);
        transform.position += newPosition;   
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
}
public enum Pivot
{
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

