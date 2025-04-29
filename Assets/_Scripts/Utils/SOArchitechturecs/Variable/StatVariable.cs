using System;
using UnityEngine;
using UnityEngine.Events;

namespace SOArchitecture
{
    [Serializable]
    public class StatEvent : UnityEvent<StatValue> {}

    [CreateAssetMenu(
        fileName = "StatVariable.asset",
        menuName = SOArchitecture_Utility.VARIABLE_SUBMENU + "Stat",
        order = SOArchitecture_Utility.ASSET_MENU_ORDER_COLLECTIONS + 3)]
    public class StatVariable : BaseVariable<StatValue, StatEvent>
    {
        protected override bool AreValuesEqual(StatValue a, StatValue b)
        {
            return false;
        }
    } 
}

[System.Serializable]
public struct StatValue
{
    public int currentValue;
    public int MaxValue;
    public StatValue(int _currentValue, int _MaxValue)
    {
        currentValue = _currentValue;
        MaxValue = _MaxValue;
    }
}
