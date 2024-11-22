using UnityEngine;

public class ParticleManager : CoreComponent
{
    protected override void Awake()
    {
        base.Awake();
        //particleContainer = GameObject.FindGameObjectWithTag("ParticleContainer").transform;
    }
    public UnityEngine.GameObject StartParticles(UnityEngine.GameObject particlePrefab,Vector2 position, Quaternion rotation){
        return Instantiate(particlePrefab, position, rotation, transform);
    }
    public UnityEngine.GameObject StartParticles(UnityEngine.GameObject particlePrefab,Vector2 position){
        return StartParticles(particlePrefab,position,Quaternion.identity);
    }

    public UnityEngine.GameObject StartParticlesRandomRotation(UnityEngine.GameObject particlePrefab){
        var randRotation = Quaternion.Euler(0f,0f, Random.Range(0,360));
        return StartParticles(particlePrefab,transform.position,randRotation);
    }
    public UnityEngine.GameObject StartParticlesRandomRotation(UnityEngine.GameObject particlePrefab,Vector2 position){
        var randRotation = Quaternion.Euler(0f,0f, Random.Range(0,360));
        return StartParticles(particlePrefab, position, randRotation);
    }
}
