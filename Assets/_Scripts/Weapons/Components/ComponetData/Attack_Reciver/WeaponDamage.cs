using System;
using UnityEngine;

public class WeaponDamage : WeaponComponents<DamageData> {

    WeaponHitBox hitBox;
    CharacterStats stats;
    private void TriggerDame(Collider2D other) {

        if(other.TryGetComponent(out IDamageable damageable)){
            
            stats.DoDamage(damageable.Target(data.prefabHit));

        }
    }
    protected override void SubscribeHandlers()
    {
        base.SubscribeHandlers();
        stats = Core.GetCoreComponent<CharacterStats>();
        hitBox = GetComponent<WeaponHitBox>();
        hitBox.Action += TriggerDame;

    }
    protected override void OnDisable()
    {
        base.OnDisable();
        hitBox.Action -= TriggerDame;
    }
    protected override void HandleEnter()
    {
        base.HandleEnter();
        stats.GetStatOfType(StatType.damage).AddModifier(data.Amout);
    }
    protected override void HandleExit()
    {
        base.HandleExit();
        stats.GetStatOfType(StatType.damage).RemoveModifier(data.Amout);
    }
}
