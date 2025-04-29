using System;
using System.Collections;
using System.Collections.Generic;
using HStrong.Quests;
using UnityEngine;
using XNode;
[Node.CreateNodeMenuAttribute("Condition/ConditionItem")] 
public class ConditionItemNode : ConditionBaseNode
{
    [SerializeField] InventoryItemSO ItemSO;
    [SerializeField] int Amount;
    public ConditionItemType ConditionType;

    public override bool CheckCondition()
    {
        bool success = false;
        switch (ConditionType)
        {
            case ConditionItemType.HasItem:
                success = PlayerManager.GetInventory().HasItem( ItemSO ) || PlayerManager.GetEquipment().HasItem( ItemSO );
                break;

            case ConditionItemType.HasItemAmount:
                success = PlayerManager.GetInventory().HasItem( ItemSO , Amount );
                break;

            case ConditionItemType.HasEquippedItem:
                success = PlayerManager.GetEquipment().HasItem( ItemSO );
                break;
        }
        return success;
    }  
}
public enum ConditionItemType{
    HasItem,
    HasItemAmount,
    HasEquippedItem,
}


