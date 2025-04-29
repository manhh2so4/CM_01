using System;
using UnityEngine;
public class HealthEvent{
    public event Action<int> onHPplayerChange;
    public void ChangeHP(int hp) => onHPplayerChange?.Invoke(hp);
}