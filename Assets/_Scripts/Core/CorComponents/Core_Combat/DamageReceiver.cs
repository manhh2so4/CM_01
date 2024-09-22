using System;
using UnityEngine;

public class DamageReceiver : CoreReceiver, IDamageable
{
    
    public CharacterStats Target( GameObject prefabHit)
    {
        PrefabEff = prefabHit;
        SetPosEff();
        particleManager?.StartParticlesRandomRotation(PrefabEff,this.transform.position + Location);
        Location = Vector3.zero;  
        return characterStats;
    }
}