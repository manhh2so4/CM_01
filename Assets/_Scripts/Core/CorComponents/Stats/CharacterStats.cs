using UnityEngine;


public class CharacterStats : CoreComponent{

    [Header("Offensive stats")]
    public Stat damage;
    public Stat critChance;
    public Stat critPower; 

    [Header("Defensive stats")]
    public Stat maxHealth;
    public Stat armor;
    public Stat magicResistance;

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

        int totalDamage = damage.GetValue();

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
    public int GetMaxHealthValue()
    {
        return maxHealth.GetValue();
    }
    public Stat GetStatOfType(StatType statType)
    {
        switch (statType)
        {
            case StatType.damage: return damage;
            case StatType.critChance: return critChance;
            case StatType.critDame: return critPower;
            case StatType.health: return maxHealth;
            case StatType.armor: return armor;
            case StatType.magicRes: return magicResistance;
        }
        return null;
    }
    #endregion

}
public enum StatType
{
    damage,
    critChance,
    critDame,
    health,
    armor,
    magicRes,
}