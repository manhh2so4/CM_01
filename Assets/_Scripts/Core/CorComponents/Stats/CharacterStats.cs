using UnityEngine;


public class CharacterStats : CoreComponent{
    [Header("Major stats")]
    public Stat_char strength; // 1 point increase damage by 1 and crit.power by 1%
    public Stat_char agility;  // 1 point increase evasion by 1% and crit.chance by 1%
    public Stat_char intelligence; // 1 point increase magic damage by 1 and magic resistance by 3
    public Stat_char vitality; // 1 point incredase health by 3 or 5 points

    [Header("Offensive stats")]
    public Stat_char damage;
    public Stat_char critChance;
    public Stat_char critPower; 

    [Header("Defensive stats")]
    public Stat_char maxHealth;
    public Stat_char armor;
    public Stat_char evasion;
    public Stat_char magicResistance;

    public int currentHealth;

    [SerializeField] protected bool isDead;
    public System.Action onHealthChanged;
    public System.Action onHealthZero;
    protected override void Awake(){
        base.Awake();
        ResetMaxHealth();
        
    }
    public void ResetMaxHealth(){
        currentHealth = GetMaxHealthValue();
        isDead = false;
        onHealthChanged?.Invoke();
    }
    public virtual void DoDamage(CharacterStats _targetStats)
    {
        if (TargetCanAvoidAttack(_targetStats))
            return;

        int totalDamage = damage.GetValue() + strength.GetValue();

        if (CanCrit())
        {
            totalDamage = CalculateCriticalDamage(totalDamage);
        }

        totalDamage = CheckTargetArmor(_targetStats, totalDamage);
        _targetStats.TakeDamage(totalDamage);

    }
    public virtual void TakeDamage(int _damage)
    {
        Debug.Log(core.transform.parent.name +" take: " + _damage);
        DecreaseHealthBy(_damage);
    }
    protected virtual void DecreaseHealthBy(int _damage)
    {
        currentHealth -= _damage;
        onHealthChanged?.Invoke();
        if (currentHealth < 0 && !isDead)
            Die();
    }
    public virtual void IncreaseHealthBy(int _amount)
    {
        currentHealth += _amount;

        if (currentHealth > GetMaxHealthValue())
            currentHealth = GetMaxHealthValue();

        onHealthChanged?.Invoke();
    }
    protected virtual void Die()
    {
        isDead = true;
        onHealthZero?.Invoke();
    }

    #region Stat calculations
    private int CheckTargetArmor(CharacterStats _targetStats, int totalDamage)
    {
        totalDamage -= _targetStats.armor.GetValue();
        totalDamage = Mathf.Clamp(totalDamage, 0, int.MaxValue);
        return totalDamage;
    }
    private bool TargetCanAvoidAttack(CharacterStats _targetStats)
    {
        int totalEvasion = _targetStats.evasion.GetValue() + _targetStats.agility.GetValue();

        if (Random.Range(0, 100) < totalEvasion)
        {
            return true;
        }

        return false;
    }
    private bool CanCrit()
    {
        int totalCriticalChance = critChance.GetValue() + agility.GetValue();

        if (Random.Range(0, 100) <= totalCriticalChance)
        {
            return true;
        }


        return false;
    }
    private int CalculateCriticalDamage(int _damage)
    {
        float totalCritPower = (critPower.GetValue() + strength.GetValue()) * .01f;
        float critDamage = _damage * totalCritPower;

        return Mathf.RoundToInt(critDamage);
    }
    public int GetMaxHealthValue()
    {
        return maxHealth.GetValue() + vitality.GetValue() * 5;
    }
    public Stat_char GetStatOfType(StatType statType)
    {
        switch (statType)
        {
            case StatType.strength: return strength;
            case StatType.agility: return agility;
            case StatType.intelegence: return strength;
            case StatType.vitality: return vitality;
            case StatType.damage: return damage;
            case StatType.critChance: return critChance;
            case StatType.critPower: return critPower;
            case StatType.health: return maxHealth;
            case StatType.armor: return armor;
            case StatType.evasion: return evasion;
            case StatType.magicRes: return magicResistance;
        }
        return null;
    }
    #endregion

}
public enum StatType
{
    strength,
    agility,
    intelegence,
    vitality,
    damage,
    critChance,
    critPower,
    health,
    armor,
    evasion,
    magicRes,
}