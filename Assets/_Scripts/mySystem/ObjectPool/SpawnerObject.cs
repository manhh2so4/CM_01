using UnityEngine;

public class SpawnerObject : MonoBehaviour {

    [SerializeField] private ObjectPools objectPools = new ObjectPools();
    public testPool prefab;
    public float ShotCooldown;
    private float lastFireTime;
    private void FireProjectile()
    {
        var projectile = objectPools.GetPool(prefab).GetObject();     
        projectile.transform.position = transform.position;
        projectile.transform.rotation = transform.rotation;
        lastFireTime = Time.time;
    }
    private void Update()
    {
        if (Time.time >= lastFireTime + ShotCooldown)
        {
            FireProjectile();
        }
    }
    
}