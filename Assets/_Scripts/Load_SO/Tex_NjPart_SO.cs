using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "Ao_lv", menuName = "GameData/Tex_NjPart_SO")]
public class Tex_NjPart_SO : ScriptableObject
{
       public EquipType PartType;
       public SpriteInfo[] spriteInfos;
       public int idPart;
       
#if UNITY_EDITOR
       [Button("AddData")]
       void AddData()
       {
              AddData(idPart, PartType);
       }
       public void AddData(int idPart, EquipType equipType)
       {
              PartType = equipType;
              Frames partTemp = Read_Nj_part.Instance.nj_Parts[idPart];
              spriteInfos = new SpriteInfo[ partTemp.imageIDs.Length ];
              
              //GetSpritesID.SetSprite();
              
              for(int i = 0; i < spriteInfos.Length; i++)
              {
                     SpriteInfo spriteInfoTemp = new SpriteInfo();
                     if(i==0 && ( PartType == EquipType.Ao || PartType == EquipType.Quan)){
                        spriteInfoTemp.sprite = GetSpritesID.Get()[ 0 ];
                     }
                     else{
                        spriteInfoTemp.sprite = GetSpritesID.Get()[ partTemp.imageIDs[i].ID ];
                     }
                     spriteInfoTemp.dx = partTemp.imageIDs[i].x0;
                     spriteInfoTemp.dy = partTemp.imageIDs[i].y0;

                     spriteInfos[i] = spriteInfoTemp;
              }
       }
#endif
}


