using System;
using System.Collections.Generic;
using HStrong.Saving;
using UnityEngine;

public class Equipment : MonoBehaviour//ISaveable
{
    Dictionary<EquipLocation,EquipableItemSO> equippedItems = new Dictionary<EquipLocation,EquipableItemSO>();
    public event Action OnEquipmentUpdate;
    [SerializeField] CharacterStats characterStats;
    
    private void Start() {
        Core core = GetComponentInChildren<Core>();
        characterStats = core.GetCoreComponent<CharacterStats>();
    }
    public EquipableItemSO GetItemInSlot(EquipLocation typeEquip){
        if(!equippedItems.ContainsKey(typeEquip)){
            return null;
        }
        return equippedItems[typeEquip];
    }
    public void AddItem(EquipLocation typeEquip, EquipableItemSO item){
        Debug.Assert(item.GetTypeEquip() == typeEquip);
        item.AddModifiers(characterStats);
        equippedItems[typeEquip] = item;
        OnEquipmentUpdate?.Invoke();

    }
    

    public void RemoveItem(EquipLocation typeEquip){
        equippedItems[typeEquip].RemoveModifiers(characterStats);
        equippedItems.Remove(typeEquip);
        OnEquipmentUpdate?.Invoke();

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