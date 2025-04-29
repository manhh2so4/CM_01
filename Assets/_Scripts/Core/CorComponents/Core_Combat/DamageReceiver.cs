using System;
using UnityEngine;

public class DamageReceiver : CoreReceiver, IDamageable
{
    public CharacterStats GetTarget(BaseEffect prefabHit)
    {
        if(prefabHit == null ) return characterStats; 
        
        BaseEffect effect = particleManager.CreatRandomRotation( prefabHit, this.transform.position + SetPosEff(prefabHit) );
        effect.SetData(core.SortingLayerID,core.uniqueID);
        return characterStats;
    }
}