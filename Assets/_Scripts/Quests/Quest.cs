using System.Collections.Generic;
using UnityEngine;

namespace HStrong.Quests
{
    [System.Serializable]
    public class Quest{
        
        public QuestInfoSO info;
        public QuestState state;
        private int currentQuestStepIndex;
        [SerializeField] QuestStepState[] questStepStates;
        Q_StepData[] questSteps;
        public QuestStepState[] GetQuestStepStates() => questStepStates; 
        public Q_StepData[] GetQ_StepData() => questSteps;
        // Constructor for creating a new quest
        public Quest(QuestInfoSO questInfo)
        {
            this.info = questInfo;
            this.state = QuestState.HAS_QUEST;
            questInfo.state = this.state;
            this.currentQuestStepIndex = 0;
            this.questSteps = questInfo.GetStepData();
            this.questStepStates = new QuestStepState[questSteps.Length];
            for (int i = 0; i < questStepStates.Length; i++)
            {
                questStepStates[i] = new QuestStepState("", QuestStepStatus.NOT_STARTED);
            }
        }
        // Constructor for loading a quest from saved data
        public Quest(QuestInfoSO questInfo, QuestState questState, int _currentQuestStepIndex, QuestStepState[] _questStepStates)
        {
            this.info = questInfo;
            this.state = questState;
            this.currentQuestStepIndex = _currentQuestStepIndex;
            this.questStepStates = _questStepStates;
            if (this.questStepStates.Length != this.questSteps.Length)
            {
                Common.LogWarning( " Quest Step Prefabs and Quest Step States are "
                    + "of different lengths. This indicates something changed "
                    + "with the QuestInfo and the saved data is now out of sync. "
                    + "Reset your data - as this might cause issues. QuestId: " + this.info.id);
            }
        }
        public void MoveToNextStep()
        {
            currentQuestStepIndex++;
        }
        public bool CanNextStep()
        {
            return ( currentQuestStepIndex < questSteps.Length );
        }
        public void InstantiateCurrentQuestStep(Transform parent){

            if(CanNextStep() == false)
            {
                Common.LogWarning("Tried to get quest step prefab, but stepIndex was out of range indicating that "
                + "there's no current step: QuestId=" + info.id + ", stepIndex=" + currentQuestStepIndex);
                return;
            }
            var dependency = questSteps[currentQuestStepIndex].ComponentDependency;
            var q_StepComponent = new GameObject(info.id +" Step " + currentQuestStepIndex).AddComponent(dependency) as Q_StepComponents;
            q_StepComponent.transform.SetParent(parent);
            q_StepComponent.Init( questSteps[currentQuestStepIndex], info.id, currentQuestStepIndex , questStepStates[currentQuestStepIndex] );

        }

        public void StoreQuestStepState(QuestStepState questStepState, int stepIndex)
        {
            if(stepIndex < questStepStates.Length){

                questStepStates[stepIndex] = questStepState;

            }else{
                Common.LogWarning("Tried to access quest step data, but stepIndex was out of range: "
                + "Quest Id = " + info.id + ", Step Index = " + stepIndex);
            }
        }

        public QuestData GetQuestData(){
            return new QuestData( state, currentQuestStepIndex, questStepStates);
        }

    }
}

public enum QuestState
{
    HAS_QUEST,
    CAN_START,
    IN_PROGRESS,
    CAN_FINISH,
    FINISHED
}