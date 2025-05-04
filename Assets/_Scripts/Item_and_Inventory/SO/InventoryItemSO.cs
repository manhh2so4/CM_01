using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Inventory/Item")]
public class InventoryItemSO : ScriptableObject
{
    [SerializeField] string itemID = null;
    [SerializeField] string displayName = null;
    [SerializeField][TextArea] string description = null;
    [SpritePreview][SerializeField] Sprite icon = null;
    [SerializeField] bool stackable = false;
    [SerializeField] int price;

    static Dictionary<string, InventoryItemSO> itemLookupCache;
    
    public static InventoryItemSO GetFromID(string itemID)
    {
        if (itemLookupCache == null)
        {
            itemLookupCache = new Dictionary<string, InventoryItemSO>();
            var itemList = Resources.LoadAll<InventoryItemSO>("Inventory/");
            foreach (var item in itemList)
            {
                if (itemLookupCache.ContainsKey(item.itemID))
                {
                    Debug.LogError(string.Format("Looks like there's a duplicate GameDevTV.UI.InventorySystem ID for objects: {0} and {1}", itemLookupCache[item.itemID], item));
                    continue;
                }

                itemLookupCache[item.itemID] = item;
            }
            Debug.Log("itemLookupCache: " + itemLookupCache.Count);
        }
        if (itemID == null || !itemLookupCache.ContainsKey(itemID)) return null;
        return itemLookupCache[itemID];
    }
    public Pickup SpawnPickup(Vector3 position, int number, Vector2 dirV = default(Vector2))
    {
        Pickup pickup = PoolsContainer.GetObject( this.GetPrefab<Pickup>() , position);
        pickup.Setup(this, number, dirV);
        return pickup;
    }
    public void SetName( string name, string description)
    {
        this.displayName = name;
        this.description = description;
    }
    public Sprite GetIcon()
    {
        return icon;
    }
    public string GetItemID()
    {
        return itemID;
    }
    public bool IsStackable()
    {
        return stackable;
    }
    public string GetDisplayName()
    {
        return displayName;
    }
    public string GetDescription()
    {
        return description;
    }
    public int GetPrice()
    {
        return price;
    }

    // PRIVATE
#if UNITY_EDITOR 
    void OnValidate(){
        itemID = this.name;
    }

    public void SetData(string displayName, string description, Sprite icon, bool stackable, int price){
        this.displayName = displayName;
        this.description = description;
        this.icon = icon;
        this.stackable = stackable;
        this.price = price;
    }
#endif
}

[System.Serializable]
public class ItemAndCount
{
    public InventoryItemSO item;
    [Min(1)]
    public int count;
    
}
