using System;
using UnityEngine;

public class InputEvent {

    public event Action onQuestLogTogglePressed;
    public void Click_Q() => onQuestLogTogglePressed?.Invoke();
    
}