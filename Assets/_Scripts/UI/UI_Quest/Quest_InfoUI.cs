using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using HStrong.Quests;
public class Quest_InfoUI : MonoBehaviour {
    [SerializeField] Transform StepQuestContener;
    [Header("Components_Text")]
    [SerializeField] TextMeshProUGUI questDisplayNameText;
    [SerializeField] TextMeshProUGUI questDescrible;
    [SerializeField] TextMeshProUGUI goldRewardsText;
    [SerializeField] TextMeshProUGUI experienceRewardsText;
    [SerializeField] TextMeshProUGUI levelRequirementsText;
    [SerializeField] TextMeshProUGUI questRequirementsText;
    void OnEnable()
    {
        
    }
    void OnDisable()
    {
        
    }
    void SetQuestLogInfo(Quest quest){
        questDisplayNameText.text = quest.info.displayName;
        questDescrible.text = quest.info.describle;
        //StepQuestContener.
        foreach ( var item in quest.info.GetStepData() )
        {
            
        }
        
    }
}