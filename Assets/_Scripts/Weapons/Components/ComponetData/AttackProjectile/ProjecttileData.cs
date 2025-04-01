using HStrong.ProjectileSystem;
using UnityEngine;
public class ProjecttileData : ComponentData
{
    [field: SerializeField] public Projectile prefabProjectile;
    protected override void SetComponentDependency()
    {
        ComponentDependency = typeof(TargeterToProjectile);
    }
}