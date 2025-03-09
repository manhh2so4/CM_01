using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using TMPro;
using UnityEngine;

public class Box_Chat : MonoBehaviour, IObjectPoolItem
{
    ObjectPool objectPool;
    SpriteRenderer bgBox;
    TextMeshPro textMeshPro;
    Vector3 startPosition;

    float speed = 10f;
    float height = .05f;
    protected void Awake(){
        LoadComponents();
    }
    void LoadComponents(){
        bgBox = transform.Find("Background").GetComponent<SpriteRenderer>();
        textMeshPro = transform.Find("Text").GetComponent<TextMeshPro>();
    }
    public void Setup(string text){
        startPosition = transform.localPosition;
        textMeshPro.text = text;
        textMeshPro.ForceMeshUpdate();
        Vector2 textSize = textMeshPro.GetRenderedValues(false);
        bgBox.size = textSize + new Vector2(0.6f,0.3f);
        TextWriter.AddWriter_Static(textMeshPro, text, .03f, true, true, () => {
            Invoke("ReturnItemToPool", 1f);
        });
    }
    void Update()
    {
        float newY = startPosition.y + Mathf.Sin(Time.time * speed) * height;
        transform.localPosition = new Vector3(startPosition.x, newY, startPosition.z);
    }

    private void ReturnItemToPool()
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