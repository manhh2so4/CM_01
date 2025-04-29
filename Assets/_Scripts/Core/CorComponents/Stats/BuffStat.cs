using System;
using System.Collections.Generic;
using UnityEngine;
public class BuffStat : CoreComponent
{

    CharacterStats charStats;
    [SerializeField] BaseBuffStat baseBuffStat;

    protected override void Awake()
    {
        base.Awake();
        charStats = core.GetCoreComponent<CharacterStats>();
    }
    
    public void AddBuff(BuffType buffType, int valuePerTick, float duration,Sprite icon = null){
        foreach(BaseBuffStat child in GetComponentsInChildren<BaseBuffStat>()){
            if(child.buffType == buffType){
                child.Remove();

            }
        }

        BaseBuffStat buff = PoolsContainer.GetObject(baseBuffStat,transform);
        buff.StartBuff(valuePerTick, duration, GetStatEffec(buffType), icon);

        

    }
    Stat GetStatEffec(BuffType buffType){
        switch(buffType){
            case BuffType.HealthPotion: 
                return charStats.Health;

            case BuffType.ManaPotion: 
                return charStats.Mana;

            default:
                return null;
        }
    }

}

public enum BuffType{
    HealthPotion,
    ManaPotion,
}

    
