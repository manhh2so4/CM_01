using UnityEngine;

public abstract class Entity : MonoBehaviour {
    public Core core { get; private set;}
    public KnockBackReceiver knockBackReceiver;
    public PoiseReceiver poiseReceiver;
    public CharacterStats CharStats;
    //--------------------------------------------
    public BoxCollider2D mCollider;
    public mPhysic2D mRB;
    protected virtual void Awake() {
        core = GetComponentInChildren<Core>();
        knockBackReceiver = core.GetCoreComponent<KnockBackReceiver>();
        poiseReceiver = core.GetCoreComponent<PoiseReceiver>();
        CharStats = core.GetCoreComponent<CharacterStats>();

        if(mRB == null) mRB = GetComponent<mPhysic2D>();
        if(mCollider == null) mCollider = GetComponent<BoxCollider2D>();

    }
}