using UnityEngine;
using UnityEngine.EventSystems;

public class UI_EquipmentSlot : UI_ItemSlot {
    public EquipmentType slotType;
    private void OnValidate() {
        gameObject.name = "Equipment slot - " + slotType.ToString();
    }
    public override void OnPointerDown(PointerEventData eventData)
    {
        if(item == null) return;
        Inventory.Instance.UnEquipment(item.data as ItemData_Equipment);
        Inventory.Instance.AddItem(item.data as ItemData_Equipment);
        
        CleanUpSlot();
    }
}