using UnityEngine;

public class WeaponProjectileSpawner : WeaponComponents<ProjectileSpawnerData, AttackProjectileSpawner> {
    private Movement movement;
    private readonly ObjectPools objectPools = new ObjectPools();
    private IProjectileSpawnerStrategy projectileSpawnerStrategy;
    public void SetProjectileSpawnerStrategy(IProjectileSpawnerStrategy newStrategy)
    {
        projectileSpawnerStrategy = newStrategy;
    }
    private void HandleAttackAction()
    {
        
        projectileSpawnerStrategy.ExecuteSpawnStrategy(currenAttackData.SpawnInfos, transform.position,
            movement.facingDirection, objectPools);
        
    }


    protected override void SubscribeHandlers()
    {
        base.SubscribeHandlers();
        movement = Core.GetCoreComponent<Movement>();
        
    }
    protected override void OnDisable()
    {
        base.OnDisable();   

    }

}