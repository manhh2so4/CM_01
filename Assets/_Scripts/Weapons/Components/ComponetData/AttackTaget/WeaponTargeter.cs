using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WeaponTargeter : WeaponComponents<TargeterData> {
    [SerializeField] private List<Transform> targets = new List<Transform>();
    public Action<List<Transform>> TargeterTrigger;

    protected override void HandleEnter()
    {
        base.HandleEnter();
        CheckForTargets();
        
    }
    
    protected override void HandleMiddle(){
        TargeterTrigger?.Invoke(targets);
    }

    protected override void HandleExit()
    {
        base.HandleExit();
        targets.Clear();
    }

    private void CheckForTargets()
    {
        Vector3 pos = transform.position + new Vector3(0,1,0);
        var targetColliders = Physics2D.OverlapCircleAll(pos, data.radius, data.DamageableLayer);
        foreach (var target in targetColliders){
            if(target.CompareTag(transform.tag)) continue;
            targets.Add(target.transform);
        }
    }
    private void OnDrawGizmosSelected()
    {
        if (data == null)
            return;
        Gizmos.DrawWireSphere(transform.position + new Vector3(0,1,0), data.radius);
    }    
}