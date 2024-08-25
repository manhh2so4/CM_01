using UnityEngine;

public class WeaponKnockBack : WeaponComponents<KnockBackData,AttackKnockBack> {
     WeaponDamage weaponDamage;
     private CoreComp<Movement> movement;

     private void HandleDetectCollider2D(Collider2D collider){
        if(collider.TryGetComponent(out IKnockBackable knockBackable)){
            knockBackable.KnockBack(currenAttackData.Angle, currenAttackData.Strength, movement.Comp.facingDirection);
        }
    }
    private void Start() {

    }

    protected override void SubscribeHandlers()
    {
        base.SubscribeHandlers();
        movement = new CoreComp<Movement>(Core);
        weaponDamage = GetComponent<WeaponDamage>();
        weaponDamage.DetectedTrigger += HandleDetectCollider2D;        
    }
    protected override void OnDisable()
    {
        base.OnDisable();
        weaponDamage.DetectedTrigger -= HandleDetectCollider2D;
    }
}