using System;
using System.Collections;
using System.Collections.Generic;
using HStrong.Saving;
using UnityEngine;

public class Inventory : MonoBehaviour,ISaveable
{
    [Tooltip("Allowed size")]
    [SerializeField] int inventorySize = 16;
    InventorySlot[] slots;
    struct InventorySlot
    {
        public InventoryItemSO item;
        public int number;
    }
    public event Action inventoryUpdated;

    private void Awake()
    {
        slots = new InventorySlot[inventorySize];
        
    }


    public bool HasSpaceFor(InventoryItemSO item)
    {

        return FindSlotAdd(item) >= 0;
    }

    public int GetSize()
    {
        return slots.Length;
    }

    public bool AddToFirstEmptySlot(InventoryItemSO item, int number)
    {

        int i = FindSlotAdd(item);

        if (i < 0)
        {
            return false;
        }

        slots[i].item = item;
        slots[i].number += number;

        inventoryUpdated?.Invoke();
        this.GameEvents().inventoryEvent.AddItemInv(item,number);
        return true;
    }

    public bool HasItem(InventoryItemSO item)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if ( object.ReferenceEquals( slots[i].item, item) )
            {
                return true;
            }
        }
        return false;
    }
    public bool HasItem(InventoryItemSO item, int number){

        int count = 0;
        for (int i = 0; i < slots.Length; i++){
            if ( object.ReferenceEquals( slots[i].item, item) ){
                count += GetNumberInSlot(i);
            }
        }
        return count >= number;
    }

    public int GetNumberItem(InventoryItemSO item){
        int count = 0;
        for (int i = 0; i < slots.Length; i++){
            if ( object.ReferenceEquals( slots[i].item, item) ){
                count += GetNumberInSlot(i);
            }
        }
        return count;
    }


    public InventoryItemSO GetItemInSlot(int slot)
    {
        return slots[slot].item;
    }
    public int GetNumberInSlot(int slot)
    {
        return slots[slot].number;
    }

    public bool UseItemAction(ActionItem item){
        int slot = FindSlot(item);
        if(slot >= 0){
            if( item.IsConsumable() ){
                RemoveFromSlot(slot,1);
            }
            item.Use( PlayerManager.GetBuffStat());
            return true;
        }
        return false;
    }

    public bool RemoveItem(InventoryItemSO item, int number = 1){
        int slot = FindSlot(item);
        if(slot >= 0) RemoveFromSlot(slot, number);
        else return false;
        
        return true;
    }
    public void RemoveFromSlot(int slot, int number)
    {
        slots[slot].number -= number;
        if (slots[slot].number <= 0)
        {
            slots[slot].number = 0;
            slots[slot].item = null;
        }

        inventoryUpdated?.Invoke();
    }

    public bool AddItemToSlot(int slot, InventoryItemSO item, int number)
    {
        if (slots[slot].item != null)
        {
            return AddToFirstEmptySlot(item, number);
        }

        var i = FindStack(item);

        if (i >= 0)
        {
            slot = i;
        }

        slots[slot].item = item;
        slots[slot].number += number;

        inventoryUpdated?.Invoke();
        return true;
    }



    private int FindSlotAdd(InventoryItemSO item)
    {
        
        int i = FindStack(item);
        if (i < 0)
        {
            i = FindEmptySlot();
        }
        return i;
    }

    private int FindEmptySlot()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == null)
            {
                return i;
            }
        }
        return -1;
    }
    private int FindStack(InventoryItemSO item)
    {
            if ( item.IsStackable() == false )
            {
                return -1;
            }

            for (int i = 0; i < slots.Length; i++)
            {
                if ( object.ReferenceEquals(slots[i].item, item))
                {
                    return i;
                }
            }
            return -1;
    }
    private int FindSlot(InventoryItemSO item){

        for (int i = 0; i < slots.Length; i++)
            {
                if ( object.ReferenceEquals(slots[i].item, item))
                {
                    return i;
            }
        }
        return -1;
    }
#region Save_File
    [System.Serializable]
    private struct InventorySlotRecord
    {
        public string itemID;
        public int number;
    }
    public object CaptureState()
    {
        var slotStrings = new InventorySlotRecord[inventorySize];
        for (int i = 0; i < inventorySize; i++)
        {
            if (slots[i].item != null)
            {
                slotStrings[i].itemID = slots[i].item.GetItemID();
                slotStrings[i].number = slots[i].number;
            }
        }
        return slotStrings;
    }

    public void RestoreState(object state)
    {
        var slotStrings = (InventorySlotRecord[])state;
        for (int i = 0; i < inventorySize; i++)
        {
            slots[i].item = InventoryItemSO.GetFromID(slotStrings[i].itemID);
            slots[i].number = slotStrings[i].number;
        }

        inventoryUpdated?.Invoke();
    }
    #endregion
}
