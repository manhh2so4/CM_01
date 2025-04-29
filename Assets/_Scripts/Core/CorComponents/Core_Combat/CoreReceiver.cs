using UnityEngine;
using UnityEngine.UIElements;

public abstract class CoreReceiver : CoreComponent {

    protected CharacterStats characterStats;
    protected PaintEffect particleManager;
    CapsuleCollider2D capsuleCollider;
    protected override void Awake()
    {

        base.Awake();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        characterStats = core.GetCoreComponent<CharacterStats>();
        particleManager = core.GetCoreComponent<PaintEffect>();

    }
 

    protected override void SetData(){
        base.SetData();
        BoxCollider2D boxCollider = core.transform.parent.GetComponent<BoxCollider2D>();
        if(boxCollider != null){
            capsuleCollider.size = new Vector2(boxCollider.size.x, core.Height);
            capsuleCollider.offset = new Vector2(boxCollider.offset.x, core.Height/2);
        }
    }
    
}