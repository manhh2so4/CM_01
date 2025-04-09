using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;

public class Read_BigImage : MonoBehaviour
{
    public TextAsset textJson;
    private JsonData data; 
    [SerializeField] public nj_Image[] nj_Images;
    private void Reset() {
        LoadData();
    }
     private void Awake() {
        LoadData();
    }
    void LoadData(){
        textJson = Resources.Load<TextAsset>("nj_image");
        data = JsonMapper.ToObject(textJson.ToString());
        nj_Images = new nj_Image[data.Count];
        for (int i = 0; i < data.Count ; i++)
        {   
            nj_Image imageId = new nj_Image();
            imageId.ID =  int.Parse(data[i][0].ToString()); 
            JsonData data2 = GetItem(data[i][1].ToString());
                ImageInfor imgInfor = new ImageInfor();
                imgInfor.ID = int.Parse(data2[0].ToString());
                imgInfor.x0 = int.Parse(data2[1].ToString());
                imgInfor.y0 = int.Parse(data2[2].ToString());
                imgInfor.w = int.Parse(data2[3].ToString());
                imgInfor.h = int.Parse(data2[4].ToString());                              
            imageId.inforImg = imgInfor;          
            nj_Images[i] = imageId;                            
        }
    }
    JsonData GetItem(string data3){
        return JsonMapper.ToObject(data3);
    }
}
