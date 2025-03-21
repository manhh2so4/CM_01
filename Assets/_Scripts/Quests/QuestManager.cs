using System.Collections.Generic;
using NaughtyAttributes;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.Rendering;
namespace HStrong.Quests
{
    public class QuestManager : Singleton<QuestManager>
    {
        [Header("Config")]
        [SerializeField] List<Quest> mQuestView;
        readonly Dictionary<string, Quest> questMap = new Dictionary<string, Quest>();
        private int currentPlayerLV=10;
        void OnEnable()
        {

            this.GameEvents().questEvent.onStartQuest += StartQuset;
            this.GameEvents().questEvent.onAdvanceQuest += AdvanceQuest;
            this.GameEvents().questEvent.onFinishQuest += FinishQuest;
            this.GameEvents().questEvent.onQuestStepStateChange += QuestStepStateChangre;
            this.GameEvents().questEvent.onAddQuestToMap += AddQuestToMap;
            
        }
        void OnDisable()
        {
            this.GameEvents().questEvent.onStartQuest -= StartQuset;
            this.GameEvents().questEvent.onAdvanceQuest -= AdvanceQuest;
            this.GameEvents().questEvent.onFinishQuest -= FinishQuest;
            this.GameEvents().questEvent.onQuestStepStateChange -= QuestStepStateChangre;
            this.GameEvents().questEvent.onAddQuestToMap -= AddQuestToMap;
        }

        void Start()
        {
            foreach(Quest quest in questMap.Values)
            {
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
                
                if(quest.state == QuestState.HAS_QUEST && CheckRequirements(quest)){

                    ChangeQuestState(quest.info.id, QuestState.CAN_START);
                    StartQuset(quest.info.id);

                }
            }
        }
    #region Quest Func Events
        public bool HasQuest(string id){
            return questMap.ContainsKey(id);
        }

        public bool IsQuestState(string id, QuestState state){
            Quest quest = GetQuestById(id);

            if(quest == null){
                Common.LogError("Quest with id " + id + " not found");
                return false;
            }
            return GetQuestById(id).state == state;
        }

        private Quest GetQuestById(string id){
            Quest quest = questMap[id];
            if(quest == null){
                Common.LogError("Quest with id " + id + " not found");
            }
            return quest;
        }
    #endregion
    #region Quest State
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

        void QuestStepStateChangre(string id, int stepIndex, QuestStepState questStepState){
            Quest quest = GetQuestById(id);
            quest.StoreQuestStepState(questStepState, stepIndex);
            ChangeQuestState(id, quest.state);
        }
    #endregion
    #region Setup QuestMap
        public void AddQuestToMap(QuestInfoSO questInfoSO){

            if(questMap.ContainsKey(questInfoSO.id)) {
                Common.LogWarning("Quest with id " + questInfoSO.id + " already exists in quest map");
                return;
            }

            questMap.Add(questInfoSO.id, LoadQuest(questInfoSO));
            
            this.GameEvents().questEvent.QuestStateChange(questMap[questInfoSO.id]);
            mQuestView.Add( questMap[questInfoSO.id] );
            //Common.Log("Quest with id " + questInfoSO.id + " added to quest map");
        }
        private Dictionary<string, Quest> CreateQuestMap()
        {
            QuestInfoSO[] allQuests = null;
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

        
        private void OnApplicationQuit()
        {
            // foreach(Quest quest in questMap.Values)
            // {
            //     SaveQuest(quest);
            // }
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
            quest = new Quest(questInfo);   

            return quest;
        }
    #endregion
    }
}
