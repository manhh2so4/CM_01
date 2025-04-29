using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using HStrong.Quests;
public class Quest_InfoUI : MonoBehaviour {
    [SerializeField] TextMeshProUGUI NameQuest;
    [SerializeField] TextMeshProUGUI NameQuestStep;
    [SerializeField] TextMeshProUGUI StatusQuestStep;

    [SerializeField] QuestLogScronllingList scrollingList;
    public Quest questFollow;
    void OnEnable()
    {
        this.GameEvents().questEvent.onQuestStateChange += QuestStateChange;
    }
    void OnDisable()
    {
        this.GameEvents().questEvent.onQuestStateChange -= QuestStateChange;
    }

    private void QuestStateChange(Quest quest)
    {
        scrollingList.CreateButtonIfNotExists(quest);

        if(questFollow == null || questFollow.state == QuestState.HAS_QUEST){
            questFollow = quest;
        }

        if(questFollow.info.id == quest.info.id){
            UpdateQuestNityfy();
        }
    }
    void UpdateQuestNityfy(){
        if(questFollow.state == QuestState.IN_PROGRESS){
            NameQuest.text = questFollow.info.displayName;
            for (int i = 0; i < questFollow.GetQ_StepData().Length; i++){
                QuestStepState questStepState = questFollow.GetQuestStepStates()[i];

                if(questStepState.state == QuestStepStatus.IN_PROGRESS){
                    NameQuestStep.text = questFollow.GetQ_StepData()[i].__description__;
                    StatusQuestStep.text = questStepState.status;
                    return;
                }

            }
        }

        if(questFollow.state == QuestState.CAN_FINISH){
            NameQuest.text = questFollow.info.displayName;
            NameQuestStep.text = "Hoàn thành";
            StatusQuestStep.text = "";
        }
    }



}