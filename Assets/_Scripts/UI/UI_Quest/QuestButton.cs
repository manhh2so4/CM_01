using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;

public class QuestButton : MonoBehaviour, IPointerClickHandler {

    private TextMeshProUGUI buttonText;
    private UnityAction onSelectAction;
    public void Initialize(string displayName, UnityAction selectAction){

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
            case QuestState.REQUIREMENTS_NOT_MET:

            case QuestState.CAN_START:
                buttonText.color = Color.red;
                break;

            case QuestState.IN_PROGRESS:

            case QuestState.CAN_FINISH:
                buttonText.color = Color.yellow;
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