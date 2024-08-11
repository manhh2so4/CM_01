using UnityEngine;
public class Combat : CoreComponent, IDamageable,IKnockbackable
{
    [SerializeField] private GameObject damageParticle;
    private Movement Movement { get => movement ?? core.GetCoreComponent(ref movement); }
    private CollisionSenses CollisionSenses {
		get => collisionSenses ?? core.GetCoreComponent(ref collisionSenses);
	}
    private Stats Stats { get => stats ?? core.GetCoreComponent(ref stats); }
    private ParticleManager ParticleManager => particleManager ? particleManager : core.GetCoreComponent(ref particleManager);

    private Movement movement;
	private CollisionSenses collisionSenses;
	private Stats stats;
    private ParticleManager particleManager;

    [SerializeField] private float maxKnockbackTime = 0.2f;

    private bool isKnockbackActive;
    private float knockbackStartTime;

    public void Damage(int amount)
    {
        Debug.Log(core.transform.parent.name + " Damaged!");
		Stats?.DecreaseHealth(amount);
        ParticleManager?.StartParticlesRandomRotation(damageParticle);
    }

    public void Knockback(Vector2 angle, float strength, int direction)
    {
        Movement?.SetVelocity(strength, angle, direction);
        Movement.CanSetVelocity = false;
        isKnockbackActive = true;
        knockbackStartTime = Time.time;
    }
    private void CheckKnockback()
    {
        if(isKnockbackActive && Movement?.CurrentVelocity.y <= 0.01f && collisionSenses.isGround)
        {
            isKnockbackActive = false;
            Movement.CanSetVelocity = true;
        }
    }
}
