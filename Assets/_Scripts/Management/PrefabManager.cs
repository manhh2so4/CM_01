using System;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class PrefabManager : Singleton<PrefabManager> {
    [SerializeField] public GameObject[] prefabs;
    readonly Dictionary<Type, Component> prefabMap = new Dictionary<Type, Component>();

    protected override void Awake(){
        base.Awake();
        foreach (var item in prefabs)
        {
            if(item.TryGetComponent(out IObjectPoolItem prefab)){
                prefabMap[ prefab.GetType() ] = item.GetComponent( prefab.GetType() );
            }
        }
    }

    public T Get<T>() where T : Component{
        if( prefabMap.ContainsKey(typeof(T)) ){
            return prefabMap[typeof(T)] as T;
        }
        Common.LogError( "PrefabManager.GetPrefab<T> , Prefab not found: " + typeof(T).ToString());
        return null;
    }
    public static T GetPrefab<T>() where T : Component{
        return Instance.Get<T>();
    }
}
public static class Extension_PrefabManager
{
    public static T GetPrefab<T>(this MonoBehaviour sender) where T : Component
    {
        return PrefabManager.Instance.Get<T>();
    }
    public static T GetPrefab<T>(this ScriptableObject sender) where T : Component
    {
        return PrefabManager.Instance.Get<T>();
    }
    
}
