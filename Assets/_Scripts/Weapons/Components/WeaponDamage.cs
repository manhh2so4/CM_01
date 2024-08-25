using System;
using UnityEngine;

public class WeaponDamage : WeaponComponents<DamageData,AttackDame> {
    public Action<Collider2D> DetectedTrigger;
    private void OnTriggerEnter2D(Collider2D other) {
        
        if(other.TryGetComponent(out IDamageable damageable)){
            damageable.Damage(currenAttackData.Amout);
            DetectedTrigger?.Invoke(other);
        }
    }
}