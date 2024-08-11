using UnityEngine;

public class ParticleManager : CoreComponent
{
    private Transform particleContainer;
    protected override void Awake()
    {
        base.Awake();
        particleContainer = GameObject.FindGameObjectWithTag("ParticleContainer").transform;
    }
    public GameObject StartParticles(GameObject particlePrefab, Vector2 position, Quaternion rotation){
        return Instantiate(particlePrefab, position,rotation, particleContainer);
    }
    public GameObject StartParticles(GameObject particlePrefab){
        return StartParticles(particlePrefab,transform.position,Quaternion.identity);
    }
    public GameObject StartParticlesRandomRotation(GameObject particlePrefab){
        var randRotation = Quaternion.Euler(0f,0f, Random.Range(0,360));
        return StartParticles(particlePrefab,transform.position,randRotation);
    }
}
