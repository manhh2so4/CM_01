using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

[CreateAssetMenu(fileName = "DataNPC_SO", menuName = "DataNPC_SO", order = 0)]
public class DataNPC_SO : ScriptableObject {
   public string NameNPC;
   public SpriteInfo[] spriteInfos;

   public float speedMove = 2;
   [SerializeField] float maxJumpHeight = 4;
	[SerializeField] float minJumpHeight = 1;
   [SerializeField] float timeToJumpApex = 0.4f;
   [HideInInspector] public float MaxJumpVel = 0;
   [HideInInspector] public float MinJumpVel = 0;


   public SkillData_SO skill_1;
   public SkillData_SO skill_2;
   public SkillData_SO skill_3;

   public float GetGravity(){
		float gravity = -(2 * maxJumpHeight) / Mathf.Pow (timeToJumpApex, 2);
		MaxJumpVel = Mathf.Abs(gravity) * timeToJumpApex;
		MinJumpVel = Mathf.Sqrt (2 * Mathf.Abs (gravity) * minJumpHeight);
      return gravity;
    }
#if UNITY_EDITOR
   [Button]
   void AddData(){
      if(spriteInfos.Length != 3) return;
         //nj_Part partTemp = new nj_Part();
         FindNjPart( 0 , Read_Nj_part.Instance.nj_Parts_Head );
         FindNjPart( 1 , Read_Nj_part.Instance.nj_Parts_Body );
         FindNjPart( 2 , Read_Nj_part.Instance.nj_Parts_Leg );   
   }
   void FindNjPart(int index , List<nj_Part> _Parts){

      int IDFind = int.Parse(spriteInfos[index].sprite.name);
      for (int i = 0; i < _Parts.Count; i++)
      {
         foreach (var item in _Parts[i].imageIDs)
         {
            if( item.ID == IDFind ){
               spriteInfos[index].dx = item.x0;
               spriteInfos[index].dy = item.y0;
               Debug.Log("seeee");
               return;
            }     
         }
      }
   }
#endif
}