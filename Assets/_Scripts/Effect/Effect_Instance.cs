using System;
using Unity.VisualScripting;
using UnityEngine;
public class Effect_Instance : MonoBehaviour
{

    [SerializeField] Sprite[] sprites;
    [SerializeField] SpriteRenderer mSPR;
    int FrameCurrent = 0;
    [SerializeField] float speedAnim;
    private void Awake() {

    }
    private void OnEnable() {
        FrameCurrent = 0;
        frameTimer = 99;
    }

    private void FixedUpdate() {
        PlayEffect();
    }
    void PlayEffect(){
        if(FrameRate(speedAnim)) return;
        if(FrameCurrent >= sprites.Length){
            FrameCurrent = 0;
            Destroy(gameObject);
        }
        mSPR.sprite = sprites[FrameCurrent];
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
