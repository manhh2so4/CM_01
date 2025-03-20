using UnityEngine;

public class WeaponKnockBack : WeaponComponents<KnockBackData> {
    WeaponHitBox HitBox;
    private CoreComp<Movement> movement;

     private void HandleTrigger2D(Collider2D collider){
        if(collider.TryGetComponent(out IKnockBackable knockBackable)){
            knockBackable.KnockBack(data.Angle, data.Strength, movement.Comp.facingDirection);
        }
    }


    protected override void SubscribeHandlers()
    {
        base.SubscribeHandlers();
        movement = new CoreComp<Movement>(Core);
        HitBox = GetComponent<WeaponHitBox>();
        HitBox.Action += HandleTrigger2D;        
    }
    protected override void OnDisable()
    {
        base.OnDisable();
        HitBox.Action -= HandleTrigger2D;
    }
}