using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;
using System;

public class QuestButton : MonoBehaviour, IPointerClickHandler {

    private TextMeshProUGUI buttonText;
    private Action onSelectAction;
    public void Initialize(string displayName , Action selectAction){

        this.buttonText = this.GetComponentInChildren<TextMeshProUGUI>();
        this.buttonText.text = displayName;
        this.onSelectAction = selectAction;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        onSelectAction?.Invoke();
    }
    public void SetState(QuestState state){

        switch(state){
            case QuestState.HAS_QUEST:

            case QuestState.CAN_START:
                buttonText.color = Color.blue;
                break;

            case QuestState.IN_PROGRESS:
                buttonText.color = Color.yellow;
                break;
            case QuestState.CAN_FINISH:
                buttonText.color = Color.magenta;
                break;

            case QuestState.FINISHED:
                buttonText.color = Color.green;
                break;

            default:
                Common.LogWarning("Quest State not recognized by switch statement: " + state);
                break;
        }
    }
}