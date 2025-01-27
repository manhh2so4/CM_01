using UnityEngine;
namespace HStrong.ProjectileSystem
{
    [RequireComponent(typeof(Projectile))]
    public class ProjectileComponent : MonoBehaviour {
        protected Projectile projectile;
        protected bool Active { get; private set; }
        protected virtual void Awake() {
            projectile = GetComponent<Projectile>();
            projectile.OnInit += Init;
            projectile.OnReset += ResetProjectile;
        }
        protected virtual void Init()
        {
            SetActive(true);
        }
        protected virtual void ResetProjectile()
        {
            
        }
        public virtual void SetActive(bool value) => Active = value;
        protected virtual void remove()
        {
            projectile.OnInit -= Init;
            projectile.OnReset -= ResetProjectile;
            Destroy(gameObject);
        }
    }
}