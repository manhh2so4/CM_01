using NaughtyAttributes;
using UnityEngine;

public class NPC : MonoBehaviour {
   [Expandable]
   [SerializeField] DataNPC_SO dataNPC_SO;

   SpriteInfo head,body,leg;
   GameObject HeadGO,BodyGO,LegGO;
   int cf;
   private readonly int[][][] CharInfo = new int[2][][]{
      new int[3][]
		{
			new int[2] { -10, 32 },
			new int[2] { -7, 7 },
			new int[2] { -11, 15 },
		},
		new int[3][]
		{
			new int[2] {  -10, 33 },
			new int[2] {  -7, 7 },
			new int[2] {  -11, 16 },
		}
   };
   [Button]
   void Awake()
   {
      LoadCompnents();
      LoadData();
      PaintChar(0);
   }
   void LoadData(){
      head = dataNPC_SO.spriteInfos[0];
      body = dataNPC_SO.spriteInfos[1];
      leg = dataNPC_SO.spriteInfos[2];
   }
   void LoadCompnents(){
      LegGO = transform.Find("Leg").gameObject;
		HeadGO = transform.Find("Head").gameObject;
		BodyGO = transform.Find("Body").gameObject;	
   }
   
   private void PaintChar(int cf){
      mPaint.Paint(BodyGO, body.sprite, CharInfo[cf][2][0] + body.dx, CharInfo[cf][2][1] - body.dy, 0);
      mPaint.Paint(HeadGO, head.sprite, CharInfo[cf][0][0] + head.dx, CharInfo[cf][0][1] - head.dy, 0);
      mPaint.Paint(LegGO,  leg.sprite,  CharInfo[cf][1][0] + leg.dx,  CharInfo[cf][1][1] - leg.dy, 0);
   }
   private void Update() {
      frameTimer += Time.deltaTime;
      if(TimeRate(1f/(3))){ 
         cf = ( cf + 1 ) % 2; 
         PaintChar(cf);						     
      }
   }
   float frameTimer = 0;
   bool TimeRate(float timeWait){        
        frameTimer += Time.deltaTime;
        if(frameTimer >= timeWait){
            frameTimer = 0;
            return true;
        }
        return false;
    }
}