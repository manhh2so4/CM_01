using System;
using UnityEngine;
namespace HStrong.Quests{
    public abstract class Q_StepComponents : MonoBehaviour
    {
        
        bool isFinished = false;
        string questId ;
        int stepIndex;
        protected QuestStepState questStepState = new QuestStepState();
        public virtual void Init( Q_StepData data , string _QuestID , int _stepIndex , QuestStepState _questStepState )
        {
            this.questId = _QuestID;
            this.stepIndex = _stepIndex;
            SetData(data);
            SetQuestStepState(_questStepState);
            SubscribeHandlers();
        }

        protected void FinishQuestStep(){

            if(!isFinished){
                isFinished = true;
                this.GameEvents().questEvent.AdvanceQuest(questId);
                Destroy(this.gameObject);
            }

        }
        
        protected virtual void SubscribeHandlers()
        {
            
        }
        protected virtual void OnDisable()
        {
            
        }
        protected virtual void ChangeState(QuestStepState state){
            this.GameEvents().questEvent.QuestStepStateChange(questId, stepIndex, state);
        }
        protected virtual void SetData(Q_StepData data){}
        protected virtual void SetQuestStepState(QuestStepState state){}

    }
    public abstract class Q_StepComponents<T> : Q_StepComponents where T : Q_StepData
    {
        [field: SerializeReference] protected T data;
        protected override void SetData(Q_StepData _data) {
            data = _data as T;
        }
    }
}
