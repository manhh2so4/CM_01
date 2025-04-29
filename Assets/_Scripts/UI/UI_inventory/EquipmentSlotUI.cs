
using HStrong.Core.UI.Dragging;
using HStrong.Core.UI.Tooltips;
using UnityEngine;

public class EquipmentSlotUI : MonoBehaviour, IItemHolder, IDragContainer<InventoryItemSO>
{
    [SerializeField] InventoryItemIcon icon = null;
    [SerializeField] EquipType  typeEquip = EquipType.Vukhi;
    EquipableItemSO item = null;
    Equipment playerEquipment;

    private void Start() {
        playerEquipment = PlayerManager.GetEquipment();
        playerEquipment.OnEquipmentUpdate += RedrawUI;
        RedrawUI();
    }

    public void AddItems(InventoryItemSO item, int number)
    {
        this.item = item as EquipableItemSO;
        icon.SetItem(item);
        playerEquipment.AddItem(typeEquip,this.item);
    }

    public InventoryItemSO GetItem()
    {
        return playerEquipment.GetItemInSlot(typeEquip);
    }

    public int GetNumber()
    {
        if(GetItem() != null){
            return 1;
        }else return 0;
    }

    public int MaxAcceptable(InventoryItemSO item)
    {
        EquipableItemSO equipableItem = item as EquipableItemSO;
        if(equipableItem == null) return 0;
        if(equipableItem.GetTypeEquip() != typeEquip) return 0;
        if(GetItem() != null) return 0;
        return 1;
    }

    public void RemoveItems(int number)
    {
        playerEquipment.RemoveItem(typeEquip);
    }
    void RedrawUI(){
        icon.SetItem(playerEquipment.GetItemInSlot(typeEquip));
    }
}