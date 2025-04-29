using System;
using System.Collections.Generic;
using HStrong.Saving;
using UnityEngine;

public class Equipment : MonoBehaviour,ISaveable
{
    Dictionary<EquipType,EquipableItemSO> equippedItems = new Dictionary<EquipType,EquipableItemSO>();
    public event Action OnEquipmentUpdate;
    
    [SerializeField] CharacterStats characterStats;
    [SerializeField] PaintChar paintChar;
    [SerializeField] SkillTreeManager skillTreeManager;


    void Awake()
    {
        characterStats = PlayerManager.GetCharStats();
        paintChar = PlayerManager.GetCore().GetCoreComponent<PaintChar>();
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

    public bool HasItem(InventoryItemSO item){
        EquipableItemSO _equipableItem = item as EquipableItemSO;
        if(_equipableItem != null){
            return equippedItems.ContainsValue(_equipableItem);
        }
        return false;
    }

    void UpdateTypeEquip(EquipType typeEquip)
    {
        switch (typeEquip)
        {
            case EquipType.Vukhi:

				if(GetItemInSlot(EquipType.Vukhi) != null) {
				    paintChar.SetWeapon( GetItemInSlot(EquipType.Vukhi).GetImageDraw().spriteInfos);
				}else paintChar.SetWeapon(null);

                skillTreeManager.UpdateSkillTreeUI( GetItemInSlot(EquipType.Vukhi) );
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