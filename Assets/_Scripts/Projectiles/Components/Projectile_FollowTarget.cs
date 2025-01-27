using UnityEngine;
namespace HStrong.ProjectileSystem
{
    public class Projectile_FollowTarget : ProjectileComponent {
        Transform target;
        float speed;
        float rotationSpeed = 5f;
        Vector3 vOffset ;
        
        void Update()
        {
            if (target == null) return;
            Vector3 direction = (target.position + vOffset - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            
        }
        protected override void Init(){
            base.Init();
            this.target = projectile.target;
            if (target == null) return;
            vOffset = new Vector3(0, target.GetComponent<CapsuleCollider2D>().size.y /2, 0);
            this.speed = projectile.speed; 
        }
    }
}