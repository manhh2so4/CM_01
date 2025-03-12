using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using TMPro;
using UnityEngine;

public class Box_Chat : MonoBehaviour, IObjectPoolItem
{
    ObjectPool objectPool;
    SpriteRenderer bgBox;
    SpriteRenderer bottom;
    TextMeshPro textMeshPro;
    Vector3 startPosition;
    TextWriterSingle textWriter;
    float speed = 10f;
    float height = .05f;
    protected void Awake(){
        LoadComponents();
    }
    void LoadComponents(){
        bgBox = transform.Find("Background").GetComponent<SpriteRenderer>();
        bottom = transform.Find("Bottom").GetComponent<SpriteRenderer>();
        textMeshPro = transform.Find("Text").GetComponent<TextMeshPro>();
    }
    public TextWriterSingle Setup(string text, Action OnComplete, int _SortingLayerID){
        bgBox.sortingLayerID = _SortingLayerID;
        bottom.sortingLayerID = _SortingLayerID;
        textMeshPro.sortingLayerID = _SortingLayerID;
        
        startPosition = Vector3.zero;
        textMeshPro.text = text;
        textMeshPro.ForceMeshUpdate();
        Vector2 textSize = textMeshPro.GetRenderedValues(false);
        bgBox.size = textSize + new Vector2(0.6f,0.3f);

        textWriter = TextWriter.AddWriter_Static(textMeshPro, text, .03f, true, true, OnComplete );
        return textWriter;
    }

    void Update()
    {
        float newY = startPosition.y + Mathf.Sin(Time.time * speed) * height;
        transform.localPosition = new Vector3(startPosition.x, newY, startPosition.z);
    }

    public void RemoveBoxChat(){
        if(gameObject.activeSelf == false){

            return; 
        }
        ReturnItemToPool();
    }



    void ReturnItemToPool()
    {
        
        if (objectPool != null)
        {
            objectPool.ReturnObject(this);
        }
        // Otherwise, destroy
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetObjectPool(ObjectPool pool)
    {
        objectPool = pool;
    }

    public void Release()
    {
        objectPool = null;
    }
}