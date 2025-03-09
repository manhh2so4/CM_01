using UnityEngine;

public interface IObjectPoolItem
{
    void SetObjectPool(ObjectPool pool);
    void Release();
}