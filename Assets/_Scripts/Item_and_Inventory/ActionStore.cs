using System;
using System.Collections.Generic;
using HStrong.Saving;
using TMPro;
using UnityEngine;

public class ActionStore : MonoBehaviour,ISaveable
{
    Dictionary<int, DockerItemSlot> dockerItems = new  Dictionary<int, DockerItemSlot>();
    private class DockerItemSlot{
        public ActionItem item;
        public int number; 
    }
    public event Action StoreUpdateed;
    public ActionItem GetAction(int index){
        if(dockerItems.ContainsKey(index))
        {
            return dockerItems[index].item;
        }
        return null;
    }
    public int GetNumber(int  index)
    {
        if(dockerItems.ContainsKey(index))
        {
            return dockerItems[index].number;
        }
        return 0;
    }
    public void AddAction(InventoryItemSO item, int index, int number)
    { 
        if(dockerItems.ContainsKey(index)){
            if(object.ReferenceEquals(item, dockerItems[index].item)){
                dockerItems[index].number += number;
            }
        }else
        {
            var slot = new DockerItemSlot();
            slot.item = item as ActionItem;
            slot.number = number;
            dockerItems[index] = slot;
        }
        StoreUpdateed?.Invoke();
    }
    public void RemoveItem(int index,int number){
        if(dockerItems.ContainsKey(index)){
            dockerItems[index].number -= number;
            if(dockerItems[index].number <= 0){
                dockerItems.Remove(index);
            }
            StoreUpdateed?.Invoke();
        }
    }
    public int MaxAcceptable(InventoryItemSO item, int index){
        var actionItem = item as ActionItem;
        if(!actionItem) return 0;
        if(dockerItems.ContainsKey(index) && !object.ReferenceEquals(item, dockerItems[index].item))
        {
            return 0;
        }
        if(actionItem.IsConsumable()){
            return int.MaxValue;
        }
        if(dockerItems.ContainsKey(index)){
            return 0;
        }
        return 1;
    }

    object ISaveable.CaptureState()
    {
        var state = new Dictionary<int,DockerItemRecord>();
        foreach (var pair in dockerItems)
        {
            var record = new DockerItemRecord();
            record.itemID = pair.Value.item.GetItemID();
            record.number = pair.Value.number;
            state[pair.Key] = record;
        }
        return state;
    }

    void ISaveable.RestoreState(object state)
    {
        var stateDict = (Dictionary<int,DockerItemRecord>)state;
        foreach (var pair in stateDict){
            AddAction(InventoryItemSO.GetFromID(pair.Value.itemID), pair.Key, pair.Value.number);
        }
    }
    
    [System.Serializable]
    private struct DockerItemRecord{
        public string itemID;
        public int number;
    }

    
}