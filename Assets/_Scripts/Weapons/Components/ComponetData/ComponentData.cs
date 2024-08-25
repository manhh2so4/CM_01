using System;
using UnityEngine;

    [Serializable]
    public abstract class ComponentData
    {
        [SerializeField, HideInInspector] private string name;
        public Type ComponentDependency {get;protected set;}
        public ComponentData(){
            SetComponentName();
            SetComponentDependency();
        }
        protected abstract void SetComponentDependency();
        public void SetComponentName() => name = GetType().Name;
    }

    [Serializable]
    public abstract class ComponentData<T> : ComponentData where T : AttackData {
        [field: SerializeField] public T AttackData {get; private set;}
    }
