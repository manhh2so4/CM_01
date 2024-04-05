using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using Unity.VisualScripting;
public class Read_FX_Skill : MonoBehaviour
{
    //public string textJson;
    public TextAsset textJson;
    private JsonData data; 
    public SkillInfor[] skillInfors;
    private void Start() { 
    }
    private void Reset() {
        LoadData();
    }
    void LoadData(){
        textJson = Resources.Load<TextAsset>("nj_effect");
        data = JsonMapper.ToObject(textJson.ToString());
        Debug.Log(data.Count);
        SkillInfor[] temp = new SkillInfor[data.Count];
        for (int i = 0; i < data.Count ; i++)
        {   
            SkillInfor skilId = new SkillInfor();  
            JsonData data2 = GetItem(data[i][1].ToString());
            skilId.info = new FormImg[data2.Count];
            for (int j = 0; j < skilId.info.Length; j++)
            {   
                FormImg formImg = new FormImg();
                formImg.imgId = int.Parse(data2[j][0].ToString());
                formImg.dx = int.Parse(data2[j][1].ToString());
                formImg.dy = int.Parse(data2[j][2].ToString());
                skilId.info[j] = formImg;        
            }            
            temp[i] = skilId;                            
        }
        skillInfors = temp;
    }
    JsonData GetItem(string data3){
        return JsonMapper.ToObject(data3);
    }
}
