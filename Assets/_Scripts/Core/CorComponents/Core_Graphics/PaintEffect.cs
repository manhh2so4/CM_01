using UnityEngine;

public class PaintEffect : CoreComponent
{
    [SerializeField] Movement movement;
    protected override void Awake()
    {
        base.Awake();
        movement = core.GetCoreComponent<Movement>();
    }
    protected override void OnEnable(){
        base.OnEnable();
        movement.OnFlip += FlipUI;   
    }
    protected override void OnDisable(){
        base.OnDisable();
        movement.OnFlip -= FlipUI;   
    }

    void FlipUI() => transform.Rotate(0, 180, 0);

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
