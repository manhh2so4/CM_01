using UnityEngine;
public class MovementData : ComponentData
{
    [field: SerializeField] public Vector2 Direction { get; private set; }
    [field: SerializeField] public float Velocity { get; private set; }
    protected override void SetComponentDependency()
    {
        ComponentDependency = typeof(WeaponMovement);
    }
}