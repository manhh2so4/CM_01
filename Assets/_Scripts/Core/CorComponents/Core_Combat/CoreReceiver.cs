using UnityEngine;
using UnityEngine.UIElements;

public abstract class CoreReceiver : CoreComponent {

    protected CharacterStats characterStats;
    protected ParticleManager particleManager;
    protected CapsuleCollider2D capsuleCollider;
    protected override void Awake()
    {

        base.Awake();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        BoxCollider2D boxCollider = core.transform.parent.GetComponent<BoxCollider2D>();
        if(boxCollider != null){
            capsuleCollider.size = boxCollider.size;
            capsuleCollider.offset = boxCollider.offset;
        }
        characterStats = core.GetCoreComponent<CharacterStats>();
        particleManager = core.GetCoreComponent<ParticleManager>();

    }
    protected virtual void Start(){

        Top.Set(0,core.height,0);
        Center.Set(0,core.height/2,0);
        Bottom.Set(0,0,0);

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