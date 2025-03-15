using UnityEngine;

namespace HStrong.Quests{
    public class CollectItemData : Q_StepData
    {
        [field: SerializeField] public InventoryItemSO itemToCollect;
        [field: SerializeField] public int countToComplete;
        protected override void SetComponentDependency()
        {
            ComponentDependency = typeof(Q_StepCollect_Item);
        }
    }
}