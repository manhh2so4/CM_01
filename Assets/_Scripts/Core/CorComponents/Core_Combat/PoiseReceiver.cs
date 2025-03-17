
using UnityEngine;
public class PoiseReceiver : CoreReceiver, IPoiseDamageable
{   

    public bool isPoise;
    public void DamagePoise(float time,Poisetype type,GameObject prefabEff)
    {
        if(prefabEff == null ) return; 
        isPoise = true;
        prefabEff?.GetComponent<Effect_Instance>().SetData(time, core.SortingLayerID, core.size);         
        particleManager?.StartParticles(prefabEff,this.transform.position + SetPosEff(prefabEff));
        Location = Vector3.zero;
    }
    public bool IsPoise() => isPoise;
    private void CheckPoise(){
        isPoise = false;
    }
    
}