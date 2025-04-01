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
        
        
        characterStats = core.GetCoreComponent<CharacterStats>();
        particleManager = core.GetCoreComponent<ParticleManager>();

    }
    protected virtual void Start(){

        Top.Set(0,core.height,0);
        Center.Set(0,core.height/2,0);
        Bottom.Set(0,0,0);
        BoxCollider2D boxCollider = core.transform.parent.GetComponent<BoxCollider2D>();
        if(boxCollider != null){
            capsuleCollider.size = new Vector2(boxCollider.size.x, core.height);
            capsuleCollider.offset = new Vector2(boxCollider.offset.x, core.height/2);
        }

    }
    protected virtual Vector3 SetPosEff(Effect_Instance PrefabEff){       
        switch( PrefabEff.posEff )
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