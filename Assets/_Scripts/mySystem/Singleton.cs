using UnityEngine;
[DefaultExecutionOrder(-2)]
public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;
    public static T Instance
    {
        get
        {
            // if (_instance == null)
            // {
            //     Debug.Log(" Not Instance in scence: " + typeof(T).Name );

            //     _instance = FindObjectOfType<T>();
                
            //     if (_instance == null)
            //     {
            //         Common.LogWarning(" Not Instance in scence: " + typeof(T).Name );
            //         GameObject singletonObject = new GameObject(typeof(T).Name);
            //         _instance = singletonObject.AddComponent<T>();
            //     }
            // }
            return _instance;
        }
    }

    protected virtual void Awake()
    {
        _instance = this as T;
        
        if (_instance != null && _instance != this as T)
        {
            Destroy(gameObject);
        }
        
    }
}