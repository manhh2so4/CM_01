using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WeaponGenerator : MonoBehaviour {
    [SerializeField] private Weapon weapon;
    private List<WeaponComponents> compAlreadyOnWeapon = new List<WeaponComponents>();
    private List<WeaponComponents> compAddedToWeapon = new List<WeaponComponents>();
    private List<Type> componetDependencies = new List<Type>();

    public void GenerateWeapon(SkillData_SO data){
        weapon.SetData(data);
        compAlreadyOnWeapon.Clear();
        compAddedToWeapon.Clear();
        componetDependencies.Clear();

        if(data == null) return;

        compAlreadyOnWeapon = GetComponents<WeaponComponents>().ToList();
        componetDependencies = data.GetAllDependencies();
        
        foreach(var dependency in componetDependencies){

            if(compAddedToWeapon.FirstOrDefault(component => component.GetType()== dependency))
                continue;

            var weaponComponent = compAlreadyOnWeapon.FirstOrDefault(component => component.GetType() == dependency);

            if(weaponComponent == null){
                if(dependency == typeof(WeaponComponents)){
                    continue;
                }
                weaponComponent = gameObject.AddComponent(dependency) as WeaponComponents;
                
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