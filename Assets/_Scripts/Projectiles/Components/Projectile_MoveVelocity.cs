using UnityEngine;
namespace HStrong.ProjectileSystem
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Projectile_MoveVelocity : ProjectileComponent {
        protected Rigidbody2D rb;
        protected override void Awake() {
            base.Awake();
            rb = GetComponent<Rigidbody2D>();
        }
        protected override void Init(){
            base.Init();
            SetVelocity();        
        }
        private void SetVelocity(){
            rb.velocity = (projectile.Dir) * projectile.speed;
            float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }
}