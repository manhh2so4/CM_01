using UnityEngine;
namespace HStrong.ProjectileSystem
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class Projectile_Graphics : ProjectileComponent {
        [SpritePreview]
        [SerializeField] Sprite[] sprites;
         Pivot anchor;
        [SerializeField] float speedAnim;
        public float life = 2;
        int FrameCurrent = 0; 
        float startTime = 0,offsetY = 0;
        SpriteRenderer mSPR;

        protected override void Init(){
            base.Init();
            FrameCurrent = 0;
            frameTimer = 99;
            startTime = Time.time;
            Paint();            
        }
        private void SetPivot(){
            float h = sprites[0].rect.height/100;
            float w = sprites[0].rect.width/100;
            switch (anchor)
            {               
                case Pivot.Top:
                    offsetY = -h/2;
                break;
                case Pivot.Bot:
                    offsetY = h/2 - 0.02f;
                break;
                case Pivot.Center:
                break;
            }
            Vector3 newPosition = new Vector3(0,offsetY,0);
            this.transform.localPosition += newPosition;
        }
        protected override void Awake() {
            base.Awake();
            mSPR = GetComponent<SpriteRenderer>();
        }
        private void Paint(){ 
            if(sprites == null) return;
            mSPR.sprite = sprites[FrameCurrent];          
        }
        #region Play_anim
        private void Update(){
            if(sprites.Length > 1){
                PlayEffect();
            }
        }
        void PlayEffect(){   
            if(FrameRate(speedAnim)) return;
            Paint();
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