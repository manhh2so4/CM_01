using System;
using UnityEngine;
namespace HStrong.Quests{
    public abstract class Q_StepComponents : MonoBehaviour {
        bool isFinished = false;
        string questId ;
        protected void FinishQuestStep(){
            if(!isFinished){
                isFinished = true;
                this.GameEvents().questEvent.AdvanceQuest(questId);
                Destroy(this.gameObject);
            }
        }
        public virtual void Init( Q_StepData data , string _QuestID)
        {
            this.questId = _QuestID;
            SetData(data);
            SubscribeHandlers();
        }

        protected virtual void SubscribeHandlers()
        {
            
        }
        protected virtual void OnDisable()
        {
            
        }
        protected virtual void  SetData(Q_StepData data){}
    }
    public abstract class Q_StepComponents<T> : Q_StepComponents where T : Q_StepData
    {
        [field: SerializeReference] protected T data;
        protected override void SetData(Q_StepData _data) {
            data = _data as T;
        }

    }
}
