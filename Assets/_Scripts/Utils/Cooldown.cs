using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Cooldown{
    Dictionary<State, float> cooldownTimers = new Dictionary<State, float>();
    public void Update(){
        if(cooldownTimers.Count == 0) return;
        var keys = new List<State>(cooldownTimers.Keys);
        foreach (State ability in keys){
            cooldownTimers[ability] -= Time.deltaTime;
            
            if (cooldownTimers[ability] <= 0){
                cooldownTimers.Remove(ability);
            }
        }
    }
    public void Start(State ability, float time){
        cooldownTimers[ability] = time;
    }
    public float GetTime(State ability){
        if ( !cooldownTimers.ContainsKey(ability) ) return 0;
        return cooldownTimers[ability];
    }
    public bool IsDone(State ability){
        return !cooldownTimers.ContainsKey(ability);
    }
}