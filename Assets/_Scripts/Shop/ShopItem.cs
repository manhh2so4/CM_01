using UnityEngine;
public class ShopItem{
    InventoryItemSO item;
    int count; 
    int price;
    int quantity;
    public ShopItem(InventoryItemSO item, int count, int price, int stock){
        this.item = item;
        this.count = count;
        this.price = price;
        this.quantity = stock;
    }
    public Sprite GetIcon()
    {
        return item.GetIcon();
    }
    public string GetName()
    {
        return item.GetDisplayName();
    }
    public int GetCount()
    {
        return count;
    }
    public InventoryItemSO GetInventoryItem()
    {
        return item;
    }
    public int GetPrice()
    {
        return price;
    }
    public int GetQuantity()
    {
        return quantity;
    }
}

