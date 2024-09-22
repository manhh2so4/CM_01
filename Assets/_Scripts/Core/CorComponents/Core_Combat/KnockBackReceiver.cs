using System;
using UnityEngine;
public class KnockBackReceiver : CoreComponent,IKnockBackable
{

    private Movement movement;
    public float maxKnockbackTime = 0.2f;
    public bool isKnockBackActive;
    private float knockBackStartTime;
    
    public override void LogicUpdate()
    {
        CheckKnockBack();
    }
    public void KnockBack(Vector2 angle, float strength, int direction)
    {
        movement.SetVelocity(strength, angle, direction);
        isKnockBackActive = true;
        knockBackStartTime = Time.time;
    }
    private void CheckKnockBack()
    {
        if( Time.time >= knockBackStartTime + maxKnockbackTime)
        {
            isKnockBackActive = false;
            //movement.SetVelocityZero();
            //movement.Comp.CanSetVelocity = true;
        }
    }
    protected override void Awake()
    {
        base.Awake();
        movement = core.GetCoreComponent<Movement>();
    }
}
