using System;
using UnityEngine;
public class Weapon : MonoBehaviour {
    public event Action OnEnter;
    public event Action OnExit;
    public GameObject WeaponSpriteGameObject { get; private set; }

    public Core core{ get; private set;}

    public void SetCore(Core core)
    {
        this.core = core;
    }

    public void Enter(){
        print($"{transform.name} enter");
        OnEnter?.Invoke();
    }

    private void Exit()
        {            
            OnExit?.Invoke();
        }

    private void Awake() {
        WeaponSpriteGameObject = transform.Find("WeaponSprite").gameObject;
    }
}