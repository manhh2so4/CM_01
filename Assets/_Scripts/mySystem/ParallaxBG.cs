using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBG : MonoBehaviour
{
    public Camera cam;
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
    
    public int BG_type;
    imgBG_SO imgBG_SO;
    float height;
    float width;
    float Field2Y,houseY,mountainY;
    public Sprite[] imgBg;
    Transform[] Bg0 = new Transform[3];
    Transform[] Bg1 = new Transform[3];
    Transform[] Bg2 = new Transform[3];
    Transform[] Bg3 = new Transform[3];
    
    public Transform Sun;
    [SerializeField] UnityEngine.GameObject prefab;
    private void Reset() {
        cam = Camera.main;
        string prefabName = "ObjDraw";
        prefab = Resources.Load<UnityEngine.GameObject>(prefabName);
        LoadComponent();
        height = 2f * cam.orthographicSize;
        width = height * cam.aspect;        
        LoadImgBG();
    }
    private void Awake(){
        gameObject.SetActive(true);
        cam = Camera.main;
        height = 2f * cam.orthographicSize;
        width = height * cam.aspect;
        string prefabName = "ObjDraw";
        prefab = Resources.Load<UnityEngine.GameObject>(prefabName);
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
        Sun = transform.Find("Sun");
        Bg0[0] = transform.GetChild(0).GetChild(0);
        Bg0[1] = transform.GetChild(0).GetChild(1);
        Bg0[2] = transform.GetChild(0).GetChild(2);

        Bg1[0] = transform.GetChild(1).GetChild(0);
        Bg1[1] = transform.GetChild(1).GetChild(1);
        Bg1[2] = transform.GetChild(1).GetChild(2);

        Bg2[0] = transform.GetChild(2).GetChild(0);
        Bg2[1] = transform.GetChild(2).GetChild(1);
        Bg2[2] = transform.GetChild(2).GetChild(2);

        Bg3[0] = transform.GetChild(3).GetChild(0);
        Bg3[1] = transform.GetChild(3).GetChild(1);
        Bg3[2] = transform.GetChild(3).GetChild(2);
    }
    void LoadImgBG(){
        //if (BG_typeCurrent == BG_type ) return;
        string resPath = "TextLoad/imgBG/BG_type " + 0;
        imgBG_SO = Resources.Load<imgBG_SO>(resPath);
        this.imgBg = imgBG_SO.imgBGs;
        Field2Y = (float)imgBg[0].texture.height/100;
        houseY = Field2Y +  (float)imgBg[1].texture.height/100;
        mountainY = Field2Y +  (float)imgBg[1].texture.height/100 +0.4f;
        LoadBG(ref Bg0[0],24,0,0);
        LoadBG(ref Bg0[1],24,1,0);
        LoadBG(ref Bg0[2],24,2,0);

        LoadBG(ref Bg1[0],24,0,1);
        LoadBG(ref Bg1[1],24,1,1);
        LoadBG(ref Bg1[2],24,2,1);

        LoadBG(ref Bg2[0],192,0,2);
        LoadBG(ref Bg2[1],192,1,2);
        LoadBG(ref Bg2[2],192,2,2);

        LoadBG(ref Bg3[0],64,0,3);
        LoadBG(ref Bg3[1],64,1,3);
        LoadBG(ref Bg3[2],64,2,3);
    }
    void LoadBG(ref Transform obj,int size,int index,int idImg){
        int loop = (int)width*100/(size*4);
        lengthX[idImg] = (loop*size*4f)/100;

        float hSet;       
        hSet = (cam.transform.position.y - height/2);
        
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
        for (int i = 0; i < loop; i ++)
        {
            UnityEngine.GameObject temp;
            try
            {
                temp = obj.GetChild(i).gameObject;
            }
            catch (System.Exception)
            {                                
                temp = Instantiate(prefab, obj);               
            } 
            temp.GetComponent<SpriteRenderer>().sprite = imgBg[idImg];
            temp.GetComponent<SpriteRenderer>().sortingOrder = -2-idImg;
            temp.name = imgBg[idImg].name;
            temp.transform.position = new Vector3((cam.transform.position.x-(float)(loop*size)/50 )+((float)(i*size*4)/100),
            hSet ,0);
        }
        obj.transform.position = new Vector3((float)(loop*size*(1-index))/25 , 0,0);
    }
    void PaintSun(){
        Sun.transform.position = new Vector3(cam.transform.position.x + width/3.5f,cam.transform.position.y + height/4f);
    }
    void PaintClound(){
        Sun.transform.position = new Vector3(cam.transform.position.x + width/3.5f,cam.transform.position.y + height/4f);
    }
}
