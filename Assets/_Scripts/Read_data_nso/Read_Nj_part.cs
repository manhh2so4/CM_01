using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;

public class Read_Nj_part
{
    public TextAsset textJson;
    private  JsonData data;
    private static Read_Nj_part _instance;
    private static readonly object _lock = new object();
    private Read_Nj_part()
    {
        LoadData();
        Debug.Log("creat! Nj_part");
        Debug.Log("nj_Parts_Head! " + nj_Parts_Head.Count);
        Debug.Log("nj_Parts_Leg! " + nj_Parts_Leg.Count);
        Debug.Log("nj_Parts_Body! " + nj_Parts_Body.Count);
        Debug.Log("nj_Parts_Wp! " + nj_Parts_Wp.Count);
    }
    public static Read_Nj_part Instance
    {
        get
        {
            if (_instance == null)
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new Read_Nj_part();
                        
                    }
                }

            }
            return _instance;
        }
    }

    [SerializeField] public List<nj_Part> nj_Parts_Head = new List<nj_Part>();
    [SerializeField] public List<nj_Part> nj_Parts_Leg = new List<nj_Part>();
    [SerializeField] public List<nj_Part> nj_Parts_Body = new List<nj_Part>();
    [SerializeField] public List<nj_Part> nj_Parts_Wp = new List<nj_Part>();

    
    void LoadData(){
        textJson = Resources.Load<TextAsset>("nj_part");
        data = JsonMapper.ToObject(textJson.ToString());

        Debug.Log(data.Count);
        
        for (int i = 0; i < data.Count ; i++)
        {   
            nj_Part PartTemp = new nj_Part();
            int type =  int.Parse(data[i]["type"].ToString());
            if(type == 0){

                PartTemp = Readpart(data[i]["part"].ToString());
                nj_Parts_Head.Add(PartTemp);

            }else if(type == 1){

                PartTemp = Readpart(data[i]["part"].ToString());
                nj_Parts_Body.Add(PartTemp);

            }else if(type == 2){

                PartTemp = Readpart(data[i]["part"].ToString());
                nj_Parts_Leg.Add(PartTemp);

            }else if(type == 3){

                PartTemp = Readpart(data[i]["part"].ToString());
                nj_Parts_Wp.Add(PartTemp);
            }
        }
    }

    nj_Part Readpart(string _data){
        JsonData data2 = GetItem(_data);

        nj_Part PartTemp = new nj_Part();

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
