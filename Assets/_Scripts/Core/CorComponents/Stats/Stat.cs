using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stat {
    
    [SerializeField] private int baseValue;
    public int CurrentValue{ 
        get{return currentValue;} 
        set{
            currentValue = value;   
            if( currentValue > GetValue() ) currentValue = GetValue();
            OnChangeValue?.Invoke();
        }
    }

    [SerializeField] int currentValue;
    public List<int> modifiers;
    public event Action OnChangeValue;
    public int GetValue()
    {
        int finalValue = baseValue; 

        foreach (int modifier in modifiers)
        {
            finalValue += modifier;
        }
        return finalValue;
    }

    public void SetDefaultValue(int _value)
    {
        baseValue = _value;
        CurrentValue += _value;
    }

    public void AddModifier(int _modifier)
    {
        modifiers.Add(_modifier);
        OnChangeValue?.Invoke();
    }

    public void RemoveModifier(int _modifier)
    {
        modifiers.Remove(_modifier);
        OnChangeValue?.Invoke();
    }
}