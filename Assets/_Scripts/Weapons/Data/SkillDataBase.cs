using UnityEngine;

[CreateAssetMenu(fileName = "SkillData", menuName = "Data/Skill Base", order = 0)]
public class SkillDataBase : ScriptableObject {
    [SpritePreview] public Sprite icon ;
    public int lvMaxSkill ;
    public string skillName ;
    public string description;
    public int lvRequirement ;
    public bool isActiveSkill;
    public ModifiersUpgrade[] AddtiveModifiers;  
}