using UnityEngine;
public class KnockBackReceiver : CoreComponent,IKnockBackable
{
    private CoreComp<Movement> movement;
    private CoreComp<CollisionSenses> collisionSenses;
    [SerializeField] private float maxKnockbackTime = 0.2f;

    private bool isKnockbackActive;
    private float knockbackStartTime;

    

    public void KnockBack(Vector2 angle, float strength, int direction)
    {
        movement.Comp?.SetVelocity(strength, angle, direction);
        movement.Comp.CanSetVelocity = false;
        isKnockbackActive = true;
        knockbackStartTime = Time.time;
    }
    private void CheckKnockBack()
    {
        if(isKnockbackActive && movement.Comp?.CurrentVelocity.y <= 0.01f && collisionSenses.Comp.isGround)
        {
            isKnockbackActive = false;
            movement.Comp.CanSetVelocity = true;
        }
    }
    protected override void Awake()
    {
        base.Awake();
        movement = new CoreComp<Movement>(core);
        collisionSenses = new CoreComp<CollisionSenses>(core);
    }
}
