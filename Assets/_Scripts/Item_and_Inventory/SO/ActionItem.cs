using UnityEngine;
[CreateAssetMenu( menuName = "Inventory/Action Item")]
public class ActionItem : InventoryItemSO {
    [SerializeField] bool isConsumable = false;
    public virtual void Use(GameObject user) {
        Debug.Log("used");
    }
    public bool IsConsumable() {
        return isConsumable;
    }
 }