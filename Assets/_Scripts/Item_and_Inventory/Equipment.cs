using System;
using System.Collections.Generic;
using HStrong.Saving;
using UnityEngine;

public class Equipment : MonoBehaviour,ISaveable
{
    Dictionary<EquipType,EquipableItemSO> equippedItems = new Dictionary<EquipType,EquipableItemSO>();
    public event Action OnEquipmentUpdate;
    public event Action<EquipType> OnTypeEquipUpdate;
    [SerializeField] CharacterStats characterStats;
    
    private void Start() {
        Core core = GetComponentInChildren<Core>();
        characterStats = core.GetCoreComponent<CharacterStats>();
    }
    public EquipableItemSO GetItemInSlot(EquipType typeEquip){
        if(!equippedItems.ContainsKey(typeEquip)){
            return null;
        }
        return equippedItems[typeEquip];
    }
    public void AddItem(EquipType typeEquip, EquipableItemSO item){
        Debug.Assert(item.GetTypeEquip() == typeEquip);
        item.AddModifiers(characterStats);
        equippedItems[typeEquip] = item;
        OnEquipmentUpdate?.Invoke();
        OnTypeEquipUpdate?.Invoke(typeEquip);

    }

    

    public void RemoveItem(EquipType typeEquip){
        equippedItems[typeEquip].RemoveModifiers(characterStats);
        equippedItems.Remove(typeEquip);
        OnEquipmentUpdate?.Invoke();
        OnTypeEquipUpdate?.Invoke(typeEquip);
    }

    public object CaptureState()
    {
        var equippedItemsForSerialization = new Dictionary<EquipType, string>();
        foreach (var item in equippedItems)
        {
            equippedItemsForSerialization[item.Key] = item.Value.GetItemID();
        }
        
        return equippedItemsForSerialization;
       
    }

    public void RestoreState(object state)
    {
        equippedItems = new Dictionary<EquipType, EquipableItemSO>();
        var equippedItemsForSerialization = (Dictionary<EquipType, string>)state;
        foreach (var pair in equippedItemsForSerialization)
        {
           var item = (EquipableItemSO)InventoryItemSO.GetFromID(pair.Value);
           if(item != null) AddItem(pair.Key, item);
        }
    }
}