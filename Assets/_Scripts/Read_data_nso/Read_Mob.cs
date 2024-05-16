using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using Unity.VisualScripting;
using System.IO;
public class Read_Mob : MonoBehaviour
{
    //public string textJson;
    public TextAsset textJson;
    private JsonData data; 

    public MobData[] mobDatas;
    public List<MobData> FindMobData = new List<MobData>();
    public int idFxFind = 1;
    private void Awake() {
        LoadData();
        FindIdSkill();   
    }
    private void Reset() {
        LoadData(); 
        FindIdSkill();  
    }
    void LoadData(){
        textJson = Resources.Load<TextAsset>("mob");
        string mystring = textJson.ToString();
        data = JsonMapper.ToObject(mystring);
        Debug.Log(data.Count); 
        MobData[] temp = new MobData[data.Count];
        for (int i = 0; i < data.Count ; i++)
        {      
            MobData tempId = new MobData();
            JsonData data2 = GetItem(data[i]["Img1"].ToString());
            tempId.imageInfors = new ImageInfor[data2.Count];
             for (int j = 0; j < data2.Count; j++)
            {
                ImageInfor image = new ImageInfor();
                image.ID = int.Parse(data2[j]["ID"].ToString());
                image.x0 = int.Parse(data2[j]["x0"].ToString());
                image.y0 = int.Parse(data2[j]["y0"].ToString());
                image.w = int.Parse(data2[j]["w"].ToString());
                image.h = int.Parse(data2[j]["h"].ToString());
                tempId.imageInfors[j] = image;
            }

            JsonData data3 = GetItem(data[i]["frameBoss"].ToString());
            tempId.frameBosses = new FrameBoss[data3.Count];
            for (int j = 0; j < data3.Count; j++)
            {
                
                FrameBoss frameTemp = new FrameBoss();
                int[] _dx = new int[data3[j]["dx"].Count];
                int[] _dy = new int[data3[j]["dy"].Count];
                int[] _Id = new int[data3[j]["idImg"].Count];
                for (int k = 0; k < data3[j]["dx"].Count; k++)
                {
                    _dx[k] = int.Parse(data3[j]["dx"][k].ToString());
                    _dy[k] = int.Parse(data3[j]["dy"][k].ToString());
                    _Id[k] = int.Parse(data3[j]["idImg"][k].ToString());
                    frameTemp.dx =  _dx;
                    frameTemp.dy =  _dy;
                    frameTemp.idImg =  _Id;              
                }
                tempId.frameBosses[j] = frameTemp;

            }
            temp[i] = tempId;
            //if(i==114) Debug.Log( data[i]["frameBossAttack"]);
            JsonData data4 = GetItem(data[i]["frameBossAttack"].ToString());
            try
            {
                tempId.frameBossAttack = new int[data4[0].Count];
                for (int j = 0; j < data4[0].Count; j++)
                {
                tempId.frameBossAttack[j] = int.Parse(data4[0][j].ToString());
                }
            }
            catch (System.Exception)
            {
                continue;
            }

            temp[i] = tempId;

            JsonData data5 = GetItem(data[i]["frameBossMove"].ToString());
            try
            {
                tempId.frameBossMove = new int[data5.Count];
                for (int j = 0; j < data5.Count; j++)
                {
                tempId.frameBossMove[j] = int.Parse(data5[j].ToString());
                }
            }
            catch (System.Exception)
            {
                continue;
            }       

            temp[i] = tempId;
        }
        mobDatas = temp;
    }
    void FindIdSkill(){
        
        
    }
    JsonData GetItem(string data3){
        return JsonMapper.ToObject(data3);
    }
    
    public string filePath = "Assets/Output.txt";
    void WriteToFile(string path, string text)
    {
        // Kiểm tra xem file có tồn tại không
        if (!File.Exists(path))
        {
            // Nếu không tồn tại, tạo mới file
            using (StreamWriter writer = File.CreateText(path))
            {
                // Viết chuỗi vào file
                writer.WriteLine(text);
            }
        }
        else
        {
            // Nếu file đã tồn tại, ghi đè nội dung cũ bằng nội dung mới
            File.WriteAllText(path, text);
        }

        Debug.Log("Successfully wrote to file: " + path);
    } 
}
