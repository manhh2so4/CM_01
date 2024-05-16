using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Testtwen : MonoBehaviour
{
    public Vector3 from;
    public float mtime;
    public Ease ease;
    public Ease easeEnd;
    void Update()
    {
        if(Input.GetButtonDown("Jump")){            
            transform.DOMove(from,mtime).SetRelative().SetEase(ease)
            .OnComplete(()=>{
                transform.DOMove(new Vector3(0,0,0),mtime).SetEase(ease);
            })
            ;
        }
    }
}
