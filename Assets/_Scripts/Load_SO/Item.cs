using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "Inventory/Item2")]
public class Item : ScriptableObject
{
    public string itemName;
    public string itemDescription;
    public int itemValue;
}