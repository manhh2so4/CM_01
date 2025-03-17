using System.Collections.Generic;
using NaughtyAttributes;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.Rendering;
namespace HStrong.Quests
{
    public class QuestManager : MonoBehaviour
    {
        [Header("Config")]
        [SerializeField] private bool loadQuestState = true;
        [SerializeField] QuestInfoSO[] questALL;
        [SerializeField] private Quest mQuest;

        private Dictionary<string, Quest> questMap;
        private int currentPlayerLV=10;
        void Awake()
        {
            questMap = CreateQuestMap();
        }
        void OnEnable()
        {
            this.GameEvents().questEvent.onStartQuest += StartQuset;
            this.GameEvents().questEvent.onAdvanceQuest += AdvanceQuest;
            this.GameEvents().questEvent.onFinishQuest += FinishQuest;
            this.GameEvents().questEvent.onQuestStepStateChange += QuestStepStateChangre;
            
        }

        void OnDisable()
        {
            this.GameEvents().questEvent.onStartQuest -= StartQuset;
            this.GameEvents().questEvent.onAdvanceQuest -= AdvanceQuest;
            this.GameEvents().questEvent.onFinishQuest -= FinishQuest;
            this.GameEvents().questEvent.onQuestStepStateChange -= QuestStepStateChangre;
        }

        [Button("Start Quest")]
        public void StartQuest()
        {
            this.GameEvents().questEvent.StartQuest( questALL[0].id );
        }

        void Start()
        {
            foreach(Quest quest in questMap.Values)
            {
                mQuest = quest;
                
                if(quest.state == QuestState.IN_PROGRESS)
                {
                    quest.InstantiateCurrentQuestStep(this.transform);
                }
                this.GameEvents().questEvent.QuestStateChange(quest);
            }
        }
        void ChangeQuestState(string id, QuestState state)
        {
            Quest quest = GetQuestById(id);
            quest.state = state;
            this.GameEvents().questEvent.QuestStateChange(quest);
        }
        void PlayerLevelChange(int Level){
            currentPlayerLV = Level;
        }
        private bool CheckRequirements(Quest quest){
            bool requirementsMet = true;
            if(currentPlayerLV < quest.info.levelRequirement){
                requirementsMet = false;
            }

            foreach (QuestInfoSO prerequisiteQuestInfo in quest.info.questPrerequisites)
            {
                if(GetQuestById(prerequisiteQuestInfo.id).state != QuestState.FINISHED){
                    requirementsMet = false;
                }
            }

            return requirementsMet;
        }
        void Update()
        {

            foreach(Quest quest in questMap.Values)
            {
                
                if(quest.state == QuestState.REQUIREMENTS_NOT_MET && CheckRequirements(quest)){

                    ChangeQuestState(quest.info.id, QuestState.CAN_START);

                }
            }
        }
        private void StartQuset(string id){
            
            Quest quest = GetQuestById(id);
            quest.InstantiateCurrentQuestStep(this.transform);
            ChangeQuestState(quest.info.id, QuestState.IN_PROGRESS);

        }
        private void AdvanceQuest(string id){
            
            Quest quest = GetQuestById(id);
            quest.MoveToNextStep();
            if(quest.CanNextStep()){
                quest.InstantiateCurrentQuestStep(this.transform);
            }else{
                ChangeQuestState(quest.info.id, QuestState.CAN_FINISH);
                this.GameEvents().questEvent.FinishQuest(id);
            }
        }
        void FinishQuest(string id){
            Quest quest = GetQuestById(id);
            ClaimRewards(quest);
            ChangeQuestState(quest.info.id, QuestState.FINISHED);
        }
        void ClaimRewards(Quest quest){
            //TODO: Implement reward claiming
            Common.Log("Claiming rewards for quest " + quest.info.displayName);
        }

        void QuestStepStateChangre(string id,int stepIndex,QuestStepState questStepState){
            Quest quest = GetQuestById(id);
            quest.StoreQuestStepState(questStepState, stepIndex);
            ChangeQuestState(id, quest.state);
        }
        private Dictionary<string, Quest> CreateQuestMap()
        {
            QuestInfoSO[] allQuests = questALL;
            Dictionary<string, Quest> idToQuestMap = new Dictionary<string, Quest>();
            foreach (QuestInfoSO questInfo in allQuests)
            {
                if (idToQuestMap.ContainsKey(questInfo.id))
                {
                    Debug.LogWarning("Duplicate ID found when creating quest map: " + questInfo.id);
                }
                idToQuestMap.Add(questInfo.id, LoadQuest(questInfo));
            }
            
            return idToQuestMap;
        }

        private Quest GetQuestById(string id){
            Quest quest = questMap[id];
            if(quest == null){
                Common.LogError("Quest with id " + id + " not found");
            }
            return quest;
        }
        private void OnApplicationQuit()
        {
            foreach(Quest quest in questMap.Values)
            {
                SaveQuest(quest);
            }
        }
        private void SaveQuest(Quest quest)
        {
            try 
            {
            QuestData questData = quest.GetQuestData();
            // serialize using JsonUtility, but use whatever you want here (like JSON.NET)
            string serializedData = JsonUtility.ToJson(questData);
            // saving to PlayerPrefs is just a quick example for this tutorial video,
            // you probably don't want to save this info there long-term.
            // instead, use an actual Save & Load system and write to a file, the cloud, etc..
            PlayerPrefs.SetString(quest.info.id, serializedData);
            }
            catch (System.Exception e)
            {
                Debug.LogError("Failed to save quest with id " + quest.info.id + ": " + e);
            }
        }
        private Quest LoadQuest(QuestInfoSO questInfo){
            Quest quest = null;
            try 
            {
                // load quest from saved data
                if (PlayerPrefs.HasKey(questInfo.id) && loadQuestState)
                {
                    string serializedData = PlayerPrefs.GetString(questInfo.id);
                    QuestData questData = JsonUtility.FromJson<QuestData>(serializedData);
                    quest = new Quest(questInfo, questData.state, questData.questStepIndex, questData.questStepStates);
                }

                // otherwise, initialize a new quest
                else 
                {
                    quest = new Quest(questInfo);
                }
            }
            catch (System.Exception e)
            {
                Debug.LogError("Failed to load quest with id " + quest.info.id + ": " + e);
            }
            return quest;
        }
    }
}
