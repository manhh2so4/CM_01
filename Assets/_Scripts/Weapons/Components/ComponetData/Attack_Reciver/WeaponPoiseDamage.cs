using UnityEngine;

public class WeaponPoiseDamage : WeaponComponents<PoiseDamageData> {
    
    WeaponHitBox HitBox;
    private void PoiseTrigger2D(Collider2D other) {
        
        if(other.TryGetComponent(out IPoiseDamageable poiseDamageable)){
            if (Random.value > (data.Rate/100)) return;
            poiseDamageable.DamagePoise(data.TimeEff,data.type,data.prefabEff);
        }
    }
    protected override void SubscribeHandlers()
    {
        base.SubscribeHandlers();
        HitBox = GetComponent<WeaponHitBox>();
        HitBox.Action += PoiseTrigger2D;        
    }
    public override void Refest()
    {
        base.Refest();
        HitBox.Action -= PoiseTrigger2D;
    }
}