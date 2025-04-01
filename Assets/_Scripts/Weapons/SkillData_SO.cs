using System;
using System.Collections.Generic;
using System.Linq;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "SkillData", menuName = "Data/Weapon Data", order = 0)]
public class SkillData_SO : ScriptableObject {

    [SpritePreview] public Sprite icon;
    [field: SerializeReference] public List<ComponentData> ComponentData { get; private set; }
    
    public T GetData<T>()
    {
        return ComponentData.OfType<T>().FirstOrDefault();
    }

    // public List<Type> GetAllDependencies(){
    //     return ComponentData.Select(ComponentData => ComponentData.ComponentDependency).ToList();
    // }
    public IEnumerable<ComponentData> GetAllData(){
        return ComponentData;
    }
    
    public void AddData(ComponentData data)
    {
        if(ComponentData.FirstOrDefault(t => t.GetType() == data.GetType()) != null) 
            return;
        ComponentData.Add(data);
    }

}