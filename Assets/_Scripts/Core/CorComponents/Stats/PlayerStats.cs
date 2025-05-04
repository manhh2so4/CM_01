using SOArchitecture;
using UnityEngine;

public class PlayerStats : CharacterStats {
    [SerializeField] IntVariable HpPlayer;
    [SerializeField] IntVariable MPPlayer;
    [SerializeField] IntVariable ArmorPlayer;
    [SerializeField] IntVariable DamagePlayer;
    [SerializeField] IntVariable CritPlayer;
    [SerializeField] IntVariable CritDamagePlayer;
    
    protected override void OnEnable()
    {
        base.OnEnable();
        Health.OnChangeValue += UpdateHp;
        Mana.OnChangeValue += UpdateMp;
        armor.OnChangeValue += UpdateArmor;    
        damage.OnChangeValue += UpdateDamage;
        critChance.OnChangeValue += UpdateCrit;
        critPower.OnChangeValue += UpdateCritDamage;
    }
    protected override void OnDisable()
    {
        base.OnDisable();
        Health.OnChangeValue -= UpdateHp;
        Mana.OnChangeValue -= UpdateMp;
        armor.OnChangeValue -= UpdateArmor;    
        damage.OnChangeValue -= UpdateDamage;
        critChance.OnChangeValue -= UpdateCrit;
        critPower.OnChangeValue -= UpdateCritDamage;
    }
    protected override void Start()
    {
        base.Start();
        UpdateHp();
        UpdateMp();
        UpdateArmor();
        UpdateDamage();
        UpdateCrit();
        UpdateCritDamage();
    }

    void UpdateHp(){
        HpPlayer.Value = Health.GetValue();
    }
    void UpdateMp(){
        MPPlayer.Value = Mana.GetValue();
    }
    void UpdateArmor(){
        ArmorPlayer.Value = armor.GetValue();
    }
    void UpdateDamage(){
        DamagePlayer.Value = damage.GetValue();
    }
    void UpdateCrit(){
        CritPlayer.Value = critChance.GetValue();
    }   
    void UpdateCritDamage(){
        CritDamagePlayer.Value = critPower.GetValue();
    }

}