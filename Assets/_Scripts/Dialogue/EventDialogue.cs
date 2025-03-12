using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "EventDialogue", menuName = "Dialogue/EventDialogue", order = 0)]
public class EventDialogue : ScriptableObject {
    [SerializeField] public UnityEvent events = new UnityEvent();
    [Button]
    public void Raise()
    {
        events?.Invoke();
    }
}