using UnityEngine;

public class KnockBackData : ComponentData
{
    [field: SerializeField] public Vector2 Angle { get; private set; }       
    [field: SerializeField] public float Strength { get; private set; }
    protected override void SetComponentDependency()
    {
        ComponentDependency = typeof(WeaponKnockBack);
    }
}