
using UnityEngine;
public class PoiseReceiver : CoreReceiver, IPoiseDamageable
{   

    [SerializeField] bool isPoise;
    [Range(0,1)]
    [SerializeField] float resistanceEffect = 1;
    float StartTime;
    float maxTime;
    public void DamagePoise(float time,Poisetype type,BaseEffect prefabEff)
    {
        if(prefabEff == null ) return;
        time = time * resistanceEffect;
        isPoise = true;

        BaseEffect effect = PoolsContainer.GetObject(prefabEff, this.transform.position + SetPosEff(prefabEff) ,transform);
        effect.SetData( core.SortingLayerID, core.uniqueID, time, (int)core.size.y);
        
        isPoise = true;
        StartTime = Time.time;
        maxTime = time;
    }
    public override void LogicUpdate(){
        CheckPoise();
    }
    private void CheckPoise()
    {
        if(isPoise){
            if( Time.time >= StartTime + maxTime)
            {
                isPoise = false;
            }   
        }
    }
    public bool IsPoise() => isPoise;
}