using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using HStrong.Quests;
public class Quest_InfoUI : MonoBehaviour {
    [SerializeField] Transform StepQuestContener;
    [SerializeField] GameObject stepsPrefab;
    [SpritePreview][SerializeField] Sprite[] Tick;
    [SerializeField] QuestLogScronllingList scrollingList;

    [Header("Components_Text")]
    [SerializeField] TextMeshProUGUI questDisplayNameText;
    [SerializeField] TextMeshProUGUI questDescrible;
    [SerializeField] TextMeshProUGUI goldRewardsText;
    [SerializeField] TextMeshProUGUI expRewardsText;
    [SerializeField] TextMeshProUGUI itemRewardsText;

    void OnEnable()
    {
        this.GameEvents().questEvent.onQuestStateChange += QuestStateChange;
    }
    void OnDisable()
    {
        this.GameEvents().questEvent.onQuestStateChange -= QuestStateChange;
    }

    private void QuestStateChange(Quest quest)
    {
        QuestButton questButton = scrollingList.CreateButtonIfNotExists(quest, () => {
            SetQuestLogInfo(quest);
        });

        questButton.SetState(quest.state);
    }
    void SetQuestLogInfo(Quest quest){
        questDisplayNameText.text = quest.info.displayName;
        questDescrible.text = quest.info.describle;
        foreach (Transform child in StepQuestContener)
        {
            Destroy(child.gameObject);
        }
        foreach ( Q_StepData data in quest.info.GetStepData() )
        {
            GameObject stepGO = Instantiate(stepsPrefab,StepQuestContener);
            TextMeshProUGUI nameStep = stepGO.transform.GetChild(0).GetChild(1).GetComponentInChildren<TextMeshProUGUI>();

            nameStep.text = data.__description__;
            Image checkBox = stepGO.transform.GetChild(0).GetComponentInChildren<Image>();

            if (true) {
                checkBox.sprite = Tick[1];
            } else {
                //checkBox.sprite = Tick[0];
            }

        }
        goldRewardsText.text = $"+{ quest.info.goldReward.ToString() } yen";
        expRewardsText.text = $"+{ quest.info.expReward.ToString() } exp";
        itemRewardsText.text = "";

        int Length = quest.info.GetItemRewards().Length;
        for (int i = 0; i < Length; i++)
        {
            ItemAndCount item = quest.info.GetItemRewards()[i];
            int count = item.count;
        
            itemRewardsText.text += $"{(count==1? null : count)} {item.item.GetDisplayName()}";
    
            if (i < Length - 1)
            {
                itemRewardsText.text += ", ";
            }
        }


    }
}