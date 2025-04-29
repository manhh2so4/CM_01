using UnityEngine;
[CreateAssetMenu( menuName = "Inventory/Action Item")]
public class ActionItem : InventoryItemSO {
    [SerializeField] bool isConsumable = false;
    [SerializeField] int duration;
    [SerializeField] int valuePerTick;
    [SerializeField] BuffType buffType;
    public virtual void Use( BuffStat buffStat ){
        
        buffStat.AddBuff(buffType,valuePerTick,duration,this.GetIcon());

    }
    public bool IsConsumable() {
        return isConsumable;
    }

}