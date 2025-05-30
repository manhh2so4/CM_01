using UnityEngine;
namespace HStrong.ProjectileSystem
{
    [RequireComponent(typeof(mPhysic2D))]
    public class Projectile_Explore : ProjectileComponent {
        protected mPhysic2D rb;
        public BaseEffect effectCol;
        protected override void Awake() {
            base.Awake();
            rb = GetComponent<mPhysic2D>();
        }
        protected override void Init(){
            base.Init();        
        }
        private void Update(){
            if(rb.collisionInfor.below){
                BaseEffect effect = PoolsContainer.GetObject(effectCol,transform.position);
                remove();
            }
        }

    }
}