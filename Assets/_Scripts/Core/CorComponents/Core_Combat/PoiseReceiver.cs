using Unity.VisualScripting;
using UnityEngine;
public class PoiseReceiver : CoreReceiver, IPoiseDamageable
{   
    public Poisetype poisetype;
    public bool isPoise;
    public void DamagePoise(float amount,Poisetype type,GameObject prefabEff)
    {
        stats.Poise.IncreaseNonStack(amount);
        isPoise = true;
        poisetype = type;
        PrefabEff = prefabEff;
        PrefabEff.GetComponent<Effect_Instance>().SetData(amount,core.layerID,core.size);  
        SetPosEff();
        particleManager?.StartParticles(PrefabEff,this.transform.position + Location);
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