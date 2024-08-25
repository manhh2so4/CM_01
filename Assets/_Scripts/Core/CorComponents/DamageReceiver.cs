using UnityEngine;

public class DamageReceiver : CoreComponent, IDamageable
{
    [SerializeField] private GameObject damageParticle;
    private Stats stats;
    private ParticleManager particleManager;
    public void Damage(int amount)
    {
        Debug.Log(core.transform.parent.name + " Damaged!");
		stats.Health.Decrease(amount);
        particleManager.StartParticlesRandomRotation(damageParticle);
    }
    protected override void Awake()
    {
        base.Awake();
        stats = core.GetCoreComponent<Stats>();
        particleManager = core.GetCoreComponent<ParticleManager>();
    }
}