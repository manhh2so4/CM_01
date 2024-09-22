using UnityEngine;

public abstract class CoreReceiver : CoreComponent {
    [SerializeField] protected GameObject PrefabEff;
    protected Stats stats;
    protected CharacterStats characterStats;
    protected ParticleManager particleManager;
    private CapsuleCollider2D mCapsul;
    private CapsuleCollider2D mCapsulCore;
    protected override void Awake()
    {
        base.Awake();
        stats = core.GetCoreComponent<Stats>();
        characterStats = core.GetCoreComponent<CharacterStats>();
        mCapsul = GetComponent<CapsuleCollider2D>();
        mCapsulCore = transform.parent.parent.GetComponent<CapsuleCollider2D>();
        particleManager = core.GetCoreComponent<ParticleManager>();
    }
    protected virtual void Start() {
        mCapsul.size = mCapsulCore.size;
        mCapsul.offset = mCapsulCore.offset;
        mCapsul.direction = mCapsulCore.direction; 

        Top.Set(0,mCapsul.size.y,0);
        Center.Set(0,mCapsul.size.y/2,0);
        Bottom.Set(0,0,0);     
    }
    protected virtual void SetPosEff(){       
        switch (PrefabEff.GetComponent<Effect_Instance>().posEff)
        {
            case PosEff.Head:
            Location = Top;

            break;          
            case PosEff.Body:
            Location = Center;

            break;
            case PosEff.Foot:
            Location = Bottom;
            break;
        }
    }
}