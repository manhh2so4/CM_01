using System;
using UnityEngine;

public class testPool : MonoBehaviour, IObjectPoolItem
{
    private ObjectPool objectPool;
    private void OnTriggerEnter2D(Collider2D other) {
        DestroyProjectile();
    }

    private void DestroyProjectile()
    {
        ReturnItemToPool();
    }
    private void ReturnItemToPool()
    {
        // If pool reference is set, return to pool
        if (objectPool != null)
        {
            objectPool.ReturnObject(this);
        }
        // Otherwise, destroy
        else
        {
            Destroy(gameObject);
        }
    }
    public void Release()
    {
        objectPool = null;
    }
    public void SetObjectPool(ObjectPool pool)
    {
        objectPool = pool;
    }
}