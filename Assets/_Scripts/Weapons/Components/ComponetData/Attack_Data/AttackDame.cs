using System;
using UnityEngine;

[Serializable]
public class AttackDame : AttackData {
    [field: SerializeField] public int Amout { get; private set; }
}