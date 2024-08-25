using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;

[CreateAssetMenu(fileName = "FX_Sprite", menuName = "GameData/SpriteSkillFX")]
public class Sprite_FXSkill_SO : ScriptableObject
{
        public int EffId;
        public SpriteInfo[] effSkillInfo_SO;
        private void Reset() {

                #if UNITY_EDITOR
                string assetPath = UnityEditor.AssetDatabase.GetAssetPath(this);
                string fileName = System.IO.Path.GetFileNameWithoutExtension(assetPath).Substring(9);
                Debug.Log("sett___" + fileName);
                
                #endif

                EffId = int.Parse(fileName);
                Sprite[] sprites = Resources.LoadAll<Sprite>("ToSlice/");
                SkillInfor1[] SkillInfor = Read_FX_Skill.LoadData();
                effSkillInfo_SO = new SpriteInfo[SkillInfor[EffId-1].info.Length];
                //Sprite temp;
                for (int i = 0; i < effSkillInfo_SO.Length; i++)
                {
                        SpriteInfo inforTemp = new SpriteInfo();
                        inforTemp.dx = SkillInfor[EffId-1].info[i].dx;
                        inforTemp.dy = SkillInfor[EffId-1].info[i].dy;
                        foreach (Sprite item in sprites)
                        {
                                if(SkillInfor[EffId-1].info[i].imgId == int.Parse(item.name)){
                                        inforTemp.sprite = item;
                                }else
                                {
                                        inforTemp.sprite = null;
                                }
                        }
                        effSkillInfo_SO[i] = inforTemp;                        
                }

                Debug.Log("sett___" + sprites.Length);
        }
}
