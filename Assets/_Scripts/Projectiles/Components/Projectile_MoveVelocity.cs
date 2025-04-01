using UnityEngine;
namespace HStrong.ProjectileSystem
{
    [RequireComponent(typeof(mPhysic2D))]
    public class Projectile_MoveVelocity : ProjectileComponent {
        protected mPhysic2D rb;
        protected override void Awake() {
            base.Awake();
            rb = GetComponent<mPhysic2D>();
            rb.Gravity = 0;
        }
        protected override void Init(){
            base.Init();
            SetVelocity();        
        }
        protected override void OnDisable(){
            base.OnDisable();
            rb.Velocity = Vector2.zero;
        }
        private void SetVelocity(){
            rb.Velocity = projectile.Dir * projectile.speed;
            float angle = Mathf.Atan2(rb.Velocity.y, rb.Velocity.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }
}