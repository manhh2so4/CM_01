using UnityEngine;
namespace HStrong.ProjectileSystem
{
    public class Projectile_Damage : ProjectileComponent {
        [SerializeField] private Effect_Instance EffecHit;
        [SerializeField] LayerMask layerMask;
        [SerializeField] float radius = .15f;
        [SerializeField] Vector2 offset = new Vector2(0,0);
        private void Update() {
            DoDamage();
        }
        void DoDamage() {
            Collider2D hit = Physics2D.OverlapCircle(transform.position + (Vector3)offset, radius, layerMask);
            if(hit){
                IDamageable damageable;
                if(hit.TryGetComponent<IDamageable>(out damageable)){
                    if(hit.tag == gameObject.tag) return;

                    if(projectile.stats != null){
                        projectile.stats.DoDamage(damageable.Target(EffecHit));
                        
                    }
                    else{
                        damageable.Target(EffecHit).DoDamage(projectile.damage);
                    }
                    remove();   
                }
                if(hit.TryGetComponent<IKnockBackable>(out IKnockBackable knockBackable)){
                    if(hit.tag == gameObject.tag) return;
                    knockBackable.KnockBack();
                }
            }
        }
        private void OnDrawGizmos() {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position + (Vector3)offset, radius);
        }

    }
    
}