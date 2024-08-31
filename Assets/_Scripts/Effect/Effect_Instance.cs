using System;
using Unity.VisualScripting;
using UnityEngine;
public class Effect_Instance : MonoBehaviour
{

    [SerializeField] Sprite[] sprites;
    [SerializeField] SpriteRenderer mSPR;
    int FrameCurrent = 0;
    public float life = 2;
    float startTime = 0;
    [SerializeField] int type;
    [SerializeField] float speedAnim;

    private void OnEnable() {
        FrameCurrent = 0;
        frameTimer = 99;
        startTime = Time.time;
        
    }
    private void FixedUpdate() {
        PlayEffect();
    }
    void PlayEffect(){
        switch (type)
        {
            case 0:
            if(FrameRate(speedAnim)) return;
            if(FrameCurrent >= sprites.Length-1){
                Destroy(gameObject);
            }
            mSPR.sprite = sprites[FrameCurrent];
            FrameCurrent++;
            break;

            case 1:
            
            if (Time.time < startTime + life){
                mSPR.sprite = sprites[0];
                return;
            } 

            if(FrameRate(speedAnim)) return;
            if(FrameCurrent >= (sprites.Length-1)){
                Destroy(gameObject);
                return;
            }

            mSPR.sprite = sprites[FrameCurrent];
            FrameCurrent++;
            break;

            case 2:
            if (Time.time >= startTime + life) Destroy(gameObject);

            if(FrameRate(speedAnim)) return;

            mSPR.sprite = sprites[FrameCurrent];
            FrameCurrent = ((FrameCurrent + 1)%sprites.Length);

            break;

        }
        
    }
    private void OnDisable() {
        
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
/*  
    0 - Once
    1 - Once_setTime
    2 - Loop_setTime
*/

