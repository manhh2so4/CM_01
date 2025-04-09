using UnityEngine;

public class PaintEffect : CoreComponent
{
    protected override void Awake()
    {
        base.Awake();
    }
    public T Creat<T>(T particlePrefab,Vector2 position, Quaternion rotation = default) where T : Component
    {
        T vale = PoolsContainer.GetObject(particlePrefab, position, transform);
        vale.transform.rotation = rotation;
        return vale;
    }

    public T CreatRandomRotation<T>(T particlePrefab) where T : Component
    {
        var randRotation = Quaternion.Euler(0f,0f, Random.Range(0,360));
        return Creat(particlePrefab,transform.position,randRotation);
    }
    public T CreatRandomRotation<T>(T particlePrefab,Vector2 position) where T : Component
    {
        var randRotation = Quaternion.Euler(0f,0f, Random.Range(0,360));
        return Creat(particlePrefab, position, randRotation);
    }
}
