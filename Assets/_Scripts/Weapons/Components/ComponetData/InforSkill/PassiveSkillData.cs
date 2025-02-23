using UnityEngine;
public class PassiveSkillData : ComponentData
{
    [field: SerializeField] public int idSkill { get; private set; }
    [field: SerializeField] public float speed { get; private set; }
    protected override void SetComponentDependency()
    {
        ComponentDependency = typeof(WeaponComponents);
    }
}