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
        private void UpdateState(){
            questStepState.status = CountCollected + "/" + data.countToComplete;
            questStepState.state = CountCollected >= data.countToComplete ? QuestStepStatus.COMPLETED : QuestStepStatus.IN_PROGRESS;
            ChangeState(questStepState);
        }

        protected override void SetQuestStepState( QuestStepState state ){
            if(state.status != ""){
                this.CountCollected = int.Parse(state.status.Split('/')[0]);
            }else{
                this.CountCollected = 0;
            }
            UpdateState();
        }

    }
}