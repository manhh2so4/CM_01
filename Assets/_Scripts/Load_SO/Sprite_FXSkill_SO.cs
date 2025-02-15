using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using NaughtyAttributes;

[CreateAssetMenu(fileName = "FX_Sprite", menuName = "GameData/SpriteSkillFX")]
public class Sprite_FXSkill_SO : ScriptableObject
{
        public int EffId;
        public SpriteInfo[] effSkillInfo_SO;
        [Button]
        public void LoadData() {

                string fileName = this.name.Substring(9);
                EffId = int.Parse(fileName);

                Sprite[] sprites = Resources.LoadAll<Sprite>("ToSlice/");
                SkillInfor1[] SkillInfor = Read_FX_Skill.LoadData();
                effSkillInfo_SO = null;
                effSkillInfo_SO = new SpriteInfo[SkillInfor[EffId-1].info.Length];
                for (int i = 0; i < effSkillInfo_SO.Length; i++)
                {
                        SpriteInfo inforTemp = new SpriteInfo();
                        inforTemp.dx = SkillInfor[EffId-1].info[i].x0;
                        inforTemp.dy = SkillInfor[EffId-1].info[i].y0;

                        foreach (Sprite item in sprites)
                        {
                                if(SkillInfor[EffId-1].info[i].ID == int.Parse(item.name)){
                                        inforTemp.sprite = item;                                        
                                }
                        }
                        if(inforTemp.sprite == null){
                                inforTemp.sprite = sprites[0];
                                inforTemp.sprite.name = SkillInfor[EffId-1].info[i].ID.ToString();

                        }        
                        effSkillInfo_SO[i] = inforTemp;                        
                }
                Debug.Log("LoadData: " + EffId);
        }
        [Button]
        public void Test(){
                string fileName = this.name.Substring(9);
                EffId = int.Parse(fileName);
        }
}
