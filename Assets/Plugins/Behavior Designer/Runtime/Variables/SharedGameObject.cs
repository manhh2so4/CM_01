using UnityEngine;

namespace BehaviorDesigner.Runtime
{
    [System.Serializable]
    public class SharedGameObject : SharedVariable<UnityEngine.GameObject>
    {
        public static implicit operator SharedGameObject(UnityEngine.GameObject value) { return new SharedGameObject { mValue = value }; }
    }
}