using UnityEngine;
namespace HStrong.ProjectileSystem
{
    public class Projectile_Damage : ProjectileComponent {
        [SerializeField] private GameObject EffecHit;
        [SerializeField] LayerMask layerMask;
        private void Update() {
            DoDamage();
        }
        void DoDamage() {
            Collider2D hit = Physics2D.OverlapCircle(transform.position, .15f, layerMask);
            if(hit){
                IDamageable damageable;
                if(hit.TryGetComponent<IDamageable>(out damageable)){
                    if(hit.tag == gameObject.tag) return;
                    projectile.stats.DoDamage(damageable.Target(EffecHit));
                    remove();
                }
            }
        }
    }
    
}