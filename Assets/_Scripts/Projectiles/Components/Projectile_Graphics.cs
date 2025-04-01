using UnityEngine;
namespace HStrong.ProjectileSystem
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class Projectile_Graphics : ProjectileComponent {
        [SpritePreview]
        [SerializeField] Sprite[] sprites;
        [SerializeField] float speedAnim;
        int FrameCurrent = 0; 
        SpriteRenderer mSPR;

        protected override void Init(){
            base.Init();
            FrameCurrent = 0;
            frameTimer = 99;  
        }
        
        protected override void Awake() {
            base.Awake();
            mSPR = GetComponent<SpriteRenderer>();
        }
        #region Play_anim
        private void Update(){
            if(sprites.Length > 1){
                PlayEffect();
            }
        }
        void PlayEffect(){   
            if(FrameRate(speedAnim)) return;
            mSPR.sprite = sprites[FrameCurrent];
            FrameCurrent = ((FrameCurrent + 1)%sprites.Length);
        }
        
        bool FrameRate(float speed){        
            frameTimer += Time.deltaTime;
            if(frameTimer >= speed){
                frameTimer = 0;
                return false;
            }
            return true;
        }
        float frameTimer = 0;
        #endregion
    }
}
// private void SetPivot(){
//             float h = sprites[0].rect.height/100;
//             float w = sprites[0].rect.width/100;
//             switch (anchor)
//             {               
//                 case Pivot.Top:
//                     offsetY = -h/2;
//                 break;
//                 case Pivot.Bot:
//                     offsetY = h/2 - 0.02f;
//                 break;
//                 case Pivot.Center:
//                 break;
//             }
//             Vector3 newPosition = new Vector3(0,offsetY,0);
//             this.transform.localPosition += newPosition;
//}