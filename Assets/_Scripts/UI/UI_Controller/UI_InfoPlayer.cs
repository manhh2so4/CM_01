using SOArchitecture;
using TMPro;
using UnityEngine;

public class UI_InfoPlayer : MonoBehaviour {

    [SerializeField] TextMeshProUGUI Hp;
    [SerializeField] TextMeshProUGUI MP;
    [SerializeField] TextMeshProUGUI Armor;
    [SerializeField] TextMeshProUGUI Damage;
    [SerializeField] TextMeshProUGUI Crit;
    [SerializeField] TextMeshProUGUI CritDamage;
    [SerializeField] TextMeshProUGUI Level;

    [Header("Share Variables")]
    [SerializeField] IntVariable HpPlayer;
    [SerializeField] IntVariable MPPlayer;
    [SerializeField] IntVariable ArmorPlayer;
    [SerializeField] IntVariable DamagePlayer;
    [SerializeField] IntVariable CritPlayer;
    [SerializeField] IntVariable CritDamagePlayer;
    [SerializeField] IntVariable LevelPlayer;

    const float size = 35;

    void OnEnable(){
        HpPlayer.OnChange += UpdateHp;
        MPPlayer.OnChange += UpdateMp;
        ArmorPlayer.OnChange += UpdateArmor;
        DamagePlayer.OnChange += UpdateDamage;
        CritPlayer.OnChange += UpdateCrit;
        CritDamagePlayer.OnChange += UpdateCritDamage;

        LevelPlayer.OnChange += UpdateLevel;

        UpdateHp();
        UpdateMp();
        UpdateArmor();
        UpdateDamage();
        UpdateCrit();
        UpdateCritDamage();
        UpdateLevel();
    }

    void OnDisable(){
        HpPlayer.OnChange -= UpdateHp;
        MPPlayer.OnChange -= UpdateMp;
        ArmorPlayer.OnChange -= UpdateArmor;
        DamagePlayer.OnChange -= UpdateDamage;
        CritPlayer.OnChange -= UpdateCrit;
        CritDamagePlayer.OnChange -= UpdateCritDamage;
        LevelPlayer.OnChange -= UpdateLevel;
    }

    void UpdateHp(){
        Hp.text = $"HP: <size={size}><color=white>{HpPlayer.Value}</size>";
    }

    void UpdateMp(){
        MP.text = $"MP: <size={size}><color=white>{MPPlayer.Value}</color></size>";
    }
    void UpdateArmor(){
        Armor.text = $"Armor: <size={size}><color=white>{ArmorPlayer.Value}</color></size>";
    }
    void UpdateDamage(){
        Damage.text = $"Damage: <size={size}><color=white>{DamagePlayer.Value}</color></size>";
    }
    void UpdateCrit(){
        Crit.text = $"Crit: <size={size}><color=white>{CritPlayer.Value}%</color></size>";
    }
    void UpdateCritDamage(){
        CritDamage.text = $"Crit Damage: <size={size}><color=white>{CritDamagePlayer.Value}%</color></size>";
    }

    void UpdateLevel(){
        Level.text = $"Level: <size={size}>{LevelPlayer.Value}</size>";
    }
    
    

}