using NaughtyAttributes;
using UnityEngine;
using SOArchitecture;
using HStrong.Saving;

public class CharacterStats : CoreComponent{

    [Header("Offensive stats")]
    public Stat damage;
    public Stat critChance;
    public Stat critPower; 

    [Header("Defensive stats")]
    public Stat Health;
    public Stat Mana;
    public Stat armor;

    [Header("Defensive stats")]
    [SerializeField] protected bool isDead;
    public event System.Action onChangeHP;
    public System.Action OnDie;
    Movement movement;
    LevelSystem levelSystem;
    [Header("Share Data")]
    [SerializeField] StatVariable ShareHealth;
    [SerializeField] StatVariable ShareMana;

    protected override void OnEnable()
    {
        base.OnEnable();
        Health.OnChangeValue += ChangeHP;
    }
    protected override void OnDisable()
    {
        base.OnDisable();
        Health.OnChangeValue -= ChangeHP;
    }

    protected override void Start()
    {
        movement = core.GetCoreComponent<Movement>();
        levelSystem = core.GetCoreComponent<LevelSystem>();
        ResetMaxHealth();
    }
    public void ResetMaxHealth(){
        Health.CurrentValue = Health.GetValue() ;
        isDead = false;
    }

    public void DoDamage(CharacterStats _targetStats)
    {
        if (TargetCanAvoidAttack(_targetStats))
            return;

        int totalDamage = Random.Range( (int)(damage.GetValue()*.9f), damage.GetValue());

        if (CanCrit())
        {
            totalDamage = CalculateCriticalDamage(totalDamage);
        }

        totalDamage = CheckTargetArmor(_targetStats, totalDamage);

        levelSystem?.AddExperience(totalDamage);

        _targetStats.TakeDamage(totalDamage,CanCrit());

    }
    public void DoDamage( int _damage ){
        _damage -= armor.GetValue();
        _damage = Mathf.Clamp(_damage, 1, int.MaxValue);
        TakeDamage( _damage);
    }
    [Button]
    void dame(){
        //TakeDamage(100);
    }
    
    public void TakeDamage(int _damage , bool isCrit = false)
    {
        PopupText textPopup = PoolsContainer.GetObject( this.GetPrefab<PopupText>(), this.transform.position );
        textPopup.Setup( -_damage, isCrit ? PopupTextType.CritDamage : PopupTextType.Damage , movement?.facingDirection ?? 0 );
        Health.CurrentValue -= _damage;
    }

    void ChangeHP()
    {
        if(ShareHealth != null) ShareHealth.Value = new StatValue( Health.CurrentValue, Health.GetValue() );
        onChangeHP?.Invoke();
        if( Health.CurrentValue <= 0 && !isDead ) Die();
    }
    void ChangeMana(int _amount)
    {
        if(ShareMana != null) ShareMana.Value = new StatValue( Mana.CurrentValue, Mana.GetValue() );
    }
    protected virtual void Die()
    {
        isDead = true;
        OnDie?.Invoke();
    }

#region Stat calculations
    private int CheckTargetArmor(CharacterStats _targetStats, int totalDamage)
    {
        totalDamage -= _targetStats.armor.GetValue();
        totalDamage = Mathf.Clamp(totalDamage, 1, int.MaxValue);
        return totalDamage;
    }
    
    private bool TargetCanAvoidAttack(CharacterStats _targetStats)
    {
        int totalEvasion = 0;

        if (Random.Range(0, 100) < totalEvasion)
        {
            return true;
        }

        return false;
    }
    private bool CanCrit()
    {
        int totalCriticalChance = critChance.GetValue() ;

        if (Random.Range(0, 100) <= totalCriticalChance)
        {
            return true;
        }


        return false;
    }
    private int CalculateCriticalDamage(int _damage)
    {
        float totalCritPower = (critPower.GetValue()) *.01f;
        float critDamage = _damage * totalCritPower;
        return Mathf.RoundToInt(critDamage);
    }

    public Stat GetStatOfType(StatType statType)
    {
        switch (statType)
        {
            case StatType.damage: return damage;
            case StatType.critChance: return critChance;
            case StatType.critDamage: return critPower;
            case StatType.health: return Health;
            case StatType.mana: return Mana;
            case StatType.armor: return armor;
        }
        return null;
    }
    public void AddModifier(StatType statType, int _modifier)
    {
        GetStatOfType(statType)?.AddModifier(_modifier);
    }
    public void RemoveModifier(StatType statType, int _modifier)
    {
        GetStatOfType(statType)?.RemoveModifier(_modifier);
    }

    
    #endregion

    #region BuffEffect
    
    // public void AddBuff(BuffType buffType, int valuePerTick, float duration){
    //     switch(buffType){
    //         case BuffType.Healing:
    //             BuffHealing healing = this.ReplaceComponent<BuffHealing>();
    //             healing.StartBuff(valuePerTick, duration,Health);
    //             break;
    //     }
    // }

    // [Button]
    // void SetBuff(){
    //     AddBuff(BuffType.Healing, 1, 10);
    // }

    #endregion
}

public enum StatType
{
    //-----------
    damage,
    damagePercent,
    critChance,
    critDamage,
    //-----------
    cooldown,
    fireDame,
    iceDame,
    electricDame,

    //-----------
    health,
    healthPercent,
    mana,
    manaPercent,
    armor,
    //-----------
    ExpPercent,
    SpeedPercent,

    BurnTime,
    FreezeTime,
    ShockTime,
    //-----------
}

[System.Serializable]
public struct Modifiers{
    public StatType statType;
    public int _value;
}
[System.Serializable]
public struct ModifiersUpgrade{
    public StatType statType;
    public int[] _value;
}