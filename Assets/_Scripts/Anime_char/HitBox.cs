using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox : MonoBehaviour
{
    [SerializeField] CapsuleCollider2D mHitBox;
    public float dame;
    public float speedAtk;
    private void Reset() {
        LoadComponet();
    }
    void Awake(){
        LoadComponet();
    }
    void LoadComponet(){
        mHitBox = GetComponent<CapsuleCollider2D>();
    }
    public void RangeHitBox(Vector2 size){
        mHitBox.size = size;
    }
}
