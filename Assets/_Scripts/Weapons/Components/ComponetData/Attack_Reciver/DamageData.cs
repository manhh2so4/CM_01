using UnityEngine;
public class DamageData : ComponentData
{
    [field: SerializeField] public int Amout { get; private set; }
    [field: SerializeField] public BaseEffect prefabHit;
    protected override void SetComponentDependency()
    {
        ComponentDependency = typeof(WeaponDamage);
    }
}