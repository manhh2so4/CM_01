using UnityEngine;

public class Pickup : MonoBehaviour,IClicker {
    InventoryItemSO item;
    int number = 1;

    Inventory inventory;
    private void Start()
    {
        var player = PlayerManager.GetPlayer();
        inventory = player.GetComponent<Inventory>();
        
    }
    public void Setup(InventoryItemSO item, int number)
    {
        
        this.item = item;
        GetComponent<SpriteRenderer>().sprite = this.item.GetIcon();
        if (!item.IsStackable())
        {
            number = 1;
        }
        this.number = number;
    }
    public InventoryItemSO GetItem()
    {
        return item;
    }
    public int GetNumber()
    {
        return number;
    }
    public void PickupItem()
    {
        bool foundSlot = inventory.AddToFirstEmptySlot(item, number);
        if (foundSlot)
        {
            Destroy(gameObject);
        }
    }
    public bool CanBePickedUp()
    {
        return inventory.HasSpaceFor(item);
    }

    public void OnClick()
    {
        PickupItem();
    }
    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("Player")){
            PickupItem();
        }
    }
}
