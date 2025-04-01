using System;
using UnityEngine;

public class DamageReceiver : CoreReceiver, IDamageable
{
    public CharacterStats Target(Effect_Instance prefabHit)
    {
        if(prefabHit == null ) return characterStats; 
        
        Effect_Instance effect = particleManager.CreatRandomRotation( prefabHit, this.transform.position + SetPosEff(prefabHit) );
        effect.SetData(core.SortingLayerID,core.uniqueID);
        return characterStats;
    }
}