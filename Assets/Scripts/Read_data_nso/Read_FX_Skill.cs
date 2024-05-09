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
    [SerializeField]
    public static SkillInfor1[] skillInfors;
    public SkillInfor1[] viewSkillInfors;
    public EffSkill[] effSkills;
    public List<SkillInfor1> FindInfor = new List<SkillInfor1>();
    public int idFxFind = 1;
    private void Awake() {
        LoadData();
        FindIdSkill();   
    }
    private void Reset() {
        LoadData(); 
        FindIdSkill();
        LoadEffInfo();   
    }
    void LoadData(){
        textJson = Resources.Load<TextAsset>("nj_effect");
        data = JsonMapper.ToObject(textJson.ToString());
        SkillInfor1[] temp = new SkillInfor1[data.Count];
        for (int i = 0; i < data.Count ; i++)
        {   
            SkillInfor1 skilId = new SkillInfor1();  
            JsonData data2 = GetItem(data[i][1].ToString());
            skilId.idSkillFx = i;
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
        viewSkillInfors = skillInfors;
    }
    void FindIdSkill(){
        
        for (int i = 0; i < skillInfors.Length; i++)
        {
            for (int j = 0; j < skillInfors[i].info.Length; j++)
            {
                if(idFxFind == skillInfors[i].info[j].imgId){
                    FindInfor.Add(skillInfors[i]);
                }
            }
        }
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
    void LoadEffInfo(){
        EffSkill[] temp = new EffSkill[skillInfors.Length+1];
        for (int i = 0; i < skillInfors.Length; i++)
        {

            EffSkill temp1 = new EffSkill();
            temp1.idEffSkill = i+1;
            temp1.effSkillInfos = new EffSkillInfo[skillInfors[i].info.Length];
            for (int j = 0; j < temp1.effSkillInfos.Length; j++)
            {
                EffSkillInfo a = new EffSkillInfo();
                a.texture2D = new Texture2D(1,1);
                a.texture2D.name = skillInfors[i].info[j].imgId.ToString();
                a.dx = skillInfors[i].info[j].dx;
                a.dy = skillInfors[i].info[j].dy;
                temp1.effSkillInfos[j] = a;
            }
            temp[i+1] = temp1;
        }
        effSkills = temp;
    } 
}
