using NaughtyAttributes;
using UnityEngine;


public class CharacterStats : CoreComponent{

    [Header("Offensive stats")]
    public Stat damage;
    public Stat critChance;
    public Stat critPower; 

    [Header("Defensive stats")]
    public Stat Health;
    public Stat armor;
    public Stat magicResistance;

    [SerializeField] protected bool isDead;

    public System.Action onHealthZero;
    void Start()
    {
        ResetMaxHealth();
    }
    public void ResetMaxHealth(){
        CalculationHP( GetMaxHealthValue() );
        isDead = false;

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
    [Button]
    void dame(){
        TakeDamage(100);
    }
    public virtual void TakeDamage(int _damage)
    {
        Debug.Log(core.transform.parent.name +" take: " + _damage);
        DecreaseHealthBy(_damage);
    }
    protected virtual void DecreaseHealthBy(int _damage)
    {
        CalculationHP( -_damage); 
    }
    public virtual void IncreaseHealthBy(int _amount)
    {
       CalculationHP(_amount);  
    }

    private void CalculationHP(int _amount)
    {
        Health.currentValue += _amount;
        //this.GameEvents().healthEvent.ChangeHP(_amount);
        if ( Health.currentValue > GetMaxHealthValue() ) Health.currentValue = GetMaxHealthValue();
        if ( Health.currentValue < 0 && !isDead ) Die();
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
        return Health.GetValue();
    }
    public Stat GetStatOfType(StatType statType)
    {
        switch (statType)
        {
            case StatType.damage: return damage;
            case StatType.critChance: return critChance;
            case StatType.critDame: return critPower;
            case StatType.health: return Health;
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