using System;
using UnityEngine;
[Serializable]
public class AttackPoiseDamage : AttackData
{
    [field: SerializeField] public float TimeEff { get; private set;}
    [field: SerializeField] public float Rate { get; private set;}
    [field: SerializeField] public Poisetype type { get; private set;}
    [field: SerializeField] public GameObject prefabEff;
}
public enum Poisetype{
    Stun,
    freeze,
}

