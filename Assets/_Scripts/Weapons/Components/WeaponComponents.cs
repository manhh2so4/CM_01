using UnityEngine;

public abstract class WeaponComponents : MonoBehaviour {
    protected Weapon weapon;
    protected Core Core => weapon.core;
    protected Movement coreMove;
    

    protected bool isAttackActive;
    protected virtual void Awake()
    {
        weapon = GetComponent<Weapon>();
    }

    public virtual void Init()
    {
        coreMove = Core.GetCoreComponent<Movement>();
        SubscribeHandlers();
        weapon.OnEnter += HandleEnter;
        weapon.OnExit += HandleExit;
        weapon.OnMidd += HandleMiddle;
        
    }
    protected virtual void SubscribeHandlers()
    {

    }

    protected virtual void HandleEnter()
    {
        isAttackActive = true;
    }
    protected virtual void HandleMiddle()
    {
       
    }
    protected virtual void HandleExit()
    {
        isAttackActive = false;
    }
    protected virtual void OnDisable()
    {
        weapon.OnEnter -= HandleEnter;
        weapon.OnExit -= HandleExit;
        weapon.OnMidd -= HandleMiddle;
    }

}
public abstract class WeaponComponents<T1> : WeaponComponents where T1 : ComponentData
{
    protected T1 data;

    // protected override void HandleEnter()
    // {
    //     base.HandleEnter();
    // }
    protected override void SubscribeHandlers()
    {
        base.SubscribeHandlers();
        data = weapon.Data.GetData<T1>();
    }
}