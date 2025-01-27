using UnityEngine;
namespace HStrong.ProjectileSystem
{
    [RequireComponent(typeof(CapsuleCollider2D))]
    public class Projectile_Damage : ProjectileComponent {
        [SerializeField] private GameObject EffecHit;
        private void OnTriggerEnter2D(Collider2D other) {
        IDamageable damageable;
        if(other.TryGetComponent<IDamageable>(out damageable)) {
            if(other.tag == gameObject.tag) return;
            projectile.stats.DoDamage(damageable.Target(EffecHit));
            remove();
        }
    }
    }
}