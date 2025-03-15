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
            questButton = null;
        }else{
            questButton = idToButtonMap[quest.info.id];
        }
        return questButton;
    }
    QuestButton InstantiateQuestLogButton(Quest quest, UnityAction selectAction){
        QuestButton questButton = Instantiate(questLogButtonPrefab,contentParent).GetComponent<QuestButton>();
        questButton.gameObject.name = quest.info.id + "_button";

        questButton.Initialize(quest.info.displayName, ()=> {
            //UpdateScrolling(this.transform);
        });

        idToButtonMap[quest.info.id] = questButton;
        return questButton;

    }

    //  private void UpdateScrolling(RectTransform buttonRectTransform)
    // {
    //     // calculate the min and max for the selected button
    //     float buttonYMin = Mathf.Abs(buttonRectTransform.anchoredPosition.y);
    //     float buttonYMax = buttonYMin + buttonRectTransform.rect.height;

    //     // calculate the min and max for the content area
    //     float contentYMin = contentRectTransform.anchoredPosition.y;
    //     float contentYMax = contentYMin + scrollRectTransform.rect.height;

    //     // handle scrolling down
    //     if (buttonYMax > contentYMax)
    //     {
    //         contentRectTransform.anchoredPosition = new Vector2(
    //             contentRectTransform.anchoredPosition.x,
    //             buttonYMax - scrollRectTransform.rect.height
    //         );
    //     }
    //     // handle scrolling up
    //     else if (buttonYMin < contentYMin) 
    //     {
    //         contentRectTransform.anchoredPosition = new Vector2(
    //             contentRectTransform.anchoredPosition.x,
    //             buttonYMin
    //         );
    //     }
    //}
    
}