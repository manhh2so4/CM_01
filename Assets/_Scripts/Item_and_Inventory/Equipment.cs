using System;
using System.Collections.Generic;
using HStrong.Saving;
using UnityEngine;

public class Equipment : MonoBehaviour//ISaveable
{
    Dictionary<EquipLocation,EquipableItemSO> equippedItems = new Dictionary<EquipLocation,EquipableItemSO>();
    public event Action equipmentUpdate;

    public EquipableItemSO GetItemInSlot(EquipLocation typeEquip){
        if(!equippedItems.ContainsKey(typeEquip)){
            return null;
        }
        return equippedItems[typeEquip];
    }
    public void AddItem(EquipLocation typeEquip, EquipableItemSO item){
        Debug.Assert(item.GetTypeEquip() == typeEquip);
        equippedItems[typeEquip] = item;
        equipmentUpdate?.Invoke();
    }
    public void RemoveItem(EquipLocation typeEquip){
        equippedItems.Remove(typeEquip);
        equipmentUpdate?.Invoke();
    }


    public object CaptureState()
    {
        throw new System.NotImplementedException();
    }

    public void RestoreState(object state)
    {
        throw new System.NotImplementedException();
    }
}