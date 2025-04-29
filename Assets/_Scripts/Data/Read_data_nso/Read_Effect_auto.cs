using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using NaughtyAttributes;

public class Read_Effect_auto
{
    private static Read_Effect_auto _instance;
    private Read_Effect_auto()
    {
        LoadData();
    }
    static Read_Effect_auto Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new Read_Effect_auto();
            }
            return _instance;
        }
    }
    public TextAsset textJson;
    JsonData data; 
    [SerializeField] public Data_Eff_auto[] Effauto_datas;
    private void Reset() {
        LoadData();
    }
     private void Awake() {
        LoadData();
    }
    [Button]
    void LoadData(){
        textJson = Resources.Load<TextAsset>("effect_data_auto");
        data = JsonMapper.ToObject(textJson.ToString());
        Data_Eff_auto[] Effauto_Temp = new Data_Eff_auto[data.Count];
        for (int i = 0; i < data.Count ; i++)
        {   
            Data_Eff_auto tempId = new Data_Eff_auto();
            JsonData data2 = HSTool.GetItem(data[i]["sprites"].ToString());
            tempId.imageInfors = new ImageInfor[data2.Count];
            for (int j = 0; j < data2.Count; j++)
            {
                ImageInfor image = new ImageInfor();
                image.ID = int.Parse(data2[j]["id"].ToString());
                image.x0 = int.Parse(data2[j]["x"].ToString());
                image.y0 = int.Parse(data2[j]["y"].ToString());
                image.w = int.Parse(data2[j]["w"].ToString());
                image.h = int.Parse(data2[j]["h"].ToString());
                tempId.imageInfors[j] = image;
            }

            JsonData data3 = HSTool.GetItem(data[i]["frames"].ToString());
            tempId.frames = new Frames[data3.Count];
            for (int j = 0; j < data3.Count; j++)
            {
                ImageID[] _idImg = new ImageID[data3[j].Count];
                for (int k = 0; k < data3[j].Count; k++)
                {
                    _idImg[k] = new ImageID();
                    _idImg[k].ID = int.Parse(data3[j][k]["id"].ToString());
                    _idImg[k].x0 = int.Parse(data3[j][k]["dx"].ToString());
                    _idImg[k].y0 = int.Parse(data3[j][k]["dy"].ToString());
                }
                tempId.frames[j].imageIDs = _idImg;
            }

            JsonData data4 = HSTool.GetItem(data[i]["running"].ToString());
            tempId.Running = new int[data4.Count];
            for (int j = 0; j < data4.Count; j++)
            {
                tempId.Running[j] = int.Parse(data4[j].ToString());
            }

            Effauto_Temp[i] = tempId;
        }
        Effauto_datas = Effauto_Temp;
    }
    public static Data_Eff_auto[] GetData(){
        return Instance.Effauto_datas;
    }
}
