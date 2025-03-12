using UnityEngine;
using UnityEngine.Events;
public class EventDialogueManager : MonoBehaviour {
    public EventDialogueType[] AllEvents;
    private void OnEnable()
    {
        foreach (var item in AllEvents)
        {
            item.SetUpEvent();
        }
    }
    
}

[System.Serializable]
public struct EventDialogueType{
    [SerializeField] EventDialogue gameEvent;
    public UnityEvent response;
    public void SetUpEvent()
    {
        gameEvent.events = response;
    }

}