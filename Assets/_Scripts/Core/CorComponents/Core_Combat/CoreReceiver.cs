using UnityEngine;

public abstract class CoreReceiver : CoreComponent {

    protected CharacterStats characterStats;
    protected ParticleManager particleManager;
    private CapsuleCollider2D mCapsul;

    protected override void Awake()
    {

        base.Awake();
        characterStats = core.GetCoreComponent<CharacterStats>();
        mCapsul = GetComponent<CapsuleCollider2D>();
        particleManager = core.GetCoreComponent<ParticleManager>();

    }
    protected virtual void Start(){

        Top.Set(0,mCapsul.size.y,0);
        Center.Set(0,mCapsul.size.y/2,0);
        Bottom.Set(0,0,0);

    }
    public CapsuleCollider2D GetColider(){
        return mCapsul;
    }
    protected virtual Vector3 SetPosEff(GameObject PrefabEff){       
        switch( PrefabEff.GetComponent<Effect_Instance>().posEff )
        {
            case PosEff.Head:
            return  Top;

            case PosEff.Body:
            return Center;

            case PosEff.Foot:
            return Bottom;

            default:
            return Vector3.zero;
        }
    }
}