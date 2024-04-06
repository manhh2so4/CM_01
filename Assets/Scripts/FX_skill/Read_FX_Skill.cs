using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using Unity.VisualScripting;
using System.IO;
public class Read_FX_Skill : MonoBehaviour
{
    //public string textJson;
    public TextAsset textJson;
    private JsonData data; 
    public static SkillInfor[] skillInfors;
    private void Awake() {
        LoadData();
    }
    private void Reset() {
                
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
    
    void show(){
        string a =null;
        for (int i = 0; i < skillInfors.Length; i++)
        {   
            a += "[" + i + "]-" + skillInfors[i].info.Length +"-: ";
            for (int j = 0; j < skillInfors[i].info.Length; j++)
            {
                a += skillInfors[i].info[j].imgId.ToString()+" | ";
            }
            a +="\n";
        }
        Debug.Log(a);
        WriteToFile(filePath, a);
    }
}
