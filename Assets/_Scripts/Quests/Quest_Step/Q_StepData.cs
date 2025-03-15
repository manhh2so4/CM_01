using System;
using UnityEditor;
using UnityEngine;
namespace HStrong.Quests{
    [Serializable]
    public abstract class Q_StepData {
        
        [SerializeField, HideInInspector] private string name;
        [field: SerializeField] public string __description__;
        public Type ComponentDependency {get;protected set;}

        public Q_StepData(){
            
            SetComponentName();
            SetComponentDependency();
        }
        protected abstract void SetComponentDependency();
        public void SetComponentName() => name = GetType().Name;
    }
}