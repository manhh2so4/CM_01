using UnityEngine;

[CreateAssetMenu( menuName = "Inventory/Equipment")]
public class EquipableItemSO : InventoryItemSO {
    [SerializeField] EquipLocation typeEquip;
    public EquipLocation GetTypeEquip(){
        return typeEquip;
    }
}