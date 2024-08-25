using Bardent.Weapons.Components;
using UnityEngine;

public class WeaponPoiseDamage : WeaponComponents<PoiseDamageData,AttackPoiseDamage> {
    
    private void OnTriggerEnter2D(Collider2D other) {
        
        if(other.TryGetComponent(out IPoiseDamageable poiseDamageable)){
            poiseDamageable.DamagePoise(currenAttackData.Amount);
        }
    }
}