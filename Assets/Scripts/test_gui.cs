using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;

public class test_gui : MonoBehaviour
{
    
    public Texture2D head; // Texture bạn muốn vẽ   
    public float xHead = -11f;
    public float yHead = 32f;

    public Texture2D leg; // Texture bạn muốn vẽ   
    public float xleg = 7f;
    public float yleg = -7f;
    public Texture2D body; // Texture bạn muốn vẽ   
    public float xbody = -10f;
    public float ybody = 15f;
    public float cx = 0f;
    public float cy = 0f;

    public int zoomlv = 1;



    void OnGUI()
    {           
            __drawRegion(leg,xleg,yleg);
            __drawRegion(body,xbody,ybody);
            __drawRegion(head,xHead,yHead);          
    }
    void __drawRegion(Texture2D image, float x, float y){
            int w = image.width;
            int h = image.height;
            float x0 = cx+x*zoomlv;
            float y0 = cy-y*zoomlv;
        GUI.DrawTexture(new Rect(x0, y0, w, h), image);
    }
}
