using System;
using System.Collections;
using UnityEngine;

public class BuffHealing : BaseBuffStat{
    Transform NotifyBuffContrainer => PlayerManager.GetNotifyBuffContrainer();
    UI_NotifyBuff notifyBuff;
    
    public override void StartBuff(int valuePerTick, float duration,Stat stat,Sprite icon = null){
        
        notifyBuff = PoolsContainer.GetObject(this.GetPrefab<UI_NotifyBuff>(),NotifyBuffContrainer);
        notifyBuff.SetData(icon);
        base.StartBuff(valuePerTick, duration, stat);
    }
        
    protected override void CountDown(float time){
        base.CountDown(time);
        string timeString = time.ToString("F1") + "s";
        notifyBuff.SetTime(timeString);
    }
    protected override void OnDisable(){
        base.OnDisable();
        notifyBuff.ReturnToPool();
    }

}