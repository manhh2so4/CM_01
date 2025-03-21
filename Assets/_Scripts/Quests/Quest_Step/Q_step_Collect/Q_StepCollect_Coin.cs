using UnityEngine;
namespace HStrong.Quests{
    public class Q_StepCollect_Coin : Q_StepComponents<CollectCoinData>{
        int coinsCollected = 0;
        protected override void SubscribeHandlers()
        {
            this.GameEvents().miscEvents.onCoinCollected += CoinCollected;
        }

        protected override void OnDisable()
        {
            this.GameEvents().miscEvents.onCoinCollected -= CoinCollected;
        }

        private void CoinCollected()
        {
            if (coinsCollected < data.coinsToComplete)
            {
                coinsCollected++;
                UpdateState();
            }

            if (coinsCollected >= data.coinsToComplete)
            {
                FinishQuestStep();
            }
        }
        private void UpdateState(){
            questStepState.status = coinsCollected + "/" + data.coinsToComplete;
            questStepState.state = coinsCollected >= data.coinsToComplete ? QuestStepStatus.COMPLETED : QuestStepStatus.IN_PROGRESS;
            ChangeState(questStepState);
        }

        protected override void SetQuestStepState( QuestStepState state ){
            if(state.status != ""){
                this.coinsCollected = int.Parse(state.status.Split('/')[0]);
            }else{
                this.coinsCollected = 0;
            }
            
            UpdateState();
        }

    }
}