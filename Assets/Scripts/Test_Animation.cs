using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Test_Animation : MonoBehaviour
{
    public int currentFrame = 0;
    public int frameCount = 4;
    public float frameTimer = 0;
    public float frameTimerMax = 1f;
    private void FixedUpdate() {
        AnimationIndex(4,0.2f);
    }

    void AnimationIndex(int Count,float delay){
        frameTimer += Time.fixedDeltaTime;
        if(frameTimer >= delay){
            frameTimer = 0;
            currentFrame = (currentFrame + 1)% Count;
        }
    }
}
