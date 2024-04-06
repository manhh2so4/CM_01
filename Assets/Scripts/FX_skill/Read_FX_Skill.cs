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
    int[][] inforSkill;
    public List<int> uniqueList = new List<int>();
    private void Start() { 
    }
    private void Reset() {
        LoadData();
        //getNondouble(ref skillInfors);
        string jsonkeke = JsonUtility.ToJson(this.skillInfors);
        Debug.Log(jsonkeke);
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
    void getNondouble(ref SkillInfor[] arr){
        
        for (int i = 0; i < arr.Length; i++)
        {
            for (int j = 0; j < arr[i].info.Length ; j++)
            {
                if (!uniqueList.Contains(arr[i].info[j].imgId))
                {
                uniqueList.Add(arr[i].info[j].imgId);
                }
            }
        }
        uniqueList.Sort();
    }
    void BubbleSort(ref SkillInfor[] arr)
    {
        
        int n = arr.Length;
        for (int i = 0; i < n - 1; i++)
        {
            for (int j = 0; j < arr[i].info.Length ; j++)
            {
                for (int z = 0; z < uniqueList.Count; z++)
                {
                    if (arr[i].info[j].imgId == uniqueList[z])
                    {
                        //arr[i].info[j].imgId
                    }
                }
            }
        }
    }
}
