using HStrong.Core.UI.Dragging;
using HStrong.Core.UI.Tooltips;
using UnityEngine;

public class ActionSlotUI : MonoBehaviour, IItemHolder, IDragContainer<InventoryItemSO>
{
    [SerializeField] InventoryItemIcon icon = null;
    [SerializeField] int  index = 0;
    ActionStore store;

    Equipment playerEquipment;
    private void Awake() {
        var player = GameObject.FindGameObjectWithTag("Player");
        store = player.GetComponent<ActionStore>();
        playerEquipment.equipmentUpdate += RedrawUI;
    }

    public void AddItems(InventoryItemSO item, int number)
    {
        store.AddAction(item, index, number);    
    }

    public InventoryItemSO GetItem()
    {
        return store.GetAction(index);
    }

    public int GetNumber()
    {
        return store.GetNumber(index);
    }

    public int MaxAcceptable(InventoryItemSO item)
    {
        return store.MaxAcceptable(item,index);
    }

    public void RemoveItems(int number)
    {
        store.RemoveItem(index,number);
    }

    void RedrawUI(){
        icon.SetItem(GetItem(),GetNumber());
    }
    
}
