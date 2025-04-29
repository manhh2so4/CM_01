[System.Serializable]
public class SkillData {
    public int lvSkill = 1;
    public int lvMaxSkill => dataSO.lvMaxSkill;
    public int lvRequirement => dataSO.lvRequirement;
    public bool IsUnlock = false;
    public bool IsMaxLv => lvSkill >= lvMaxSkill ;
    public bool CanUpgrade => !IsMaxLv ;
    
    
    //public ModifiersUpgrade[] AddtiveModifiers => dataSO.AddtiveModifiers;
    //-------- Active Skill
    public int Range => (dataSO as SkillData_Active_SO).Range;
    public int ConsumeMana => (dataSO as SkillData_Active_SO).consumeMana[ lvSkill-1 ];
    public float Cooldown => (dataSO as SkillData_Active_SO).cooldown[ lvSkill-1 ];

    public SkillDataBase dataSO;

    public void UpLvSkill(){
        if(IsUnlock == false) {
            IsUnlock = true;
            return;
        }
        if( CanUpgrade && IsUnlock){
            lvSkill++;
        }
    }
}

