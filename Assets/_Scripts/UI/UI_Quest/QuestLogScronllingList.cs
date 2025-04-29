using System.Collections.Generic;
using HStrong.Quests;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class QuestLogScronllingList : MonoBehaviour, IPointerClickHandler{
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
    //-----------------------------
    [SerializeField] Quest_InfoUI quest_InfoUI;   
    [SerializeField] GameObject FolowQuest;
    [SpritePreview][SerializeField] Sprite[] Tick;
    Quest CurrentQuest;
    private Dictionary<string, QuestButton> idToButtonMap = new Dictionary<string, QuestButton>();

    [SerializeField] string idQuestButtonSelect;
    void OnEnable()
    {
        SetQuestLogInfo(null);
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
        if(_quest == null){
            QuestInfoNull();
            return;
        }

        CurrentQuest = _quest;
        FolowQuest.SetActive(true);
        CheckFollowQuest();

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

            itemRewardsText.text += $"{ (count == 1 ? null : count) } {item.item.GetDisplayName()}";

            if (i < Length - 1)
            {
                itemRewardsText.text += ", ";
            }
        }
        LogQuestSteps(_quest);
    }

    private void LogQuestSteps(Quest _quest)
    {

        for (int i = StepQuestContener.childCount - 1; i >= 0; i--)
        {
            QuestStep_InfoUI child = StepQuestContener.GetChild(i).GetComponent<QuestStep_InfoUI>();
            child.RemoveQuestStepInfo();
        }

        for (int i = 0; i < _quest.GetQ_StepData().Length; i++){

            QuestStep_InfoUI step_InfoUI = PoolsContainer.GetObject( this.GetPrefab<QuestStep_InfoUI>(), StepQuestContener );
            step_InfoUI.transform.localScale = Vector3.one;
            step_InfoUI.SetQuestStepInfo( _quest.GetQ_StepData()[i].__description__ , _quest.GetQuestStepStates()[i] );

        }
    }
    void QuestInfoNull(){
        FolowQuest.SetActive(false);
        questDisplayNameText.text = "Chọn nhiệm vụ để xem thông tin";
        questDescrible.text = "";
        goldRewardsText.text = "";
        expRewardsText.text = "";
        itemRewardsText.text = "";

        for (int i = StepQuestContener.childCount - 1; i >= 0; i--)
        {
            QuestStep_InfoUI child = StepQuestContener.GetChild(i).GetComponent<QuestStep_InfoUI>();
            child.RemoveQuestStepInfo();
        }
    }
    void CheckFollowQuest(){
        if(quest_InfoUI.questFollow.info.id == CurrentQuest.info.id){
            FolowQuest.GetComponentInChildren<Image>().sprite = Tick[1];
        }else{
            FolowQuest.GetComponentInChildren<Image>().sprite = Tick[0];
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("click");
        if(quest_InfoUI.questFollow.info.id == CurrentQuest.info.id) return;

        quest_InfoUI.questFollow = CurrentQuest;
        CheckFollowQuest();
    }
}