
using System.Collections.Generic;
using System.Linq;
using Cathei.BakingSheet;
using Cathei.BakingSheet.Unity;
using UnityEditor;
using UnityEngine;

public class SkillSheet : Sheet<SkillSheet.Row>
{
    public class Row : SheetRowArray<Elem>
    {
        public string skillName { get; set; }
        public string description { get; set; }
        public int lvRequirement { get; set; }
        public int Range { get; set; }
        public int Icon { get; set; }
        public int MaxLevel => Count;

    }
    public class Elem : SheetRowElem
    {
        public int consumeMana { get; set; }
        public float cooldown { get; set; }
        public Dictionary<StatType, int> Stat { get; set; }
    }
}
public class SkillPassiveSheet : Sheet<SkillPassiveSheet.Row>
{
    public class Row : SheetRowArray<Elem>
    {
        public string skillName { get; set; }
        public string description { get; set; }
        public int lvRequirement { get; set; }
        public int Icon { get; set; }

    }
    public class Elem : SheetRowElem
    {
        public StatType statType { get; private set; }
        public List<int> value { get; private set; }
    }
}
[System.Serializable]
public class SheetSkillContainer : BaseSheetContainer{
    // public SkillSheet Kiem { get; set; }
    // public SkillSheet Kunai { get; set; }
    // public SkillSheet Dao { get; set; }
    
    public SkillSheet Quat { get; set; }
    public SkillSheet Cung { get; set; }

    //public SkillPassiveSheet SkillPassive { get; set; }
    public override void BakeData(){
        //BakeActiveSkill(Kunai, "Kunai");
        //BakeActiveSkill(Dao, "Dao");
        //BakeActiveSkill(Kiem, "Kiem");
        //BakeActiveSkill(Quat, "Quat");
        BakeActiveSkill(Cung, "Cung");

        //BakePassiveSkill(SkillPassive);
    }
    void BakeActiveSkill(SkillSheet skillSheet,string nameSkill){
        string assetPath = "Assets/Data/Data_skill/"+ nameSkill +"/";
  
        foreach( var skill in skillSheet ){   
            string namePAth = assetPath + skill.Id + ".asset";
            SkillData_Active_SO skillSO = AssetDatabase.LoadAssetAtPath<SkillData_Active_SO>( namePAth );
            if (skillSO == null)
            {
                skillSO = ScriptableObject.CreateInstance<SkillData_Active_SO>();
                AssetDatabase.CreateAsset(skillSO, namePAth);
            }
            skillSO.skillName = skill.skillName;
            skillSO.description = skill.description;
            skillSO.lvRequirement = skill.lvRequirement;
            skillSO.lvMaxSkill = skill.MaxLevel;
            skillSO.Range = skill.Range;
            skillSO.cooldown = new float[skill.MaxLevel];
            skillSO.consumeMana = new int[skill.MaxLevel];
            if(GetSpritesID.Get().ContainsKey(skill.Icon)){
                skillSO.icon  = GetSpritesID.Get()[skill.Icon];
            }
            int countStat = skill[0].Stat.Count;
            skillSO.AddtiveModifiers = new ModifiersUpgrade[countStat];

            for(int i = 0; i < countStat; i++){
                skillSO.AddtiveModifiers[i].statType = skill[0].Stat.Keys.ToArray()[i];
                skillSO.AddtiveModifiers[i]._value = new int[skill.MaxLevel];
            }

            for(int i = 0; i < skill.MaxLevel; i++){

                SkillSheet.Elem skillElem = skill.Arr[i];
                skillSO.consumeMana[i] = skillElem.consumeMana;
                skillSO.cooldown[i] = skillElem.cooldown;
                
                for(int j = 0; j < countStat; j++){
                    skillSO.AddtiveModifiers[j]._value[i] = skillElem.Stat.Values.ToArray()[j];
                }
            }
            EditorUtility.SetDirty(skillSO);
        }
    }
    void BakePassiveSkill(SkillPassiveSheet skillPassive){
        string assetPath = "Assets/Data/Data_skill/PassiveSkill/";
        foreach( var skill in skillPassive ){  
            string namePAth = assetPath + skill.Id + ".asset";
            SkillDataBase skillSO = AssetDatabase.LoadAssetAtPath<SkillDataBase>( namePAth );
            if (skillSO == null)
            {
                skillSO = ScriptableObject.CreateInstance<SkillDataBase>();
                AssetDatabase.CreateAsset(skillSO, namePAth);
            }
            skillSO.skillName = skill.skillName;
            skillSO.description = skill.description;
            skillSO.lvRequirement = skill.lvRequirement;
            skillSO.lvMaxSkill = skill[0].value.Count;
            skillSO.icon = GetSpritesID.Get()[skill.Icon];


            int Count = skill.Count;

            ModifiersUpgrade[] AddtiveModifiers = new ModifiersUpgrade[Count];
            for(int i = 0; i < Count; i++){
                AddtiveModifiers[i] = new ModifiersUpgrade{ statType = skill[i].statType, _value = skill[i].value.ToArray()};
            }
            skillSO.AddtiveModifiers = AddtiveModifiers;
            EditorUtility.SetDirty(skillSO);
        }
    }
}