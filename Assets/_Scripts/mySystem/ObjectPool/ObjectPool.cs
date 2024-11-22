using UnityEngine;
using System.Collections.Generic;
[System.Serializable]
public abstract class ObjectPool {
    public abstract void Release();
    public abstract void ReturnObject(Component comp);
}
[System.Serializable]
public class ObjectPool<T> : ObjectPool where T : Component
{
    private readonly T prefab;
    private readonly Queue<T> pool = new Queue<T>();

    private readonly List<IObjectPoolItem> allItems = new List<IObjectPoolItem>();

    public ObjectPool(T prefab, int startCount = 1){

        this.prefab = prefab;
        for (var i = 0; i < startCount; i++)
        {
            var obj = InstantiateNewObject();
            pool.Enqueue(obj);
        }
    }

    private T InstantiateNewObject(){

        var obj = Object.Instantiate(prefab);
        obj.name = prefab.name;

        if ( !obj.TryGetComponent<IObjectPoolItem>(out var objectPoolItem) )
        {
            Debug.LogWarning($"{obj.name} does not have a component that implements IObjectPoolItem");
            return obj;
        }
        // If object has the IObjectPool interface, set this ObjectPool as it's pool and store in list
        objectPoolItem.SetObjectPool(this, obj);
        allItems.Add(objectPoolItem);
        return obj;
    }
    public T GetObject()
    {
        // Try to get item from the queue. TryDequeue returns true if object available and false if not
        if (!pool.TryDequeue(out var obj))
        {
            // If not available, instantiate a new one and return
            obj = InstantiateNewObject();
            return obj;
        }

        // If available, return
        obj.gameObject.SetActive(true);
        return obj;
    }
    public override void ReturnObject(Component comp)
    {
        if (comp is not T compObj)
            return;
        
        compObj.gameObject.SetActive(false);
        pool.Enqueue(compObj);
    }

    public override void Release()
    {
        foreach (var item in pool)
        {
            allItems.Remove(item as IObjectPoolItem);
            Object.Destroy(item.gameObject);
        }

        foreach (var item in allItems)
        {
            item.Release();
        }
    }
}