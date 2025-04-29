using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;

public class Read_Nj_part
{
    public TextAsset textJson;
    private  JsonData data;
    private static Read_Nj_part _instance;
    private Read_Nj_part()
    {
        LoadData();
    }
    public static Read_Nj_part Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new Read_Nj_part();
            }
            return _instance;
        }
    }

    public List<Frames> nj_Parts_Head = new List<Frames>();
    public List<Frames> nj_Parts_Leg = new List<Frames>();
    public List<Frames> nj_Parts_Body = new List<Frames>();
    public List<Frames> nj_Parts_Wp = new List<Frames>();
    public Frames[] nj_Parts;
    
    void LoadData(){
        textJson = Resources.Load<TextAsset>("nj_part");
        data = JsonMapper.ToObject(textJson.ToString());
        Debug.Log(data.Count);
        nj_Parts = new Frames[data.Count];

        for (int i = 0; i < data.Count ; i++)
        {   
            nj_Parts[i] = Readpart(data[i]["part"].ToString());    
        }
    }

    Frames Readpart(string _data){

        JsonData data2 = GetItem(_data);
        Frames PartTemp = new Frames();

        PartTemp.imageIDs = new ImageID[data2.Count];

        for (int i = 0; i < data2.Count ; i++)
        {   
            ImageID imageIDtemp = new ImageID();
            imageIDtemp.ID =  int.Parse(data2[i]["id"].ToString());
            imageIDtemp.x0 = int.Parse(data2[i]["dx"].ToString());
            imageIDtemp.y0 = int.Parse(data2[i]["dy"].ToString());
            PartTemp.imageIDs[i] = imageIDtemp;        
        }
        return PartTemp;
    }

    JsonData GetItem(string data3){
        return JsonMapper.ToObject(data3);
    }

}
