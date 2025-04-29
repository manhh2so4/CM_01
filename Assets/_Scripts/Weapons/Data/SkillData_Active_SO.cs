using System;
using System.Collections.Generic;
using System.Linq;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "SkillData", menuName = "Data/Skill Active Data", order = 0)]
public class SkillData_Active_SO : SkillDataBase {
    public int Range ;
    public int[] consumeMana ;
    public float[] cooldown ;
    void Awake()
    {
        isActiveSkill = true;
    }
    [field : SerializeReference] public List<ComponentData> ComponentData { get; private set; }
    public T GetData<T>()
    {
        return ComponentData.OfType<T>().FirstOrDefault();
    }

#if UNITY_EDITOR 
    public IEnumerable<ComponentData> GetAllData(){
        return ComponentData;
    }
    
    public void AddData(ComponentData data)
    {
        if(ComponentData.FirstOrDefault(t => t.GetType() == data.GetType()) != null) 
            return;
        ComponentData.Add(data);
    }
#endif
}