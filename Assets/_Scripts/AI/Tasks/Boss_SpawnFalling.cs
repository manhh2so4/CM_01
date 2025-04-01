using BehaviorDesigner.Runtime.Tasks;
using DG.Tweening;
using UnityEngine;
public class Boss_SpawnFalling : Boss_Action {
    public Collider2D spawnAreaCollider;

    public int spawnCount = 4;
    public float spawnInterval = 0.3f;
    public override TaskStatus OnUpdate()
    {
        var sequence = DOTween.Sequence();
        for (int i = 0; i < spawnCount; i++)
        {
            sequence.AppendCallback(SpawnRock);
            sequence.AppendInterval(spawnInterval);
        }
        return TaskStatus.Success;
    }

    private void SpawnRock()
    {

    }

}