using UnityEngine;

public abstract class WeaponComponents : MonoBehaviour {
    protected SKill weapon;
    protected Core Core;
    protected Movement coreMove;
    
    protected virtual void Awake()
    {
        weapon = GetComponent<SKill>();
        Core = weapon.core;
    }

    public virtual void Init()
    {
        coreMove = Core.GetCoreComponent<Movement>();
        weapon.OnEnter += HandleEnter;
        weapon.OnExit += HandleExit;
        weapon.OnMidd += HandleMiddle;
        SubscribeHandlers();
    }
    protected virtual void SubscribeHandlers(){}

    protected virtual void HandleEnter() {}
    protected virtual void HandleMiddle() {}
    protected virtual void HandleExit() {}
    public virtual void Refest(){
        weapon.OnEnter -= HandleEnter;
        weapon.OnExit -= HandleExit;
        weapon.OnMidd -= HandleMiddle;
    }
    public virtual void SetData(ComponentData data){}
}

public abstract class WeaponComponents<T1> : WeaponComponents where T1 : ComponentData
{
    protected T1 data;
    public override void SetData(ComponentData _data){
        this.data = _data as T1;
    }
}