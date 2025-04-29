using System;
using NaughtyAttributes;
using UnityEngine;

public class WeaponDamage : WeaponComponents<DamageData> {

    [SerializeField] WeaponHitBox hitBox;
    [SerializeField] CharacterStats stats;
    private void TriggerDame(Collider2D other) {

        if(other.TryGetComponent(out IDamageable damageable)){
            stats.DoDamage( damageable.GetTarget(data.prefabHit) );
        }
    }
    [Button]
    protected override void SubscribeHandlers()
    {
        base.SubscribeHandlers();
        stats = Core.GetCoreComponent<CharacterStats>();
        hitBox = GetComponent<WeaponHitBox>();
        hitBox.Action += TriggerDame;
    }

    public override void Refest()
    {
        base.Refest();
        hitBox.Action -= TriggerDame;
    }
    protected override void HandleEnter()
    {
        base.HandleEnter();
        stats.AddModifier(StatType.damage, data.Amout);
    }
    protected override void HandleExit()
    {
        base.HandleExit();
        stats.RemoveModifier(StatType.damage, data.Amout);
    }
}
