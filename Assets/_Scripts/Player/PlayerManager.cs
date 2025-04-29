using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : Singleton<PlayerManager>
{
    Core core;
    [SerializeField] Player player;
    [SerializeField] Transform NotifyBuffContrainer;
    Purse purse;
    Inventory inventory;
    Equipment equipment;
    InteracButton interacButton;
    CharacterStats charStats;
    BuffStat buffStat;
    LevelSystem levelSystem;
    SkillTreeManager skillTreeManager;


    protected override void Awake() {
        base.Awake();
        if(player == null) player = FindObjectOfType<Player>(); 

        purse = player.GetComponent<Purse>();   
        inventory = player.GetComponent<Inventory>();
        equipment = player.GetComponent<Equipment>();
        interacButton = player.GetComponent<InteracButton>();

        core = player.GetComponentInChildren<Core>();
        charStats = core.GetCoreComponent<CharacterStats>();
        levelSystem = core.GetCoreComponent<LevelSystem>();
        skillTreeManager = player.GetComponent<SkillTreeManager>();
        buffStat = core.GetCoreComponent<BuffStat>();
    }

    public static Player GetPlayer(){
        return Instance.player;
    }
    public static Core GetCore(){
        return Instance.core;
    }
    public static CharacterStats GetCharStats(){
        return Instance.charStats;
    }
    public static Purse GetPurse(){
        return Instance.purse;
    }
    public static Equipment GetEquipment(){
        return Instance.equipment;
    }
    public static Inventory GetInventory(){
        return Instance.inventory;
    }
    public static InteracButton GetInteracButton(){
        return Instance.interacButton;
    }
    
    public static LevelSystem GetLevelSystem(){
        return Instance.levelSystem;
    }
    public static SkillTreeManager GetSkillTree(){
        return Instance.skillTreeManager;
    }
    public static Transform GetNotifyBuffContrainer(){
        return Instance.NotifyBuffContrainer;
    }
    public static BuffStat GetBuffStat(){
        return Instance.buffStat;
    }
}
