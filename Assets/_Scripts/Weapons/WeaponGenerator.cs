using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WeaponGenerator : MonoBehaviour {
    [SerializeField] private SKill weapon;
    private List<WeaponComponents> compAlreadyOnWeapon = new List<WeaponComponents>();
    private List<WeaponComponents> compAddedToWeapon = new List<WeaponComponents>();
    //private List<Type> componetDependencies = new List<Type>();

    public void GenerateWeapon(SkillData_SO data){
        weapon.SetData(data);
        
        compAlreadyOnWeapon.Clear();
        compAddedToWeapon.Clear();
        //componetDependencies.Clear();

        if(data == null) return;

        compAlreadyOnWeapon = GetComponents<WeaponComponents>().ToList();
        foreach(var component in compAlreadyOnWeapon){
            component.Refest();
        }

        //componetDependencies = data.GetAllDependencies();

        foreach(ComponentData componentData in data.GetAllData()){
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