using UnityEngine;

public class InventoryUI : MonoBehaviour {
    [SerializeField] InventorySlotUI InventoryItemPrefab = null;
    Inventory playerInventory;

    private void Start()
    {
        playerInventory = Inventory.GetPlayerInventory();
        playerInventory.inventoryUpdated += Redraw;
        Redraw();
    }
    private void Redraw()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < playerInventory.GetSize(); i++)
        {
            var itemUI = Instantiate(InventoryItemPrefab, transform);
            itemUI.gameObject.name = "Item_slot_" + i;
            itemUI.Setup(playerInventory, i);
        }
    }
}