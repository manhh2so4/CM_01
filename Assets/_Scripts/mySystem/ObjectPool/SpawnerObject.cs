using NaughtyAttributes;
using UnityEngine;

public class SpawnerObject : MonoBehaviour {

    public testPool prefab;
    public float ShotCooldown;
    private float lastFireTime;
    bool isReleased = false;
    private void FireProjectile()
    {
        var projectile = PoolsContainer.GetObject(prefab);
        projectile.transform.parent = this.transform;
        projectile.transform.position = transform.position;
        projectile.transform.rotation = transform.rotation;
        lastFireTime = Time.time;
    }
    [Button("Release Pool")]
    void ReleasePool()
    {
        isReleased = true;
        PoolsContainer.Release(prefab);

    }
    private void Update()
    {
        if (isReleased) return;
        if (Time.time >= lastFireTime + ShotCooldown)
        {
            FireProjectile();
        }
    }
    
}