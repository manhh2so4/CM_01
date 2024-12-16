using Unity.VisualScripting;
using UnityEngine;
public class PoiseReceiver : CoreReceiver, IPoiseDamageable
{   
    public Poisetype poisetype;
    public bool isPoise;
    public void DamagePoise(float time,Poisetype type,GameObject prefabEff)
    {
        stats.Poise.IncreaseNonStack(time);
        isPoise = true;
        poisetype = type;
        prefabEff?.GetComponent<Effect_Instance>().SetData(time,core.layerID,core.size);  
        particleManager?.StartParticles(prefabEff,this.transform.position + SetPosEff(prefabEff));
        Location = Vector3.zero;
    }
    public bool IsPoise() => isPoise;
    private void CheckPoise(){
        isPoise = false;
    }
    private void OnEnable()
    {
        stats.Poise.OnCurrentValueZero += CheckPoise;
    }
    private void OnDisable()
    {
        stats.Poise.OnCurrentValueZero -= CheckPoise;
    }
    
}