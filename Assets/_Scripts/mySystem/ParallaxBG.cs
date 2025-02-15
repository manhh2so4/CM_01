using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class ParallaxBG : MonoBehaviour
{
    public Camera cam;
    [SortingLayer] [SerializeField] int layerId;
    [SerializeField] private float[] parallaxEffect_X = new float[4];
    [SerializeField] private float[] parallaxEffect_Y = new float[4];
    
    float[] startPosX0 = new float[3];
    float[] startPosX1 = new float[3];
    float[] startPosX2 = new float[3];   
    float[] startPosX3 = new float[3];

    float[] startPosY = new float[4];
    float[] lengthX = new float[4];
    private float startCamX, startCamY;
    float distX,distY;
    
    imgBG_SO imgBG_SO;
    float heightCam;
    float widthCam;
    float Field2Y,houseY,mountainY;
    public Sprite[] imgBg;
    Transform[] Bg0 = new Transform[3];
    Transform[] Bg1 = new Transform[3];
    Transform[] Bg2 = new Transform[3];
    Transform[] Bg3 = new Transform[3];
    
    public Transform Sun;
    private void Reset() {
        LoadComponent();     
        LoadImgBG();
    }
    private void Awake(){
        LoadComponent();           
        LoadImgBG();
        startCamX = cam.transform.position.x;
        startCamY = cam.transform.position.y;
        
        for (int i = 0; i < 3; i++)
        {
            startPosX0[i] = Bg0[i].position.x;
            startPosX1[i] = Bg1[i].position.x;
            startPosX2[i] = Bg2[i].position.x;
            startPosX3[i] = Bg3[i].position.x;
        }                
        startPosY[0] = Bg0[0].position.y;
        startPosY[1] = Bg1[0].position.y;
        startPosY[2] = Bg2[0].position.y;
        startPosY[3] = Bg3[0].position.y;
    }
    private void FixedUpdate() {
        ParallaxEff();
        PaintSun();
    }
    void ParallaxEff(){                
                           
            LoopMap(Bg0, startPosX0, 0);
            LoopMap(Bg1, startPosX1, 1);
            LoopMap(Bg2, startPosX2, 2);
            LoopMap(Bg3, startPosX3, 3);  
    }
    void LoopMap(Transform[] layer,float[] startPos,int indexLayer){
        distX = (cam.transform.position.x - startCamX)* parallaxEffect_X[indexLayer];
        distY = (cam.transform.position.y - startCamY)* parallaxEffect_Y[indexLayer];
        for (int i = 0; i < 3; i++)
            {
                layer[i].position = new Vector3(startPos[i] + distX,startPosY[indexLayer] + distY);   

                float tempX = (cam.transform.position.x * (1 - parallaxEffect_X[indexLayer]));
                if (tempX >= startPos[i] + lengthX[indexLayer]*1.5f) startPos[i] += lengthX[indexLayer]*3;
                else if (tempX < startPos[i] - lengthX[indexLayer]*1.5f) startPos[i] -= lengthX[indexLayer]*3; 
            }
    }
    void LoadComponent(){
        cam = Camera.main;
        heightCam = 2f * cam.orthographicSize;
        widthCam = heightCam * cam.aspect;  
        Sun = transform.Find("Sun");
        for (int i = 0; i < 3; i++)
        {
            Bg0[i] = transform.GetChild(0).GetChild(i); 
            Bg1[i] = transform.GetChild(1).GetChild(i); 
            Bg2[i] = transform.GetChild(2).GetChild(i); 
            Bg3[i] = transform.GetChild(3).GetChild(i); 
        }               
    }
    void LoadImgBG(){
        //if (BG_typeCurrent == BG_type ) return;
        string resPath = "TextLoad/imgBG/BG_type " + 0;
        imgBG_SO = Resources.Load<imgBG_SO>(resPath);
        this.imgBg = imgBG_SO.imgBGs;
        Field2Y = (float)imgBg[0].texture.height/100;
        houseY = Field2Y +  (float)imgBg[1].texture.height/100;
        mountainY = Field2Y +  (float)imgBg[1].texture.height/100 +0.4f;
        for (int i = 0; i < 3; i++)
        {
            LoadBG(ref Bg0[i],i,0);
            LoadBG(ref Bg1[i],i,1);
            LoadBG(ref Bg2[i],i,2);
            LoadBG(ref Bg3[i],i,3);
        }
    }
    void LoadBG(ref Transform obj,int index,int idImg){
        int size = imgBg[idImg].texture.width;
        int loop = (int)widthCam*100/(size);
        lengthX[idImg] = ((float)loop*size)/100;

        float hSet;       
        hSet = (cam.transform.position.y - heightCam/2);
        float xSet = cam.transform.position.x;
        switch (idImg)
            {
                case 0:
                hSet +=  (float)imgBg[idImg].texture.height/200;
                break;
                case 1:
                hSet +=  Field2Y + (float)imgBg[idImg].texture.height/200;
                break;
                case 2:
                hSet +=  houseY + (float)imgBg[idImg].texture.height/200;
                break;
                case 3:
                hSet +=  mountainY + (float)imgBg[idImg].texture.height/200;
                break;
            }
        obj.transform.position = new Vector3(0,0,0);
        SpriteRenderer spr = obj.GetComponent<SpriteRenderer>();
        spr.sprite = imgBg[idImg];
        spr.sortingLayerID = layerId;
        spr.sortingOrder = -idImg - 10;
        spr.drawMode = SpriteDrawMode.Tiled;
        spr.size = new Vector2(lengthX[idImg],spr.size.y);

        obj.transform.position = new Vector3( (lengthX[idImg]*(1-index)) + xSet, hSet, 0 );
    }
    void PaintSun(){
        Sun.transform.position = new Vector3(cam.transform.position.x + widthCam/3.5f,cam.transform.position.y + heightCam/4f);
    }
    void PaintClound(){
        Sun.transform.position = new Vector3(cam.transform.position.x + widthCam/3.5f,cam.transform.position.y + heightCam/4f);
    }
}
