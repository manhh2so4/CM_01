using System;
using UnityEngine;

public class DamageReceiver : CoreReceiver, IDamageable
{
    public CharacterStats Target(GameObject prefabHit)
    {
        if(prefabHit == null ) return characterStats; 
        particleManager?.StartParticlesRandomRotation(prefabHit.gameObject,this.transform.position + SetPosEff(prefabHit));
        Location = Vector3.zero;  
        return characterStats;
    }
}