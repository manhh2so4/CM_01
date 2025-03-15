using UnityEngine;

namespace HStrong.Quests{
    public class CollectCoinData : Q_StepData
    {
        [Min(1)]
        [SerializeField ] public int coinsToComplete ;
        protected override void SetComponentDependency()
        {
            ComponentDependency = typeof(Q_StepCollect_Coin);
        }
    }
}