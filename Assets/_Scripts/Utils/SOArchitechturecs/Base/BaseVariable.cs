using UnityEngine;
using UnityEngine.Events;

public abstract class BaseVariable : GameEventBase
{
    public abstract System.Type Type { get; }
    //public abstract System.Type ReferenceType { get; }
}
public abstract class BaseVariable<T> : BaseVariable {
    private T _oldValue;
    [SerializeField] protected T _value = default(T);
    [SerializeField] bool _useDefaultValue = false;
    [SerializeField] protected T _defaultValue = default(T);
    [SerializeField] bool enableDebug = false;
    public override System.Type Type { get { return typeof(T); } }

    public virtual T Value
    {
        get
        {
            return _value;
        }
        set
        {
             if(enableDebug) Debug.Log($"Variable {name} was Modified ",this);
            _value = SetValue(value);
        }
    }

    public virtual T SetValue(BaseVariable<T> value)
    {
        return SetValue(value.Value);
    }
    public virtual T SetValue(T newValue)
    {
        _value = newValue;
        if (!AreValuesEqual(newValue, _oldValue)) Raise();
        _oldValue = _value;

        return newValue;
    }
    protected virtual bool AreValuesEqual(T a, T b)
    {
        if (a != null) return a.Equals(b);

        return b == null;
    }
    public void OnEnable()
    {
        _oldValue = _value;
        if(_useDefaultValue)
            ResetToDefaultValue();
    }

    private void ResetToDefaultValue()
    {
        Value = _defaultValue;
    }
}
public abstract class BaseVariable<T, TEvent> : BaseVariable<T> where TEvent : UnityEvent<T>
{
    [SerializeField] TEvent _event = default(TEvent);

    protected override void Raise()
    {
        base.Raise();
       _event.Invoke(Value);
    }
    public void AddListener(UnityAction<T> callback)
    {
        _event.AddListener(callback);
    }
    public void RemoveListener(UnityAction<T> callback)
    {
        _event.RemoveListener(callback);
    }

}

