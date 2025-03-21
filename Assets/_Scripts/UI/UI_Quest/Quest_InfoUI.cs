using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using HStrong.Quests;
public class Quest_InfoUI : MonoBehaviour {
    
    [SerializeField] QuestLogScronllingList scrollingList;
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
    }


}