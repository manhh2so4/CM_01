using UnityEngine;
public class WeaponMovement : WeaponComponents<MovementData> {

    private void StartMovement(){

        coreMove.SetVelocity(data.Velocity, data.Direction, coreMove.facingDirection);
    }
    private void StopMovement(){

        coreMove.SetVelocityZero();
    }
    protected override void SubscribeHandlers()
    {
        base.SubscribeHandlers();
        weapon.OnStarMove += StartMovement;
        weapon.OnStopMove += StopMovement;
    }
    public override void Refest()
    {
        base.Refest();        
        weapon.OnStarMove -= StartMovement;
        weapon.OnStopMove -= StopMovement;
    }
}