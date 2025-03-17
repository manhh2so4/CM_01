using System;
using HStrong.Quests;

public class InventoryEvent {
    public event Action<InventoryItemSO , int> OnAddItemInv;
    public void AddItemInv(InventoryItemSO item, int number) => OnAddItemInv?.Invoke(item,number);

}