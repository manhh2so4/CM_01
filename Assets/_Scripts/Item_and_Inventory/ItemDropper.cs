using System.Collections.Generic;
using HStrong.Saving;
using UnityEngine;

public class ItemDropper : MonoBehaviour, ISaveable
{
    private List<Pickup> droppedItems = new List<Pickup>();
    public void DropItem(InventoryItemSO item, int number)
    {
        SpawnPickup(item,GetDropLocation(), number);
    }
    protected virtual Vector3 GetDropLocation()
    {
        return transform.position + new Vector3(0,.3f,0);
    }
    

    public void SpawnPickup(InventoryItemSO item, Vector3 spawnLocation, int number)
    {
        var pickup = item.SpawnPickup(spawnLocation, number);
        droppedItems.Add(pickup);
    }

    [System.Serializable]
    private struct DropRecord
    {
        public string itemID;
        public SerializableVector3 position;
        public int number;
    }
    public object CaptureState()
    {
        RemoveDestroyedDrops();
        var droppedItemsList = new DropRecord[droppedItems.Count];
        for (int i = 0; i < droppedItemsList.Length; i++)
        {
            droppedItemsList[i].itemID = droppedItems[i].GetItem().GetItemID();
            droppedItemsList[i].position = new SerializableVector3(droppedItems[i].transform.position);
            droppedItemsList[i].number = droppedItems[i].GetNumber();
        }
        return droppedItemsList;
    }

    public void RestoreState(object state)
    {
        var droppedItemsList = (DropRecord[])state;
        foreach (var item in droppedItemsList)
        {
            var pickupItem = InventoryItemSO.GetFromID(item.itemID);
            Vector3 position = item.position.ToVector();
            int number = item.number;
            SpawnPickup(pickupItem, position, number);
        }
    }

    private void RemoveDestroyedDrops(){
        var newList = new List<Pickup>();
        foreach (var item in droppedItems)
        {
            if (item != null)
            {
                newList.Add(item);
            }
        }
        droppedItems = newList;
    }
}