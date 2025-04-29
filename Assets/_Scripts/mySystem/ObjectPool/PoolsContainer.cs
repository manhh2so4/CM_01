using UnityEngine;

public class PoolsContainer : Singleton<PoolsContainer> {
    public ObjectPools objectPools = new ObjectPools();
    void Start()
    {
        Box_Chat box_Chat = GetObject( this.GetPrefab<Box_Chat>(),this.transform );
        box_Chat.RemoveBoxChat();
    }

    public static T GetObject<T>(T prefab, int startCount = 1) where T : Component
    {
        return Instance.objectPools.GetObject(prefab, startCount);
    }

    public static T GetObject<T>(T prefab, Transform _parent, int startCount = 1) where T: Component
    {
        T obj = GetObject(prefab, startCount);
        obj.transform.SetParent(_parent);
        return obj;
    }

    public static T GetObject<T>(T prefab,Vector3 _position,Transform _parent = null, int startCount = 1) where T: Component
    {
        T obj = GetObject(prefab, startCount);
        obj.transform.localPosition = _position;
        obj.transform.SetParent(_parent);
        return obj;
    }
    
    public static void Release<T>(T prefab) where T : Component
    {
        Instance.objectPools.Release(prefab);
    }

}