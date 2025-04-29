using System;
using NaughtyAttributes;
using UnityEngine;

public class LevelSystem : CoreComponent {
    [Header("Level Settings")]
    public int currentLevel = 1;
    public int currentXP = 0;
    public int xpToNextLevel = int.MaxValue;
    public event Action OnLevelUp;
    public event Action OnXpChange;
    CharacterStats charStats;
    PaintEffect paintEffect;
    
    [SerializeField] BaseEffect effLvUp;
    protected override void Awake()
    {
        base.Awake();
        charStats = core.GetCoreComponent<CharacterStats>();
        paintEffect = core.GetCoreComponent<PaintEffect>();
    }
    protected override void Start()
    {
        base.Start();
        xpToNextLevel = StaticValue.C_EXP_REQUIRE[ currentLevel - 1];
        OnXpChange?.Invoke();
        CalculateStats();
    }
    public void AddExperience(int xpAmount)
    {
        currentXP += xpAmount;
        PoolsContainer.GetObject( this.GetPrefab<PopupText>(), transform.position + Top).Setup(xpAmount, PopupTextType.Exp );
        
        while (currentXP >= xpToNextLevel )
        {
            currentXP -= xpToNextLevel;
            currentLevel++;
            
            xpToNextLevel = StaticValue.C_EXP_REQUIRE[ currentLevel - 1];
            
            CalculateStats();
            OnLevelUp?.Invoke();
            PlayLevelUpEffect();

            Common.Log($"Level Up! Now level {currentLevel}");
        }
        OnXpChange?.Invoke();

    }
    void CalculateStats(){
        //charStats.damage.SetDefaultValue( StaticValue.C_LV_DAM_ADD[ currentLevel - 1] );
        charStats.Health.SetDefaultValue( StaticValue.C_LV_HP_ADD [ currentLevel - 1] );  
    }
    [Button]
    void Test(){
        AddExperience(100);
    }
    [Button]
    void PlayLevelUpEffect(){
        paintEffect.Creat( effLvUp, transform.position).SetData(core.SortingLayerID,core.uniqueID);
    }
}