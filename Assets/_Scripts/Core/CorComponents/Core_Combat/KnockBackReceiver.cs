using System;
using UnityEngine;
public class KnockBackReceiver : CoreComponent,IKnockBackable
{

    private Movement movement;
    float knockBackStartTime;
    public float maxKnockbackTime = 0.5f;
    public bool isKnockBack;
    public event Action OnKnockBack;
    public override void LogicUpdate()
    {
        CheckKnockBack();
    }
    public void KnockBack(Vector2 angle, float strength, int direction)
    {
        movement.SetVelocity(strength, angle, direction);
        isKnockBack = true;
        knockBackStartTime = Time.unscaledTime;
        OnKnockBack?.Invoke();
    }
    private void CheckKnockBack()
    {
        if(isKnockBack){
            if( Time.unscaledTime >= knockBackStartTime + maxKnockbackTime)
            {
                isKnockBack = false;
            }   
        }
    }
    protected override void Awake()
    {
        base.Awake();
        movement = core.GetCoreComponent<Movement>();
    }
}
