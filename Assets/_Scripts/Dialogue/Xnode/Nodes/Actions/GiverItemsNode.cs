using System.Collections;
using System.Collections.Generic;
using HStrong.Quests;
using UnityEngine;
using XNode;
[Node.CreateNodeMenuAttribute("Action/GiverItems")] 
public class GiverItemsNode : BaseActionNode
{
    [SerializeField] List<ItemAndCount> ItemSOs;
    public override void Trigger(){
        foreach(ItemAndCount item in ItemSOs){
            bool isAdd = PlayerManager.GetInventory().AddToFirstEmptySlot(item.item,item.count);
            if(isAdd){
                NotifyUIManager.NotifyUI( $"Nhận {item.count} {item.item.GetDisplayName()}", item.item.GetIcon() );
            }else{
                NotifyUIManager.NotifyUI( $"Không có chỗ trong túi đồ", null );
            }
        }
    }
}