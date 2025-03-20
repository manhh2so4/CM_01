using UnityEngine;

public class PoolsContainer : MonoBehaviour {
    public ObjectPools objectPools = new ObjectPools();
    public static PoolsContainer Instance { get; private set; }
    private void Awake(){

        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public static T GetObject<T>(T prefab,int startCount = 1) where T : Component
    {
        return Instance.objectPools.GetObject(prefab, startCount);
    }
    public static T GetObject<T>(T prefab,Vector3 _position,Transform _parent = null, int startCount = 1) where T: Component
    {
        T obj = GetObject(prefab, startCount);
        obj.transform.position = _position;
        obj.transform.SetParent(_parent);
        return obj;
    }
    
    public static void Release<T>(T prefab) where T : Component
    {
        Instance.objectPools.Release(prefab);
    }
}