using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreComponent : MonoBehaviour, ILogicUpdate
{
    protected Core core;
    [SerializeField] protected Vector3 Top, Bottom , Center ;
    protected virtual void Awake()
    {
        core = transform.parent.GetComponent<Core>();
        if(core == null) { Debug.LogError("There is no Core on the parent");}
        core.AddComponent(this);
    }
    public virtual void LogicUpdate() {

    }
    protected virtual void Start() {}
    protected virtual void OnEnable()
    {
        core.OnChangeData += SetData;
    }
    protected virtual void OnDisable()
    {
        core.OnChangeData -= SetData;
    }
    protected virtual void SetData(){
        Top.Set(0,core.Height,0);
        Center.Set(0,core.Height/2,0);
        Bottom.Set(0,0,0);
    }

    protected virtual Vector3 SetPosEff(BaseEffect PrefabEff){
        Effect_Sprite eff = PrefabEff as Effect_Sprite;
        if(eff == null) return Vector3.zero;   
            
        switch( eff.posEff )
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
