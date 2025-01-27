using UnityEngine;
public class HitBoxData : ComponentData
{
    [field: SerializeField] public LayerMask DetectableLayers { get; private set; }
    protected override void SetComponentDependency()
    {
        ComponentDependency = typeof(WeaponHitBox);
    }
}