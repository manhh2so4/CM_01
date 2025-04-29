using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WeaponGenerator : MonoBehaviour {
    [SerializeField] private SKill weapon;
    private List<WeaponComponents> compAlreadyOnWeapon = new List<WeaponComponents>();
    private List<WeaponComponents> compAddedToWeapon = new List<WeaponComponents>();
    //private List<Type> componetDependencies = new List<Type>();

    public void GenerateWeapon(SkillData data){
        weapon.SetData(data);
        
        compAlreadyOnWeapon.Clear();
        compAddedToWeapon.Clear();
        //componetDependencies.Clear();

        if(data == null) return;

        compAlreadyOnWeapon = GetComponents<WeaponComponents>().ToList();
        foreach(var component in compAlreadyOnWeapon){
            component.Refest();
        }

        SkillData_Active_SO dataSO = data.dataSO as SkillData_Active_SO;
       
        foreach(ComponentData componentData in dataSO.GetAllData()){
            var dependency = componentData.ComponentDependency;
            WeaponComponents weaponComponent = compAlreadyOnWeapon.FirstOrDefault(component => component.GetType() == dependency);

            if(weaponComponent == null){
                if(dependency == typeof(WeaponComponents)){
                    continue;
                }
                weaponComponent = gameObject.AddComponent(dependency) as WeaponComponents;
                weaponComponent.SetData(componentData);
            }
            compAddedToWeapon.Add(weaponComponent);
        }


        var componentsToRemove = compAlreadyOnWeapon.Except(compAddedToWeapon);


        foreach (var wpComp in componentsToRemove)
        {
            Destroy(wpComp);
        }

        foreach (var component in compAddedToWeapon)
        {
            component.Init();
        }

    }
}