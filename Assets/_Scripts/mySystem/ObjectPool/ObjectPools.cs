using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class ObjectPools {
    private readonly Dictionary< Component, ObjectPool> pools = new Dictionary<Component, ObjectPool>();
    
    public ObjectPool<T> GetPool<T>(T prefab, int startCount = 1) where T : Component
    {
        if (!pools.ContainsKey(prefab))
        {
            pools[prefab] = new ObjectPool<T>(prefab, startCount);
        }

        return (ObjectPool<T>)pools[prefab];
    }

    public T GetObject<T>(T prefab, int startCount = 1) where T: Component
    {
        return GetPool(prefab, startCount).GetObject();
    }
    public void ReturnObject<T>(T obj) where T : Component
    {
        var objPool = GetPool(obj);
        objPool.ReturnObject(obj);
    }
    public void Release()
    {
        foreach (var pool in pools)
        {
            pool.Value.Release();
        }
    }
}