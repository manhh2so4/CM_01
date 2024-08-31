using UnityEngine;
public class KnockBackReceiver : CoreComponent,IKnockBackable
{
    private CoreComp<Movement> movement;
    public float maxKnockbackTime = 0.2f;

    public bool isKnockBackActive;
    private float knockBackStartTime;

    public override void LogicUpdate()
    {
        CheckKnockBack();
    }

    public void KnockBack(Vector2 angle, float strength, int direction)
    {
        movement.Comp?.SetVelocity(strength, angle, direction);
        movement.Comp.CanSetVelocity = false;
        isKnockBackActive = true;
        knockBackStartTime = Time.time;
    }
    private void CheckKnockBack()
    {
        if(isKnockBackActive && 
            (movement.Comp?.CurrentVelocity.y <= 0.01f)
            || Time.time >= knockBackStartTime + maxKnockbackTime)
        {
            isKnockBackActive = false;
            movement.Comp.CanSetVelocity = true;
        }
    }
    protected override void Awake()
    {
        base.Awake();
        movement = new CoreComp<Movement>(core);
    }
}
