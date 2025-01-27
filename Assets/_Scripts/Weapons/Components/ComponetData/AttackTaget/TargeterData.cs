using UnityEngine;

public class TargeterData : ComponentData
{
    [field: SerializeField] public float radius { get; private set; }
    [field: SerializeField] public LayerMask DamageableLayer { get; private set; }
    protected override void SetComponentDependency()
    {
        ComponentDependency = typeof(WeaponTargeter);
    }
}