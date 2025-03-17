using System.Collections.Generic;
using HStrong.Quests;
using UnityEngine;
using UnityEngine.Events;

public class QuestLogScronllingList : MonoBehaviour {
    [Header("Quest Log Button")]
    [SerializeField] private GameObject questLogButtonPrefab;

    [Header("Components")]
    [SerializeField] private Transform contentParent;

    private Dictionary<string, QuestButton> idToButtonMap = new Dictionary<string, QuestButton>();

    public QuestButton CreateButtonIfNotExists(Quest quest, UnityAction selectAction){
        QuestButton questButton = null;

        if(!idToButtonMap.ContainsKey(quest.info.id)){
            questButton = InstantiateQuestLogButton(quest, selectAction);
        }else{
            questButton = idToButtonMap[quest.info.id];
        }
        return questButton;
    }
    QuestButton InstantiateQuestLogButton(Quest quest, UnityAction selectAction){
        QuestButton questButton = Instantiate(questLogButtonPrefab,contentParent).GetComponent<QuestButton>();
        questButton.gameObject.name = quest.info.id + "_button";

        questButton.Initialize(quest.info.displayName, ()=> {

            selectAction.Invoke();

        });

        idToButtonMap[quest.info.id] = questButton;
        return questButton;

    }
    
}