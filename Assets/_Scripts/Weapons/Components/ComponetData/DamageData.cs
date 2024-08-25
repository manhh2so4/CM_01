public class DamageData : ComponentData<AttackDame>{
    protected override void SetComponentDependency()
    {
        ComponentDependency = typeof(WeaponDamage);
    }
}