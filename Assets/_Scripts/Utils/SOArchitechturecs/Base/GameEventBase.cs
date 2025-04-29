using System;
using UnityEngine;
public abstract class GameEventBase : ScriptableObject {
    public event Action OnChange;
    protected virtual void Raise(){
        OnChange?.Invoke();
    }
}
