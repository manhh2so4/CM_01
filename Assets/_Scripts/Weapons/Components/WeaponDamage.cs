using System;
using UnityEngine;

public class WeaponDamage : WeaponComponents<DamageData,AttackDame> {
    public Action<Collider2D> KnockBackTrigger;
    public Action<Collider2D> PoiseTrigger;
    CharacterStats stats;

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.TryGetComponent(out IDamageable damageable)){
            stats.DoDamage(damageable.Target(currenAttackData.prefabHit));
            
            PoiseTrigger?.Invoke(other);
            KnockBackTrigger?.Invoke(other);  

        }
    }
    protected override void SubscribeHandlers()
    {
        base.SubscribeHandlers();
        stats = Core.GetCoreComponent<CharacterStats>();
               
    }
}