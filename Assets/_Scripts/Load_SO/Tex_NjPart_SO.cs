using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
[CreateAssetMenu(fileName = "Ao_lv", menuName = "GameData/Tex_NjPart_SO")]
public class Tex_NjPart_SO : ScriptableObject
{
       public njPart Part;
       [SerializeField] private Sprite[] importSprites;
       public SpriteInfo[] spriteInfos;
       public void AddData()
       {
              // if(spriteInfos == null) return;

              nj_Part partTemp = new nj_Part();
              switch (Part)
              {
                     case njPart.Head:
                            FindNjPart(ref partTemp,Read_Nj_part.Instance.nj_Parts_Head);
                     break;

                     case njPart.Body:
                            FindNjPart(ref partTemp,Read_Nj_part.Instance.nj_Parts_Body);
                     break;

                     case njPart.Leg:
                            FindNjPart(ref partTemp,Read_Nj_part.Instance.nj_Parts_Leg);
                     break;

                     case njPart.Weapon:
                            FindNjPart(ref partTemp,Read_Nj_part.Instance.nj_Parts_Wp);
                     break;
              }   

              Debug.Log(partTemp.imageIDs.Length);  

              spriteInfos = new SpriteInfo[importSprites.Length];
              for (int i = 0; i < importSprites.Length; i++)
              {
                     SpriteInfo SpriteInfoTemp = new SpriteInfo();
                     SpriteInfoTemp.sprite = importSprites[i];

                     foreach (var item in partTemp.imageIDs)
                     {
                            if(item.ID == int.Parse(importSprites[i].name)){
                                  SpriteInfoTemp.dx = item.x0;
                                  SpriteInfoTemp.dy = item.y0;
                            }
                     }
                     spriteInfos[i] = SpriteInfoTemp;                                        
              }

       }
       void FindNjPart(ref nj_Part part , List<nj_Part> _Parts){

              Debug.Log("quan" + _Parts.Count);
              
              for (int i = 0; i < _Parts.Count; i++)
              {
                     foreach (var item in _Parts[i].imageIDs)
                     {
                            if( item.ID == int.Parse(importSprites[1].name)){
                                   part =  _Parts[i];
                                   Debug.Log("seeee");
                                   return;
                     }     
                     }
                     
              }
       }
}

