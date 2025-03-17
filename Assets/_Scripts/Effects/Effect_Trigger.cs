using System;
using UnityEngine;
public class Effect_Trigger : MonoBehaviour
{

    [SerializeField] Sprite[] sprites;
    [SerializeField]  SpriteRenderer mSPR;
    [SerializeField] SmallEffect_SO smallEffect_SO;
    int FrameCurrent = 0;
    [SerializeField] float speedAnim;
    float x = 0;
    Vector3 posEff;
    void LoadEffect(string name){
        string resPath = "Effec_Load/Small_Eff/" + name;
        this.smallEffect_SO = Resources.Load<SmallEffect_SO>(resPath);
        mPaint.LoadSprite(ref sprites,smallEffect_SO.textures, mPaint.VCENTER|mPaint.HCENTER);        
    }
    private void Awake() {
        LoadComponent();
    }
    private void Reset() {
        LoadComponent();
    }
    void LoadComponent(){
        if(mSPR == null) mSPR = GetComponent<SpriteRenderer>();
        LoadEffect(gameObject.name);
    }
    private void FixedUpdate() {
        PlayEffect();
    }
    private void OnEnable() {
        FrameCurrent = 0;
        frameTimer = 99;
    }
    void PlayEffect(){
        if(FrameRate(speedAnim)) return;
        if(FrameCurrent >= sprites.Length){
            FrameCurrent = 0;
            transform.localPosition = new Vector3(0f,0f,0f);
            x = 0;
            Debug.Log("Done_eff");
            gameObject.SetActive(false);
        }
        mSPR.sprite = sprites[FrameCurrent];
        x -= 0.01f;
        posEff.Set(-0.08f + x,0f,0f);
        transform.localPosition = posEff;
        FrameCurrent++;
    }
    bool FrameRate(float speed){        
        frameTimer += Time.fixedDeltaTime;
        if(frameTimer >= speed){
            frameTimer = 0;
            return false;
        }
        return true;
    }
    float frameTimer = 0;
}
