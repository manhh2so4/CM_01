using Bardent.Weapons.Components;

public class PoiseDamageData : ComponentData<AttackPoiseDamage>
{
    protected override void SetComponentDependency()
    {
        ComponentDependency = typeof(WeaponPoiseDamage);
    }
}