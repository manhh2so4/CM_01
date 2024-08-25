using UnityEngine;

public abstract class WeaponComponents : MonoBehaviour {
    protected Weapon weapon;
    protected Core Core => weapon.core;

    protected bool isAttackActive;
    protected virtual void Awake()
    {
        weapon = GetComponent<Weapon>();
    }

    public virtual void Init()
    {
        SubscribeHandlers();
    }
    protected virtual void SubscribeHandlers()
    {
        weapon.OnEnter += HandleEnter;
        weapon.OnExit += HandleExit;
    }

    protected virtual void HandleEnter()
    {
        isAttackActive = true;
    }
    protected virtual void HandleExit()
    {
        isAttackActive = false;
    }
    protected virtual void OnDisable()
    {
        Debug.Log("Script OnDisable name: " + this.GetType().Name);
        weapon.OnEnter -= HandleEnter;
        weapon.OnExit -= HandleExit;
    }

}
public abstract class WeaponComponents<T1,T2> : WeaponComponents where T1 : ComponentData<T2> where T2 : AttackData
{
    protected T1 data;
    protected T2 currenAttackData;
    protected override void HandleEnter()
    {
        base.HandleEnter();
        currenAttackData = data.AttackData;
    }
    protected override void SubscribeHandlers()
    {
        base.SubscribeHandlers();
        data = weapon.Data.GetData<T1>();
    }
}