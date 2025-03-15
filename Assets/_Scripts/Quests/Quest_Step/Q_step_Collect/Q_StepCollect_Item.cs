using UnityEngine;
namespace HStrong.Quests{
    public class Q_StepCollect_Item : Q_StepComponents<CollectItemData>{

        int CountCollected = 0;
        
        protected override void SubscribeHandlers()
        {
            this.GameEvents().inventoryEvent.OnAddItemInv += CoinCollected;
        }

        protected override void OnDisable()
        {
            this.GameEvents().inventoryEvent.OnAddItemInv -= CoinCollected;
        }

        private void CoinCollected(InventoryItemSO item, int number)
        {
            InventoryItemSO ItemReQuire = data.itemToCollect;
            if(item.GetItemID() == ItemReQuire.GetItemID()){
                if(ItemReQuire.IsStackable()){
                    CountCollected += number;
                }else{
                    CountCollected++;
                }

                if (CountCollected >= data.countToComplete)
                {
                    FinishQuestStep();
                }
            }
        }

    }
}