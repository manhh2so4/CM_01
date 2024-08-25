using UnityEngine;
public class WeaponMovement : WeaponComponents<MovementData,AttackMovement> {

    private Movement  coreMove;
    private Movement  CoreMove => coreMove ? coreMove : Core.GetCoreComponent(ref coreMove);
    private void StartMovement(){

        CoreMove.SetVelocity(currenAttackData.Velocity,currenAttackData.Direction,coreMove.facingDirection);
    }
    private void StopMovement(){

        CoreMove.SetVelocityZero();
    }
    protected override void SubscribeHandlers()
    {
        base.SubscribeHandlers();
        weapon.OnStarMove += StartMovement;
        weapon.OnStopMove += StopMovement;
    }
    protected override void OnDisable()
    {
        base.OnDisable();        
        weapon.OnStarMove -= StartMovement;
        weapon.OnStopMove -= StopMovement;
    }
}