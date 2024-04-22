using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteAnimator : MonoBehaviour
{
    private Sprite[] frameArray;
    int currentFrame;
    float timer;
    private void Update() {
        if(timer >= 1f){
            timer -= 1f;
            currentFrame++;
            gameObject.GetComponent<SpriteRenderer>().sprite = frameArray[currentFrame];
        }
    }

}
