using UnityEngine;

public class DamageReceiver : CoreComponent, IDamageable
{
    [SerializeField] private GameObject damageParticle;
    private Stats stats;
    private ParticleManager particleManager;
    private CapsuleCollider2D mCapsul;
    private CapsuleCollider2D mCapsulCore;
    public void Damage(int amount)
    {
        Debug.Log(core.transform.parent.name + " Damaged! : " + amount);
		stats.Health.Decrease(amount);
        particleManager?.StartParticlesRandomRotation(damageParticle,this.transform.position);
    }
    protected override void Awake()
    {
        base.Awake();
        stats = core.GetCoreComponent<Stats>();
        mCapsul = GetComponent<CapsuleCollider2D>();
        mCapsulCore = transform.parent.parent.GetComponent<CapsuleCollider2D>();
        particleManager = core.GetCoreComponent<ParticleManager>();
    }
    private void Start() {
        mCapsul.size = mCapsulCore.size;
        mCapsul.offset = mCapsulCore.offset;
        mCapsul.direction = mCapsulCore.direction;
    }
}