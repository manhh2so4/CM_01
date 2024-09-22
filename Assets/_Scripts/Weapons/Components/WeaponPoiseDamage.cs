using UnityEngine;

public class WeaponPoiseDamage : WeaponComponents<PoiseDamageData,AttackPoiseDamage> {
    
    WeaponDamage weaponDamage;
    private void PoiseDetectCollider2D(Collider2D other) {
        
        if(other.TryGetComponent(out IPoiseDamageable poiseDamageable)){
            if (Random.value > (currenAttackData.Rate/100)) return;
            poiseDamageable.DamagePoise(currenAttackData.Amount,currenAttackData.type,currenAttackData.prefabEff);
        }
    }
    protected override void SubscribeHandlers()
    {
        base.SubscribeHandlers();
        weaponDamage = GetComponent<WeaponDamage>();
        weaponDamage.KnockBackTrigger += PoiseDetectCollider2D;        
    }
    protected override void OnDisable()
    {
        base.OnDisable();
        weaponDamage.KnockBackTrigger -= PoiseDetectCollider2D;
    }
}