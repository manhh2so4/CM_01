using UnityEngine;
using UnityEngine.Events;

namespace SOArchitecture
{
    [System.Serializable]
    public class IntEvent : UnityEvent<int> { }

    [CreateAssetMenu(
        fileName = "IntVariable.asset",
        menuName = SOArchitecture_Utility.VARIABLE_SUBMENU + "int",
        order = SOArchitecture_Utility.ASSET_MENU_ORDER_COLLECTIONS + 4)]
    public class IntVariable : BaseVariable<int, IntEvent>
    {
        protected override bool AreValuesEqual(int a, int b)
        {
            return false;
        }
    } 
}