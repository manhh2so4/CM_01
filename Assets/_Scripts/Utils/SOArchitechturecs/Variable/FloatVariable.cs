using System;
using UnityEngine;
using UnityEngine.Events;

namespace SOArchitecture
{
    [Serializable]
    public class FloatEvent : UnityEvent<float> {}

    [CreateAssetMenu(
        fileName = "FloatVariable.asset",
        menuName = SOArchitecture_Utility.VARIABLE_SUBMENU + "float",
        order = SOArchitecture_Utility.ASSET_MENU_ORDER_COLLECTIONS + 3)]
    public class FloatVariable : BaseVariable<float, FloatEvent>
    {
        protected override bool AreValuesEqual(float a, float b)
        {
            return Mathf.Abs(a - b) < Mathf.Epsilon;
        }
    } 
}