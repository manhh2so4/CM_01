using UnityEngine;

public abstract class WeaponComponents : MonoBehaviour {
    protected Weapon weapon;
    protected Core Core => weapon.core;

    protected bool isAttackActive;
    protected virtual void Awake()
    {
        weapon = GetComponent<Weapon>();
    }
    protected virtual void Start(){}


    protected virtual void HandleEnter()
    {
        isAttackActive = true;
    }
    protected virtual void HandleExit()
    {
        isAttackActive = false;
    }


    protected virtual void OnEnable()
    {
        weapon.OnEnter += HandleEnter;
        weapon.OnExit += HandleExit;
    }
    protected virtual void OnDisable()
    {
        weapon.OnEnter -= HandleEnter;
        weapon.OnExit -= HandleExit;
    }

}