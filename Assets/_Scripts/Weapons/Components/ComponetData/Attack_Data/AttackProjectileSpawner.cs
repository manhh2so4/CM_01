using System;
using UnityEngine;

[Serializable]
public class AttackProjectileSpawner : AttackData
{
    // This is an array as each attack can spawn multiple projectiles.
    [field: SerializeField] public ProjectileSpawnInfo SpawnInfos { get; private set; }
}


[Serializable]
public struct ProjectileSpawnInfo
{
    [field: SerializeField] public float amount { get; private set; }
    [field: SerializeField] public Vector2 Direction { get; private set; }
            
}