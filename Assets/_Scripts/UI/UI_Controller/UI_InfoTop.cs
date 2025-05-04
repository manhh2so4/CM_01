using UnityEngine;
using UnityEngine.UI;
using TMPro;
using SOArchitecture;
public class UI_InfoTop : MonoBehaviour {
    //---------------- Components
 
    [SerializeField] StatVariable HealthPlayer;
    [SerializeField] StatVariable ManaPlayer;
    LevelSystem levelSystem;
    //-------------- HP Variables
    [Header("HP")]
    [SerializeField] Image hpBar;
    [SerializeField] Image mpBar;
    [SerializeField] TextMeshProUGUI hpText;
    [SerializeField] TextMeshProUGUI mpText;
   
    //-------------- HP Level System
    [Header("Level System")]
    [SerializeField] TextMeshProUGUI levelText;
    [SerializeField] TextMeshProUGUI progressPercent;
    [SerializeField] Image progressLV;
    
    void Awake()
    {
        levelSystem = PlayerManager.GetLevelSystem();
    }
    void Start(){
        UpdateLevel();
        UpdateProgress();
    }

    void OnEnable(){
        HealthPlayer.OnChange += UpdateHP;
        ManaPlayer.OnChange += UpdateMP;

        levelSystem.OnLevelUp += UpdateLevel;
        levelSystem.OnXpChange += UpdateProgress;
    }

    void OnDisable(){
        HealthPlayer.OnChange -= UpdateHP;
        ManaPlayer.OnChange -= UpdateMP;

        levelSystem.OnLevelUp -= UpdateLevel;
        levelSystem.OnXpChange -= UpdateProgress;
    }

    void UpdateHP(){
        hpBar.fillAmount = Mathf.Clamp( (float)HealthPlayer.Value.currentValue / HealthPlayer.Value.MaxValue ,0 , 1 );
        hpText.text = HealthPlayer.Value.currentValue.ToString();
    }
    void UpdateMP(){
        mpBar.fillAmount = Mathf.Clamp( (float)ManaPlayer.Value.currentValue / ManaPlayer.Value.MaxValue ,0 , 1 );
        mpText.text = ManaPlayer.Value.currentValue.ToString();
    }
    void UpdateLevel(){
        levelText.text = levelSystem.CurrentLevel.ToString();
    }
    void UpdateProgress(){
        float number = (float)levelSystem.currentXP / levelSystem.XpToNextLevel;
        progressPercent.text = $"{Mathf.Round( number * 1000f) / 10f} % ";
        progressLV.fillAmount = number ;
    }
}