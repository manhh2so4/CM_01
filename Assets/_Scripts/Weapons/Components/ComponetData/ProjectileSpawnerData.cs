public class ProjectileSpawnerData : ComponentData<AttackProjectileSpawner>
    {
        protected override void SetComponentDependency()
        {
            ComponentDependency = typeof(WeaponProjectileSpawner);
        }
    }