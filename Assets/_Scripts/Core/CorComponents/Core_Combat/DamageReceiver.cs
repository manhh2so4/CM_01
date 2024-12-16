using System;
using UnityEngine;

public class DamageReceiver : CoreReceiver, IDamageable
{
    public CharacterStats Target(GameObject prefabHit)
    {
        particleManager?.StartParticlesRandomRotation(prefabHit.gameObject,this.transform.position + SetPosEff(prefabHit));
        Location = Vector3.zero;  
        return characterStats;
    }
}