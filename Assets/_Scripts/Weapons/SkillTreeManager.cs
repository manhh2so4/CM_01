using NaughtyAttributes;
using UnityEngine;
using TMPro;
using HStrong.Saving;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Diagnostics;


public class SkillTreeManager : MonoBehaviour,ISaveable
{
    public WeaponSkill SkillKiem;
    public WeaponSkill SkillKunai;
    public WeaponSkill SkillDao;
    public WeaponSkill SkillCung;
    public WeaponSkill SkillQuat;
    
    public SkillData[] PassiveSkills;
    //public int PointWeapon;
    public bool CanUpgradeSkill =>  currentWeaponSkill.pointWeapon > 0;
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
        LoadSkillActiveTreeUI();
        LoadSkillPassiveTreeUI();
        
    }
    void OnDisable()
    {
        UiSkillTooltip.OnUpgradeSkill -= UpgradeSkill;
    }

    public void UpdateSkillTreeUI(WeaponItemSO weapon){
        if(weapon != null){
            switch(weapon.GetWeaponType()){
                case WeaponType.Kunai:
                    currentWeaponSkill = SkillKunai;
                    break;
                case WeaponType.Kiem:
                    currentWeaponSkill = SkillKiem;
                    break;
                case WeaponType.Cung:
                    currentWeaponSkill = SkillCung;
                    break;
                case WeaponType.Dao:
                    currentWeaponSkill = SkillDao;
                    break;
                case WeaponType.Quat:
                    currentWeaponSkill = SkillQuat;
                    break;
                default:
                    currentWeaponSkill = null;
                    break;
            }
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
    [Button]
    public void ResetSkill(){
        foreach(SkillData skill in currentWeaponSkill.ActiveSkills){
            skill.ResetSkill();
        }
        foreach(SkillData skill in PassiveSkills){
            skill.ResetSkill();
        }
        currentWeaponSkill.pointWeapon = 20;
        UpdateUITree();
        LoadSkillActiveTreeUI();
        LoadSkillPassiveTreeUI();
    }

    void UpdateUITree()
    {
        if(currentWeaponSkill == null) {
            noWeaponText.SetActive(true);
            pointWeapon.text = "Không có vũ khí";
        }else{
            noWeaponText.SetActive(false);
            pointWeapon.text = $"Điểm kỹ năng : <color=green><size={45}>{ currentWeaponSkill.pointWeapon }";
        }
        
        
    }

    void SetSkillTooltip(SkillData skillData){
        CurrentSelectSkill = skillData;
        UiSkillTooltip.SetSkillData( skillData );
    }

    void UpgradeSkill(){
        currentWeaponSkill.pointWeapon --;
        CurrentSelectSkill.UpLvSkill();

        UpdateUITree();
        UiSkillTooltip.UpDateUI();

        LoadSkillActiveTreeUI();
        LoadSkillPassiveTreeUI();
    }

#region Save_Object
    [Serializable]
    class SkillWeaponRecord{
        public SkillRecord SkillKiem;
        public SkillRecord SkillKunai;
        public SkillRecord SkillDao;
        public SkillRecord SkillCung;
        public SkillRecord SkillQuat;
        public int[] levelSkillPassive;
        public SkillWeaponRecord(){
            SkillKiem = new SkillRecord();
            SkillKunai = new SkillRecord();
            SkillDao = new SkillRecord();
            SkillCung = new SkillRecord();
            SkillQuat = new SkillRecord();
        }
    }

    [Serializable]
    class SkillRecord{
        public int pointWeapon;
        public int[] slotsSkill;
        public int[] levelSkillActive;
    }

    public object CaptureState()
    {
        SkillWeaponRecord skillKiemRecord = new SkillWeaponRecord();

        skillKiemRecord.levelSkillPassive = new int[PassiveSkills.Length];
        skillKiemRecord.SkillKiem = SaveSkillRecord(SkillKiem);
        skillKiemRecord.SkillKunai = SaveSkillRecord(SkillKunai);
        skillKiemRecord.SkillDao = SaveSkillRecord(SkillDao);
        skillKiemRecord.SkillCung = SaveSkillRecord(SkillCung);
        skillKiemRecord.SkillQuat = SaveSkillRecord(SkillQuat);
        for(int i = 0; i < PassiveSkills.Length; i++){
            if(PassiveSkills[i].IsUnlock){
                skillKiemRecord.levelSkillPassive[i] = PassiveSkills[i].lvSkill;
            }else{
                skillKiemRecord.levelSkillPassive[i] = 0;
            }
        }
        return skillKiemRecord;
    }

    SkillRecord SaveSkillRecord(WeaponSkill _weaponSkill){
        
        SkillRecord skillRecord = new SkillRecord();
        skillRecord.pointWeapon = _weaponSkill.pointWeapon;

        skillRecord.slotsSkill = new int[3];
        for (int i = 0; i < 3; i++){
            skillRecord.slotsSkill[i] = _weaponSkill.slotsSkill[i];
        }

        skillRecord.levelSkillActive = new int[_weaponSkill.ActiveSkills.Length];
        for(int i = 0; i < _weaponSkill.ActiveSkills.Length; i++){
            if(_weaponSkill.ActiveSkills[i].IsUnlock){
                skillRecord.levelSkillActive[i] = _weaponSkill.ActiveSkills[i].lvSkill;
            }else{
                skillRecord.levelSkillActive[i] = 0;
            }
        }
        return skillRecord;
    }

    public void RestoreState(object state)
    {
        SkillWeaponRecord skillSaves = (SkillWeaponRecord)state;
        
        LoadSkillRecord(skillSaves.SkillKiem, SkillKiem);
        LoadSkillRecord(skillSaves.SkillKunai, SkillKunai);
        LoadSkillRecord(skillSaves.SkillDao, SkillDao);
        LoadSkillRecord(skillSaves.SkillCung, SkillCung);
        LoadSkillRecord(skillSaves.SkillQuat, SkillQuat);

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
        LoadSkillActiveTreeUI();
    }
    void LoadSkillRecord(SkillRecord _skillRecord, WeaponSkill _weaponSkill){
        _weaponSkill.pointWeapon = _skillRecord.pointWeapon;

        for(int i = 0; i < 3; i++){
            _weaponSkill.slotsSkill[i] = _skillRecord.slotsSkill[i];
        }
        for(int i = 0; i < _weaponSkill.ActiveSkills.Length; i++){
            if(_skillRecord.levelSkillActive[i] > 0){
                _weaponSkill.ActiveSkills[i].IsUnlock = true;
                _weaponSkill.ActiveSkills[i].lvSkill = _skillRecord.levelSkillActive[i];
            }else{
                _weaponSkill.ActiveSkills[i].IsUnlock = false;
                _weaponSkill.ActiveSkills[i].lvSkill = 1;
            }
        }
        
    }
#endregion
}

[Serializable]
public class WeaponSkill
{   
    public WeaponType weaponType;
    public int pointWeapon;
    public List<int> slotsSkill = new List<int>(3){0,-1,-1};
    public SkillData[] ActiveSkills;
}

