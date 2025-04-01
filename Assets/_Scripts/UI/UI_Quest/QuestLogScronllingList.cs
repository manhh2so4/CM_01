using System.Collections.Generic;
using HStrong.Quests;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class QuestLogScronllingList : MonoBehaviour {
    [Header("Quest Log Button")]
    [SerializeField] private GameObject questLogButtonPrefab;

    [Header("Components")]
    [SerializeField] private Transform contentParent;

    [Header("InfoText")]
    [SerializeField] Transform StepQuestContener;
    [SerializeField] TextMeshProUGUI questDisplayNameText;
    [SerializeField] TextMeshProUGUI questDescrible;
    [SerializeField] TextMeshProUGUI goldRewardsText;
    [SerializeField] TextMeshProUGUI expRewardsText;
    [SerializeField] TextMeshProUGUI itemRewardsText;

    private Dictionary<string, QuestButton> idToButtonMap = new Dictionary<string, QuestButton>();
    [SerializeField] string idQuestButtonSelect;
    void OnEnable()
    {
        
    }
    void OnDisable(){

    }
    public void CreateButtonIfNotExists(Quest quest){
        QuestButton questButton = null;
        if(!idToButtonMap.ContainsKey(quest.info.id)){
            questButton = InstantiateQuestLogButton(quest);
        }else{
            questButton = idToButtonMap[quest.info.id];
        }
        if(idQuestButtonSelect == quest.info.id){
            LogQuestSteps(quest);
        }
        questButton.SetState(quest.state);
        
    }
    QuestButton InstantiateQuestLogButton(Quest quest){
        QuestButton questButton = Instantiate(questLogButtonPrefab,contentParent).GetComponent<QuestButton>();
        questButton.gameObject.name = quest.info.id + "_button";
        questButton.Initialize(quest.info.displayName, () => {
            idQuestButtonSelect = quest.info.id;
            SetQuestLogInfo(quest);
        });

        idToButtonMap.Add(quest.info.id, questButton);
        return questButton;
    }
    
    void SetQuestLogInfo(Quest _quest)
    {
        questDisplayNameText.text = _quest.info.displayName;
        questDescrible.text = _quest.info.describle;
        goldRewardsText.text = $"+{_quest.info.goldReward.ToString()} yen";
        expRewardsText.text = $"+{_quest.info.expReward.ToString()} exp";
        itemRewardsText.text = "";
        int Length = _quest.info.GetItemRewards().Length;
        
        for (int i = 0; i < Length; i++)
        {
            ItemAndCount item = _quest.info.GetItemRewards()[i];
            int count = item.count;

            itemRewardsText.text += $"{(count == 1 ? null : count)} {item.item.GetDisplayName()}";

            if (i < Length - 1)
            {
                itemRewardsText.text += ", ";
            }
        }
        LogQuestSteps(_quest);
    }

    private void LogQuestSteps(Quest _quest)
    {

        List<QuestStep_InfoUI> questStep_InfoUI = new List<QuestStep_InfoUI>();
        foreach(Transform child in StepQuestContener) {
            questStep_InfoUI.Add(child.GetComponent<QuestStep_InfoUI>());
        }

        foreach(QuestStep_InfoUI step in questStep_InfoUI){
            step.RemoveQuestStepInfo();
        }
        questStep_InfoUI.Clear();

        for (int i = 0; i < _quest.GetQ_StepData().Length; i++){

            QuestStep_InfoUI step_InfoUI = PoolsContainer.GetObject( this.GetPrefab<QuestStep_InfoUI>(), StepQuestContener );
            step_InfoUI.transform.localScale = Vector3.one;
            step_InfoUI.SetQuestStepInfo( _quest.GetQ_StepData()[i].__description__ , _quest.GetQuestStepStates()[i] );

        }
    }
    
}