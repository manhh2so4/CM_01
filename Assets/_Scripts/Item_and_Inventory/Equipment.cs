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
    [SerializeField] PaintChar paintChar;
    [SerializeField] Skill_Manager skillManager;
    
    private void Start() {
        Core core = GetComponentInChildren<Core>();
        characterStats = core.GetCoreComponent<CharacterStats>();
        paintChar = core.GetCoreComponent<PaintChar>();
        skillManager = GetComponent<Skill_Manager>();
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
        UpdateTypeEquip(typeEquip);
        
    }

    public void RemoveItem(EquipType typeEquip){
        equippedItems[typeEquip].RemoveModifiers( characterStats );
        equippedItems.Remove(typeEquip);
        OnEquipmentUpdate?.Invoke();
        UpdateTypeEquip(typeEquip);
        
    }

    void UpdateTypeEquip(EquipType typeEquip)
    {
        switch (typeEquip)
        {
            case EquipType.Vukhi:

				if(GetItemInSlot(EquipType.Vukhi) != null) {
				    paintChar.SetWeapon(GetItemInSlot(EquipType.Vukhi).GetImageDraw().spriteInfos);
                    skillManager.UpdateTypeEquip();
				}else paintChar.SetWeapon(null);

                break;
            case EquipType.Ao:
				if(GetItemInSlot(EquipType.Ao) != null) {
					paintChar.SetBody(GetItemInSlot(EquipType.Ao).GetImageDraw().spriteInfos);
				}else paintChar.SetBody(null);

                break;
            case EquipType.Quan:

				if(GetItemInSlot(EquipType.Quan) != null) {
					paintChar.SetLeg(GetItemInSlot(EquipType.Quan).GetImageDraw().spriteInfos);
				}else paintChar.SetLeg(null);

                break;
				
            default:
                break;
        }
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