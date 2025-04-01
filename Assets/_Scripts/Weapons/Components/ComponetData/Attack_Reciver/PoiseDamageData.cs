using UnityEngine;

public class PoiseDamageData : ComponentData
{
    [field: SerializeField] public float TimeEff { get; private set;}
    [field: SerializeField] public float Rate { get; private set;}
    [field: SerializeField] public Poisetype type { get; private set;}
    [field: SerializeField] public Effect_Instance prefabEff;
    protected override void SetComponentDependency()
    {
        ComponentDependency = typeof(WeaponPoiseDamage);
    }
}
public enum Poisetype{
    Stun,
    freeze,
}