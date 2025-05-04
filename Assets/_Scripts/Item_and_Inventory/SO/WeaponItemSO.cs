using UnityEngine;

[CreateAssetMenu( menuName = "Inventory/Weapon")]
public class WeaponItemSO : EquipableItemSO {
    [SerializeField] WeaponType weaponType;

    public WeaponType GetWeaponType(){
        return weaponType;
    } 
    public void SetWeaponType(WeaponType type){
        this.weaponType = type;
    }

}

public enum WeaponType{
    None = 0,
    Kiem  = 1,
    Tieu  = 2,
    Kunai = 3,
    Cung = 4,
    Dao = 5,
    Quat = 6,
}