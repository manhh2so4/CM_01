using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "SkillData", menuName = "Data/Weapon Data", order = 0)]
public class SkillData_SO : ScriptableObject {
    [field: SerializeReference] public List<ComponentData> ComponentData { get; private set; }
    
    public T GetData<T>()
    {
        return ComponentData.OfType<T>().FirstOrDefault();
    }
    public List<Type> GetAllDependencies(){
        return ComponentData.Select(ComponentData => ComponentData.ComponentDependency).ToList();
    }
    
    public void AddData(ComponentData data)
    {
        if(ComponentData.FirstOrDefault(t => t.GetType() == data.GetType()) != null) 
            return;
        ComponentData.Add(data);
    }

}