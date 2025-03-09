using UnityEngine;

public class PoolsContainer : MonoBehaviour {
    public ObjectPools objectPools = new ObjectPools();
    public static PoolsContainer Instance { get; private set; }
    private void Awake() {
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
    public static void Release<T>(T prefab) where T : Component
    {
        Instance.objectPools.Release(prefab);
    }
}