using UnityEngine;
namespace HStrong.ProjectileSystem
{
    [RequireComponent(typeof(Projectile))]
    public class ProjectileComponent : MonoBehaviour {
        protected Projectile projectile;
        protected virtual void Awake() {
            projectile = GetComponent<Projectile>();
            projectile.OnInit += Init;
        }
        protected virtual void Init()
        {
            
        }
        protected virtual void remove()
        {
            projectile.Destroy();
        }
        protected virtual void OnDisable(){}    
    }
}