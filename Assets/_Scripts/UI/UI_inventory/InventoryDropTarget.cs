using HStrong.Core.UI.Dragging;
using UnityEngine;

public class InventoryDropTarget : MonoBehaviour, IDragDestination<InventoryItemSO>
{
    public void AddItems(InventoryItemSO item, int number)
    {
        var player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<ItemDropper>().DropItem(item,number);
    }

    public int MaxAcceptable(InventoryItemSO item)
    {
        return int.MaxValue;
    }
}