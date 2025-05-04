using System;
using NaughtyAttributes;
using UnityEngine;
using SOArchitecture;

public class LevelSystem : CoreComponent {
    [Header("Level Settings")]
    private int currentLevel = 1;
    public int CurrentLevel {get => currentLevel; 
                            set {
                                currentLevel = value;
                                CalculateStats();
                                if(ShareLevel != null) ShareLevel.Value = currentLevel;
                            }}
                            
    public int currentXP = 0;

    public event Action OnLevelUp;
    public event Action OnXpChange;
    CharacterStats charStats;
    
    PaintEffect paintEffect;
    [SerializeField] IntVariable ShareLevel;
    [SerializeField] BaseEffect effLvUp;

    #region Level Player
    [SerializeField] LevelPlayer_SO levelPlayerSO;
    int HP => levelPlayerSO.levelPlayer[currentLevel - 1].HP;
    int ATK => levelPlayerSO.levelPlayer[currentLevel - 1].ATK;
    int DEF => levelPlayerSO.levelPlayer[currentLevel - 1].DEF;
    public int XpToNextLevel => levelPlayerSO.levelPlayer[currentLevel - 1].xpToNextLevel;
    #endregion
    protected override void Awake()
    {
        base.Awake();
        charStats = core.GetCoreComponent<CharacterStats>();
        paintEffect = core.GetCoreComponent<PaintEffect>();
    }
    protected override void Start()
    {
        base.Start();
        OnXpChange?.Invoke();
        CalculateStats();
        if(ShareLevel != null) ShareLevel.Value = CurrentLevel;
    }
    public void AddExperience(int xpAmount)
    {
        currentXP += xpAmount;
        PoolsContainer.GetObject( this.GetPrefab<PopupText>(), transform.position + Top).Setup(xpAmount, PopupTextType.Exp );
        
        while (currentXP >= XpToNextLevel )
        {
            currentXP -= XpToNextLevel;
            CurrentLevel++;
            
            OnLevelUp?.Invoke();
            PlayLevelUpEffect();

            Common.Log($"Level Up! Now level {currentLevel}");
        }
        OnXpChange?.Invoke();
    }
    void CalculateStats(){
        charStats.damage.SetDefaultValue( ATK );
        charStats.Health.SetDefaultValue( HP ); 
        charStats.armor.SetDefaultValue( DEF );
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