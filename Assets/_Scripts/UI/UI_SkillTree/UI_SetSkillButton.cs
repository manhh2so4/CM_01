using System;
using UnityEngine;
using UnityEngine.UI;

public class UI_SetSkillButton : MonoBehaviour {
    [SerializeField] WeaponGenerator Slot1;
    [SerializeField] WeaponGenerator Slot2;
    [SerializeField] WeaponGenerator Slot3;
    [SerializeField] Button button1;
    [SerializeField] Button button2;
    [SerializeField] Button button3;
    SkillTreeManager skillTreeManager;

    public void Start(){
        skillTreeManager = PlayerManager.GetSkillTree();
        button1.onClick.RemoveAllListeners();
        button2.onClick.RemoveAllListeners();
        button3.onClick.RemoveAllListeners();
        
        button1.onClick.AddListener( () => SetSlot(1) );
        button2.onClick.AddListener( () => SetSlot(2) );
        button3.onClick.AddListener( () => SetSlot(3) );
    }
    void SetSlot(int index){
        bool success = skillTreeManager.TrySetSlotSkill( index, skillTreeManager.CurrentSelectSkill );
        if(success) skillTreeManager.ToggleSetSkillSlot();
        else Debug.Log("Failed to set slot");
    }

}