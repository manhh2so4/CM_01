using System;
using UnityEngine;
public class AttackTargeter : AttackData {
    [field: SerializeField] public float radius { get; private set; }
    [field: SerializeField] public LayerMask DamageableLayer { get; private set; }
}