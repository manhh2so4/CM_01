using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;

public class Read_anim_skill : MonoBehaviour
{
    public static SkillPaint[] skillPaints;
    public TextAsset textJson;
    private JsonData data; 
    public List<SkillPaint> FindFX = new List<SkillPaint>();
    public int idFxFind = 1;
    
    private void Reset() {
        LoadData();
        FindIdSkill();
    }
    private void Awake() {
        LoadData();
    }
    void LoadData(){
        textJson = Resources.Load<TextAsset>("nj_skill");
        data = JsonMapper.ToObject(textJson.ToString());
        Debug.Log(data.Count);
        SkillPaint[] temp = new SkillPaint[data.Count];
        for (int i = 0; i < data.Count ; i++)
        {   
            SkillPaint skilId = new SkillPaint();
            skilId.idSkill =  int.Parse(data[i][0].ToString())-1; 
            JsonData data2 = GetItem(data[i][4].ToString());
            skilId.skillStand = new SkillInfoPaint[data2.Count];
            for (int j = 0; j < skilId.skillStand.Length; j++)
            {   
                SkillInfoPaint skillInfo = new SkillInfoPaint();
                skillInfo.status = int.Parse(data2[j]["status"].ToString());
                skillInfo.effS0Id = int.Parse(data2[j]["effS0Id"].ToString());
                skillInfo.e0dx = int.Parse(data2[j]["e0dx"].ToString());
                skillInfo.e0dy = int.Parse(data2[j]["e0dy"].ToString());
                skillInfo.effS1Id = int.Parse(data2[j]["effS1Id"].ToString());
                skillInfo.e1dx = int.Parse(data2[j]["e1dx"].ToString());
                skillInfo.e1dy = int.Parse(data2[j]["e1dy"].ToString());
                skillInfo.effS2Id = int.Parse(data2[j]["effS2Id"].ToString());
                skillInfo.e2dx = int.Parse(data2[j]["e2dx"].ToString());
                skillInfo.e2dy = int.Parse(data2[j]["e2dy"].ToString());
                skillInfo.arrowId = int.Parse(data2[j]["arrowId"].ToString());
                skillInfo.adx = int.Parse(data2[j]["adx"].ToString());
                skillInfo.ady = int.Parse(data2[j]["ady"].ToString());
                
                skilId.skillStand[j] = skillInfo;        
            }
            JsonData data3 = GetItem(data[i][5].ToString());
            skilId.skillfly = new SkillInfoPaint[data2.Count];
            for (int j = 0; j < skilId.skillfly.Length; j++)
            {   
                SkillInfoPaint skillInfo = new SkillInfoPaint();
                skillInfo.status = int.Parse(data2[j]["status"].ToString());
                skillInfo.effS0Id = int.Parse(data2[j]["effS0Id"].ToString());
                skillInfo.e0dx = int.Parse(data2[j]["e0dx"].ToString());
                skillInfo.e0dy = int.Parse(data2[j]["e0dy"].ToString());
                skillInfo.effS1Id = int.Parse(data2[j]["effS1Id"].ToString());
                skillInfo.e1dx = int.Parse(data2[j]["e1dx"].ToString());
                skillInfo.e1dy = int.Parse(data2[j]["e1dy"].ToString());
                skillInfo.effS2Id = int.Parse(data2[j]["effS2Id"].ToString());
                skillInfo.e2dx = int.Parse(data2[j]["e2dx"].ToString());
                skillInfo.e2dy = int.Parse(data2[j]["e2dy"].ToString());
                skillInfo.arrowId = int.Parse(data2[j]["arrowId"].ToString());
                skillInfo.adx = int.Parse(data2[j]["adx"].ToString());
                skillInfo.ady = int.Parse(data2[j]["ady"].ToString());
                
                skilId.skillfly[j] = skillInfo;        
            }            
            temp[i] = skilId;                            
        }
        skillPaints = temp;
    }
    JsonData GetItem(string data3){
        return JsonMapper.ToObject(data3);
    }
    void FindIdSkill(){
        
        for (int i = 0; i < skillPaints.Length; i++)
        {
            for (int j = 0; j < skillPaints[i].skillStand.Length; j++)
            {
                if(idFxFind == skillPaints[i].skillStand[j].effS0Id){
                    FindFX.Add(skillPaints[i]);
                }
            }
        }
    }
}
