using System;
using UnityEngine;

namespace HStrong.Quests{
    [Serializable]
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