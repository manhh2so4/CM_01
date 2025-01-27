using UnityEngine;
public class ProjecttileData : ComponentData
{
    [field: SerializeField] public GameObject prefabProjectile;
    protected override void SetComponentDependency()
    {
        ComponentDependency = typeof(TargeterToProjectile);
    }
}