using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;

[CreateAssetMenu(fileName = "FX_Sprite", menuName = "GameData/SpriteSkillFX")]
public class Sprite_FXSkill_SO : ScriptableObject
{
        public int EffId;
        public EffSkillInfo_Sprite[] effSkillInfo_SO;
        private void Reset() {
                #if UNITY_EDITOR
                string assetPath = UnityEditor.AssetDatabase.GetAssetPath(this);
                string fileName = System.IO.Path.GetFileNameWithoutExtension(assetPath).Substring(9);
                Debug.Log("sett___" + fileName);
                #endif

                EffId = int.Parse(fileName);
                Sprite[] sprites = Resources.LoadAll<Sprite>("ToSlice/");
                SkillInfor1[] SkillInfor = Read_FX_Skill.LoadData();
                effSkillInfo_SO = new EffSkillInfo_Sprite[SkillInfor[EffId-1].info.Length];
                //Sprite temp;
                for (int i = 0; i < effSkillInfo_SO.Length; i++)
                {
                        EffSkillInfo_Sprite inforTemp = new EffSkillInfo_Sprite();
                        inforTemp.dx = SkillInfor[EffId-1].info[i].dx;
                        inforTemp.dy = SkillInfor[EffId-1].info[i].dy;
                        foreach (Sprite item in sprites)
                        {
                        if(SkillInfor[EffId-1].info[i].imgId == int.Parse(item.name)){
                                inforTemp.sprite = item;
                        }
                        }
                        effSkillInfo_SO[i] = inforTemp;                        
                }

                Debug.Log("sett___" + sprites.Length);
        }
}
