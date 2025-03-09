using UnityEngine;
using System;

[Serializable]
public class SerializableCallback<T>
{
    public UnityEngine.Events.UnityEvent<T> callback;

    public bool Invoke(T parameter)
    {
        if (callback != null)
        {
            callback.Invoke(parameter);
            return true;
        }
        return false;
    }
}