using NaughtyAttributes;
using UnityEngine;

public class Skill_Manager : MonoBehaviour {

    public WeaponGenerator Skill_1;
    public WeaponGenerator Skill_2;
    public WeaponGenerator Skill_3;
    public WeaponSkill[] weaponSkills;
    private void Awake() {
        
        Skill_1 = transform.Find("Skill_1").GetComponent<WeaponGenerator>();
        Skill_2 = transform.Find("Skill_2").GetComponent<WeaponGenerator>();
        Skill_3 = transform.Find("Skill_3").GetComponent<WeaponGenerator>();

    }

    
    // void UpdateSkillWeapon(WeaponGenerator Skill_slot,int index, WeaponItemSO weapon)
    // {
    //     if(Skill_slot.gameObject.activeSelf == false) return;

        
        
    //     if(weapon != null) {
    //         for (int i = 0; i < weaponSkills.Length; i++)
    //         {
    //             if(weapon.GetWeaponType() == weaponSkills[i].weaponType)
    //             {
    //                 Skill_slot.GenerateWeapon(weaponSkills[i].ActiveSkills[index]);
    //             }
    //         }
    //     }else{
    //         Skill_slot.GenerateWeapon(null);
    //     }
    // }

    // public void UpdateTypeEquip( WeaponItemSO weapon)
    // {

    //     UpdateSkillWeapon(Skill_1,0,weapon);
    //     UpdateSkillWeapon(Skill_2,2,weapon);
    //     UpdateSkillWeapon(Skill_3,4,weapon);
    // }

}
