using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace HStrong.Quests
{
    
    [CreateAssetMenu(fileName = "QuestInfoSO", menuName = "Quest/QuestInfoSO", order = 0)]
    public class QuestInfoSO : ScriptableObject {
        [field: SerializeField] public string id { get; private set; }
        [Header("General")]
        public string displayName ;
        [TextArea]public string describle;
        public QuestState state;
        
        [Header("Requirements")]
        public int levelRequirement ;
        public QuestInfoSO[] questPrerequisites;
        [field: SerializeReference] Q_StepData[] questSteps;
        [Header("Rewards")]
        public int goldReward;
        public int expReward;
        public ItemAndCount[] itemRewards;
        public ItemAndCount[] GetItemRewards()
        {
            return itemRewards;
        }
        public Q_StepData[] GetStepData()
        {
            Debug.Log("questSteps " + questSteps.Length);
            return questSteps;
        }
        

    #region EDitor
        
#if UNITY_EDITOR
        [field: SerializeReference] List<Q_StepData> ComponentData;
        public void AddData(Q_StepData data)
        {
            ComponentData.Add(data);
            questSteps = ComponentData.ToArray();
        }
        private void OnValidate()
        {
            id = this.name;
            
            if (questSteps == null || questSteps.Length != ComponentData.Count)
            {
                questSteps = new Q_StepData[ComponentData.Count]; // Tạo mảng mới nếu cần
            }
            ComponentData.CopyTo(questSteps);
            UnityEditor.EditorUtility.SetDirty(this);
        }
        public IEnumerable<Q_StepData> GetComponentData()
        {
            return ComponentData;
        }
        
#endif
    #endregion

    }
}