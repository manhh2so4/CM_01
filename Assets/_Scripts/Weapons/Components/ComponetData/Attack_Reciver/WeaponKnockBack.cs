using UnityEngine;

public class WeaponKnockBack : WeaponComponents<KnockBackData> {
    WeaponDamage weaponDamage;
    private CoreComp<Movement> movement;

     private void HandleDetectCollider2D(Collider2D collider){
        if(collider.TryGetComponent(out IKnockBackable knockBackable)){
            knockBackable.KnockBack(data.Angle, data.Strength, movement.Comp.facingDirection);
        }
    }


    protected override void SubscribeHandlers()
    {
        base.SubscribeHandlers();
        movement = new CoreComp<Movement>(Core);
        weaponDamage = GetComponent<WeaponDamage>();
        weaponDamage.KnockBackTrigger += HandleDetectCollider2D;        
    }
    protected override void OnDisable()
    {
        base.OnDisable();
        weaponDamage.KnockBackTrigger -= HandleDetectCollider2D;
    }
}