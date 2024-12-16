using System.Collections.Generic;
using UnityEngine;

public class WeaponTargeter : WeaponComponents<TargeterData, AttackTargeter> {
    private List<Transform> targets = new List<Transform>();
    private Movement movement;
    private bool isActive;
    protected override void HandleEnter()
    {
        base.HandleEnter();
        isActive = true;
    }
    protected override void HandleExit()
    {
        base.HandleExit();

        isActive = false;
    }
    public List<Transform> GetTargets()
    {
        return targets;
    }
    private void CheckForTargets()
        {
            var pos = transform.position + new Vector3(0,1,0);
            //var targetColliders =
                //Physics2D.OverlapCircleAll(pos, currentAttackData.radius, currentAttackData.);

            //targets = targetColliders.Select(item => item.transform).ToList();
        }
}