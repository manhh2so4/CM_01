using UnityEngine;

public abstract class Entity : MonoBehaviour {
    
    [HideInInspector] public Core core { get; private set;}
    [HideInInspector] public Movement movement;
    [HideInInspector] public KnockBackReceiver knockBackReceiver;
    [HideInInspector] public PoiseReceiver poiseReceiver;
    [HideInInspector] public CharacterStats CharStats;
    //--------------------------------------------
    [HideInInspector] public mPhysic2D mPhysic2D;
    protected virtual void Awake() {

        core = GetComponentInChildren<Core>();
        knockBackReceiver = core.GetCoreComponent<KnockBackReceiver>();
        poiseReceiver = core.GetCoreComponent<PoiseReceiver>();
        CharStats = core.GetCoreComponent<CharacterStats>();
        movement = core.GetCoreComponent<Movement>();
        if(mPhysic2D == null) mPhysic2D = GetComponent<mPhysic2D>();

    }
    
}