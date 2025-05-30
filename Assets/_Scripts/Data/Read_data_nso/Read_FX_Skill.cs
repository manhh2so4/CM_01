using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using NaughtyAttributes;
public class Read_FX_Skill : MonoBehaviour
{
    //public string textJson;
    public static TextAsset textJson;
    private static JsonData data; 
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
    [Button]
    public static SkillInfor1[] LoadData(){
        textJson = Resources.Load<TextAsset>("nj_effect");
        data = JsonMapper.ToObject(textJson.ToString());

        SkillInfor1[] temp = new SkillInfor1[data.Count];
        for (int i = 0; i < data.Count ; i++)
        {   
            SkillInfor1 skilId = new SkillInfor1();  
            JsonData data2 = GetItem(data[i][1].ToString());
            skilId.idSkillFx = i;
            skilId.info = new ImageID[data2.Count];
            for (int j = 0; j < skilId.info.Length; j++)
            {   
                ImageID formImg = new ImageID();
                formImg.ID = int.Parse(data2[j][0].ToString());
                formImg.x0 = int.Parse(data2[j][1].ToString());
                formImg.y0 = int.Parse(data2[j][2].ToString());
                skilId.info[j] = formImg;        
            }            
            temp[i] = skilId;  
                                      
        }
        skillInfors = temp;
        
        return temp;
    }
    [Button]
    void showInfor(){
        viewSkillInfors = LoadData();
    }
    void FindIdSkill(){
        
        for (int i = 0; i < skillInfors.Length; i++)
        {
            for (int j = 0; j < skillInfors[i].info.Length; j++)
            {
                if(idFxFind == skillInfors[i].info[j].ID){
                    FindInfor.Add(skillInfors[i]);
                }
            }
        }
    }
    static JsonData GetItem(string data3){
        return JsonMapper.ToObject(data3);
    }
    
    void show(){
        string a =null;
        for (int i = 0; i < skillInfors.Length; i++)
        {   
            a += "[" + i + "]-" + skillInfors[i].info.Length +"-: ";
            for (int j = 0; j < skillInfors[i].info.Length; j++)
            {
                a += skillInfors[i].info[j].ID.ToString()+" | ";
            }
            a +="\n";
        }
        Debug.Log(a);
    }
    void LoadEffInfo(){
        EffSkill[] temp = new EffSkill[skillInfors.Length+1];
        for (int i = 0; i < skillInfors.Length; i++)
        {

            EffSkill temp1 = new EffSkill();
            temp1.idEffSkill = i+1;
            for (int j = 0; j < temp1.effSkillInfos.Length; j++)
            {
                Texture2DInfo a = new Texture2DInfo();
                a.texture2D = new Texture2D(1,1);
                a.texture2D.name = skillInfors[i].info[j].ID.ToString();
                a.dx = skillInfors[i].info[j].x0;
                a.dy = skillInfors[i].info[j].y0;
            }
            temp[i+1] = temp1;
        }
        effSkills = temp;
    } 
}
