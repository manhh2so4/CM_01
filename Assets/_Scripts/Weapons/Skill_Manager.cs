using UnityEngine;

public class Skill_Manager : MonoBehaviour {
    Equipment playerEquipment;
    public WeaponGenerator Skill_1;
    public WeaponGenerator Skill_2;
    public WeaponGenerator Skill_3;
    public WeaponSkill[] weaponSkills;
    private void Awake() {
        playerEquipment = GetComponent<Equipment>();
        Skill_1 = transform.Find("Skill_1").GetComponent<WeaponGenerator>();
        Skill_2 = transform.Find("Skill_2").GetComponent<WeaponGenerator>();
        Skill_3 = transform.Find("Skill_3").GetComponent<WeaponGenerator>();
    }
    
    void UpdateSkillWeapon(WeaponGenerator Skill_slot,int index)
    {
        WeaponItemSO weapon = playerEquipment.GetItemInSlot(EquipLocation.Vukhi) as WeaponItemSO;
        
        if(weapon != null) {
            for (int i = 0; i < weaponSkills.Length; i++)
            {
                if(weapon.GetWeaponType() == weaponSkills[i].weaponType)
                {
                    Skill_slot.GenerateWeapon(weaponSkills[i].skillData[index]);
                }
            }
        }else{
            Skill_slot.GenerateWeapon(null);
        }
    }

    void UpdateTypeEquip(EquipLocation typeEquip)
    {
        switch (typeEquip)
        {
            case EquipLocation.Vukhi:
                UpdateSkillWeapon(Skill_1,0);
                UpdateSkillWeapon(Skill_2,2);
                UpdateSkillWeapon(Skill_3,4);
                break;
            default:
                break;
        }
    }
    private void OnEnable() {
		playerEquipment.OnTypeEquipUpdate += UpdateTypeEquip;
	}
	private void OnDisable() {
		playerEquipment.OnTypeEquipUpdate -= UpdateTypeEquip;
	}


}
[System.Serializable]
public struct WeaponSkill
{   
    public WeaponType weaponType;
    public SkillData_SO[] skillData;
}