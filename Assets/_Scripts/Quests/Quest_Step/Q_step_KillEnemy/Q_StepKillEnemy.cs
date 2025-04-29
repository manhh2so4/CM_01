using UnityEngine;
namespace HStrong.Quests{
    public class Q_StepKillEnemy : Q_StepComponents<KillEnemyData>{
        int countKilled = 0;
        protected override void SubscribeHandlers()
        {
            this.GameEvents().miscEvents.onKillEnemy += CoinCollected;
        }

        protected override void OnDisable()
        {
            this.GameEvents().miscEvents.onKillEnemy -= CoinCollected;
        }
        private void CoinCollected(EnemyEntity enemy)
        {
            if(enemy.idEnemy != data.idEnemy) return;

            if (countKilled < data.countComplete)
            {
                countKilled++;
                UpdateState();
            }
            if (countKilled >= data.countComplete)
            {
                FinishQuestStep();
            }
        }

        private void UpdateState(){
            questStepState.status = countKilled + "/" + data.countComplete;
            questStepState.state = countKilled >= data.countComplete ? QuestStepStatus.COMPLETED : QuestStepStatus.IN_PROGRESS;
            ChangeState(questStepState);
        }

        protected override void SetQuestStepState( QuestStepState state ){
            if(state.status != ""){
                this.countKilled = int.Parse(state.status.Split('/')[0]);
            }else{
                this.countKilled = 0;
            }
            UpdateState();
        }

    }
}