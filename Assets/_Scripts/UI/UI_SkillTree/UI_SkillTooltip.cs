using UnityEngine;
using TMPro;
using NaughtyAttributes;
using UnityEngine.UI;
using System;

public class UI_SkillTooltip : MonoBehaviour {
    [SerializeField] GameObject GOtooltip;
    [SerializeField] TextMeshProUGUI NameSkill;
    [SerializeField] TextMeshProUGUI DescriptionSkill;
    [SerializeField] TextMeshProUGUI LvRequirement;
    [SerializeField] RectTransform SkillInfoContainer;
    //-----------------------------
    [Header("---------------")]
    [SerializeField] UI_TextStat Range;
    [SerializeField] UI_TextStat ConsumeMana;
    [SerializeField] UI_TextStat Cooldown;

    //-----------------------------
    [SerializeField] UI_TextStat uiTextTooltipPrefab;
    [SerializeField] SkillData skillData;
    //-----------------------------
    [Header("Button ----------------")]
    [SerializeField] Button setSkillSlot;
    [SerializeField] UI_SetSkillButton uiSetSkillSlot;
    [SerializeField] Button UpgradeSkill;
    [SerializeField] public Action OnUpgradeSkill;

    void UpdateButton()
    {
        
        if(skillData.dataSO.isActiveSkill){

            setSkillSlot.gameObject.SetActive(true);

        }else{
            setSkillSlot.gameObject.SetActive(false);
        }
        
        if( skillData.IsMaxLv ){
            UpgradeSkill.interactable = false;
            UpgradeSkill.GetComponentInChildren<TextMeshProUGUI>().text = "<color=green>Max";
        }else if( !PlayerManager.GetSkillTree().CanUpgradeSkill ){
            UpgradeSkill.interactable = false;
            UpgradeSkill.GetComponentInChildren<TextMeshProUGUI>().text = "<color=grey>Không đủ điểm";
        }else if( !skillData.IsUnlock ){
            UpgradeSkill.interactable = true;
            UpgradeSkill.GetComponentInChildren<TextMeshProUGUI>().text = "Học";
        }
        else{
            UpgradeSkill.interactable = true;
            UpgradeSkill.GetComponentInChildren<TextMeshProUGUI>().text = "Nâng cấp";
        }

    }
    void OnEnable()
    {
        setSkillSlot.onClick.AddListener( ToggleSetSkillSlot );
        UpgradeSkill.onClick.AddListener( () => OnUpgradeSkill?.Invoke() );
        ShowHideUI(false);
    }
    void OnDisable()
    {
        setSkillSlot.onClick.RemoveAllListeners();
        UpgradeSkill.onClick.RemoveAllListeners();
    }
    
    public void ToggleSetSkillSlot(){

        bool isActive = uiSetSkillSlot.gameObject.activeSelf;
        uiSetSkillSlot.gameObject.SetActive( !isActive) ;

        if( uiSetSkillSlot.gameObject.activeSelf){
            setSkillSlot.GetComponentInChildren<TextMeshProUGUI>().text = "Đóng";
        }else{
            setSkillSlot.GetComponentInChildren<TextMeshProUGUI>().text = "Set Skill";
        }
    }
    void ShowHideUI(bool isHide){
        GOtooltip.SetActive(isHide);
        setSkillSlot.gameObject.SetActive(isHide);
        UpgradeSkill.gameObject.SetActive(isHide);
    }


    public void SetSkillData(SkillData _skillData){
        skillData = _skillData;
        UpDateUI();
    }

    public void UpDateUI(){

        ShowHideUI(true);

        UpdateButton();

        NameSkill.text = skillData.dataSO.skillName + "  +" + skillData.lvSkill.ToString();
        DescriptionSkill.text = skillData.dataSO.description;
        int _size = 40;
        LvRequirement.text = "Yêu cầu cấp độ : " + skillData.lvRequirement.ToString();

        if(skillData.dataSO.isActiveSkill){
            Range.SetTextUI( $"Phạm vi : <size={_size}>{ skillData.Range }</size>" , Color.white, "");
            ConsumeMana.SetTextUI( $"Tiêu hao mana : <size={_size}>{ skillData.ConsumeMana }</size>" , Color.white, "");
            Cooldown.SetTextUI( $"Hồi chiêu : <size={_size}>{ skillData.Cooldown }</size>s" , Color.white, "");
        }else{
            Range.gameObject.SetActive(false);
            ConsumeMana.gameObject.SetActive(false);
            Cooldown.gameObject.SetActive(false);
        }

        SkillInfoContainer.RemoveAllChild();
        
        foreach (var item in skillData.dataSO.AddtiveModifiers)
        {
            
            int value = item._value[ skillData.lvSkill-1 ];
            string upgradeText = "";
            if(value == 0) continue;
            if(skillData.CanUpgrade){
                upgradeText = $" --> { item._value[skillData.lvSkill] }";
            } 

            UI_TextStat uiTextTooltip = Instantiate( uiTextTooltipPrefab, SkillInfoContainer);
            uiTextTooltip.SetTextUI( StaticValue.GetNameStat(item.statType, value), StaticValue.GetColorStat(item.statType), upgradeText);
        }

        LayoutRebuilder.ForceRebuildLayoutImmediate(SkillInfoContainer);
        LayoutRebuilder.ForceRebuildLayoutImmediate(SkillInfoContainer.parent.GetComponent<RectTransform>());

    }


}