using NaughtyAttributes;
using UnityEngine;
using TMPro;
using HStrong.Saving;
using System.Collections.Generic;
using System.Linq;
using System;


public class SkillTreeManager : MonoBehaviour,ISaveable
{
    public WeaponSkill SkillKiem;
    public WeaponSkill SkillKunai;
    public WeaponSkill SkillDao;
    public SkillData[] PassiveSkills;
    public int PointWeapon;
    public bool CanUpgradeSkill => PointWeapon > 0;
#region UI
    [SerializeField] Transform ActiveSkillsContainer;
    [SerializeField] Transform PassiveSkillsContainer;
    [SerializeField] UI_SlotSkill SlotSkillPrefab;
    [SerializeField] UI_SkillTooltip UiSkillTooltip;
    [SerializeField] TextMeshProUGUI pointWeapon;
    [SerializeField] GameObject noWeaponText;

#endregion 

#region Logic
    [SerializeField] WeaponGenerator Slot1;
    [SerializeField] WeaponGenerator Slot2;
    [SerializeField] WeaponGenerator Slot3;
    //-------- Currrent Skill
    public WeaponSkill currentWeaponSkill = null;
    public SkillData CurrentSelectSkill ;
#endregion
    void OnEnable()
    {
        UiSkillTooltip.OnUpgradeSkill += UpgradeSkill;
        currentWeaponSkill = null;
        
    }
    void OnDisable()
    {
        UiSkillTooltip.OnUpgradeSkill -= UpgradeSkill;
    }

    public void UpdateSkillTreeUI(EquipableItemSO weapon){
        if(weapon != null){
            currentWeaponSkill = SkillKiem;
        }else{
            currentWeaponSkill = null;
        }
        LoadSkillActiveTreeUI();
    }
    void LoadSkillPassiveTreeUI(){      

        PassiveSkillsContainer.RemoveAllChild();
        foreach(SkillData skill in PassiveSkills){
            UI_SlotSkill slotSkill = Instantiate(SlotSkillPrefab, PassiveSkillsContainer);
            slotSkill.SetSkill( skill.dataSO.icon , () => SetSkillTooltip( skill ) , skill.IsUnlock );
        }
    }

    void LoadSkillActiveTreeUI(){

        ActiveSkillsContainer.RemoveAllChild();
        UpdateUITree();
        LoadSlotSkill();

        if(currentWeaponSkill == null) return;

        foreach(SkillData skill in currentWeaponSkill.ActiveSkills){
            UI_SlotSkill slotSkill = Instantiate(SlotSkillPrefab, ActiveSkillsContainer);
            slotSkill.SetSkill( skill.dataSO.icon , () => SetSkillTooltip( skill ) , skill.IsUnlock );
        }
        
    }

    void LoadSlotSkill(){
        if(currentWeaponSkill == null) {
            Slot1.GenerateWeapon(null);
            Slot2.GenerateWeapon(null);
            Slot3.GenerateWeapon(null);

        }else{
            if( currentWeaponSkill.slotsSkill[0] >= 0) Slot1.GenerateWeapon( currentWeaponSkill.ActiveSkills[ currentWeaponSkill.slotsSkill[0] ] );
            if( currentWeaponSkill.slotsSkill[1] >= 0) Slot2.GenerateWeapon( currentWeaponSkill.ActiveSkills[ currentWeaponSkill.slotsSkill[1] ] );
            if( currentWeaponSkill.slotsSkill[2] >= 0) Slot3.GenerateWeapon( currentWeaponSkill.ActiveSkills[ currentWeaponSkill.slotsSkill[2] ] );   
        }
        
    }

    public bool TrySetSlotSkill(int slot, SkillData skillData){
        int index = Array.IndexOf(currentWeaponSkill.ActiveSkills, skillData);

        if(index == -1) return false;
        if( currentWeaponSkill.slotsSkill.Contains(index) ) return false;

        switch(slot){
            case 1:
                Slot1.GenerateWeapon( skillData );
                break;
            case 2:
                Slot2.GenerateWeapon( skillData );
                break;
            case 3:
                Slot3.GenerateWeapon( skillData );
                break;
        }
        currentWeaponSkill.slotsSkill[slot - 1] = index;
        return true;

    }
    public void ToggleSetSkillSlot(){
        UiSkillTooltip.ToggleSetSkillSlot();
    }

    void UpdateUITree()
    {
        if(currentWeaponSkill == null) {
            noWeaponText.SetActive(true);
        }else{
            noWeaponText.SetActive(false);
        }
        pointWeapon.text = $"Điểm kỹ năng : <color=green><size={45}>{ PointWeapon }";
    }

    void SetSkillTooltip(SkillData skillData){
        CurrentSelectSkill = skillData;
        UiSkillTooltip.SetSkillData( skillData );
    }

    void UpgradeSkill(){
        PointWeapon --;
        CurrentSelectSkill.UpLvSkill();

        UpdateUITree();
        UiSkillTooltip.UpDateUI();

        LoadSkillActiveTreeUI();
        LoadSkillPassiveTreeUI();
    }

#region Save_Object
    [System.Serializable]
    class SkillKiemRecord{
        public int pointWeapon;
        public int[] slotsSkill;
        public int[] levelSkillActive;
        public int[] levelSkillPassive;

    }
    
    public object CaptureState()
    {
        SkillKiemRecord skillKiemRecord = new SkillKiemRecord();

        skillKiemRecord.pointWeapon = PointWeapon;
        skillKiemRecord.slotsSkill = new int[3];
        skillKiemRecord.levelSkillActive = new int[SkillKiem.ActiveSkills.Length];
        skillKiemRecord.levelSkillPassive = new int[PassiveSkills.Length];

        for (int i = 0; i < 3; i++){
            skillKiemRecord.slotsSkill[i] = SkillKiem.slotsSkill[i];
        }
        for(int i = 0; i < SkillKiem.ActiveSkills.Length; i++){
            if(SkillKiem.ActiveSkills[i].IsUnlock){
                skillKiemRecord.levelSkillActive[i] = SkillKiem.ActiveSkills[i].lvSkill;
            }else{
                skillKiemRecord.levelSkillActive[i] = 0;
            }
        }
        for(int i = 0; i < PassiveSkills.Length; i++){
            if(PassiveSkills[i].IsUnlock){
                skillKiemRecord.levelSkillPassive[i] = PassiveSkills[i].lvSkill;
            }else{
                skillKiemRecord.levelSkillPassive[i] = 0;
            }
        }
        return skillKiemRecord;
    }

    public void RestoreState(object state)
    {
        SkillKiemRecord skillSaves = (SkillKiemRecord)state;
        PointWeapon = skillSaves.pointWeapon;
        
        for(int i = 0; i < 3; i++){
            SkillKiem.slotsSkill[i] = skillSaves.slotsSkill[i];
        }

        for(int i = 0; i < SkillKiem.ActiveSkills.Length; i++){
            if(skillSaves.levelSkillActive[i] > 0){
                SkillKiem.ActiveSkills[i].IsUnlock = true;
                SkillKiem.ActiveSkills[i].lvSkill = skillSaves.levelSkillActive[i];
            }else{
                SkillKiem.ActiveSkills[i].IsUnlock = false;
                SkillKiem.ActiveSkills[i].lvSkill = 1;
            }
        }

        for(int i = 0; i < PassiveSkills.Length; i++){
            if(skillSaves.levelSkillPassive[i] > 0){
                PassiveSkills[i].IsUnlock = true;
                PassiveSkills[i].lvSkill = skillSaves.levelSkillPassive[i];
            }else{
                PassiveSkills[i].IsUnlock = false;
                PassiveSkills[i].lvSkill = 1;
            }
        }
        UpdateUITree();
        LoadSlotSkill();
        LoadSkillPassiveTreeUI();
    }
#endregion
}
[System.Serializable]
public class WeaponSkill
{   
    public WeaponType weaponType;
    public List<int> slotsSkill = new List<int>(3){0,-1,-1};
    public SkillData[] ActiveSkills;
}