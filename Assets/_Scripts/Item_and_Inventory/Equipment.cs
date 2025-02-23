using System;
using System.Collections.Generic;
using HStrong.Saving;
using UnityEngine;

public class Equipment : MonoBehaviour//ISaveable
{
    Dictionary<EquipLocation,EquipableItemSO> equippedItems = new Dictionary<EquipLocation,EquipableItemSO>();
    public event Action OnEquipmentUpdate;
    public event Action<EquipLocation> OnTypeEquipUpdate;
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
        Debug.Log("AddItem");

        item.AddModifiers(characterStats);
        equippedItems[typeEquip] = item;
        OnEquipmentUpdate?.Invoke();
        OnTypeEquipUpdate?.Invoke(typeEquip);

    }

    

    public void RemoveItem(EquipLocation typeEquip){
        equippedItems[typeEquip].RemoveModifiers(characterStats);
        Debug.Log("RemoveItem");

        equippedItems.Remove(typeEquip);
        OnEquipmentUpdate?.Invoke();
        OnTypeEquipUpdate?.Invoke(typeEquip);
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