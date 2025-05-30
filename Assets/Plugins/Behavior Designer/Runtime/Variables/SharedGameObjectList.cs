using UnityEngine;
using System.Collections.Generic;

namespace BehaviorDesigner.Runtime
{
    [System.Serializable]
    public class SharedGameObjectList : SharedVariable<List<UnityEngine.GameObject>>
    {
        public SharedGameObjectList()
        {
            mValue = new List<UnityEngine.GameObject>();
        }

        public static implicit operator SharedGameObjectList(List<UnityEngine.GameObject> value) { return new SharedGameObjectList { mValue = value }; }
    }
}