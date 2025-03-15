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
            }

            if (coinsCollected >= data.coinsToComplete)
            {
                FinishQuestStep();
            }
        }

    }
}