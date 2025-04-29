using System;
using UnityEngine;

namespace HStrong.Quests{
    [Serializable]
    public class KillEnemyData : Q_StepData
    {
        [SerializeField] public int idEnemy;
        [Min(0)]
        [SerializeField] public int countComplete ;

        protected override void SetComponentDependency()
        {
            ComponentDependency = typeof(Q_StepKillEnemy);
        }
    }
}