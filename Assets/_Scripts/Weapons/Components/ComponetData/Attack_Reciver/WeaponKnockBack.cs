using UnityEngine;

public class WeaponKnockBack : WeaponComponents<KnockBackData> {
    WeaponHitBox HitBox;
    private void HandleTrigger2D(Collider2D collider){
        if(collider.TryGetComponent(out IKnockBackable knockBackable)){
            knockBackable.KnockBack(data.Angle, data.Strength, coreMove.facingDirection);
        }
    }


    protected override void SubscribeHandlers()
    {
        base.SubscribeHandlers();
        HitBox = GetComponent<WeaponHitBox>();
        HitBox.Action += HandleTrigger2D;        
    }
    public override void Refest()
    {
        base.Refest();
        HitBox.Action -= HandleTrigger2D;
    }
}