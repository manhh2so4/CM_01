using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using NaughtyAttributes;
using UnityEditor;
using System.Threading.Tasks;

[CreateAssetMenu(fileName = "FX_Sprite", menuName = "GameData/SpriteSkillFX")]
public class Sprite_FXSkill_SO : ScriptableObject
{
        [SerializeField] int EffId;
        public SpriteInfo[] effSkillInfo;
#if UNITY_EDITOR
        
        static Sprite[] sprites ;
        [Button]
        public void LoadData() {

        string fileName = this.name.Substring(9);
        EffId = int.Parse(fileName);
        if( sprites == null) {
                sprites = Resources.LoadAll<Sprite>("ToSlice/");
        }
        SkillInfor1[] SkillInfor = Read_FX_Skill.LoadData();
        effSkillInfo = null;
        effSkillInfo = new SpriteInfo[SkillInfor[EffId-1].info.Length];
        Texture2D texture = sprites[0].texture;

        for (int i = 0; i < effSkillInfo.Length; i++)
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
                        inforTemp.sprite = sprites[i];
                        inforTemp.sprite.name = SkillInfor[EffId-1].info[i].ID.ToString();

                }        
                effSkillInfo[i] = inforTemp; 

        }
        
        EditorUtility.SetDirty(this);
        }
#endif
}
