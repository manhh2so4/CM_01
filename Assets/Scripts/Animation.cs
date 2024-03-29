using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation : MonoBehaviour
{
    public bool isRepeated;
    public int[] frameImages;
    public int currentFrame;
    public float delayFrame;
    public float beginTime;

    public void setCurrentFrame(int currentFrame){
        if(currentFrame >= 0 && currentFrame < frameImages.Length)
            this.currentFrame = currentFrame;
        else this.currentFrame = 0;
    }
    public void reset(){
        currentFrame = 0+45+5+6+6;
        beginTime = 0;
    }
    public int getCurrentFrame(){
        return this.currentFrame;
    }   
    public void Update22(long deltaTime){
        
        if(beginTime == 0) beginTime = deltaTime;
        else{            
            if(deltaTime - beginTime > delayFrame){
                nextFrame();
                beginTime = deltaTime;
            }
        }
        
    }
    private void nextFrame(){       
        if(currentFrame >= frameImages.Length - 1){            
            currentFrame = 0;
        }
        else currentFrame++;       
    }
}
