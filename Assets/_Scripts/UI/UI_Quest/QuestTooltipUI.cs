using UnityEngine;
using TMPro;
using UnityEngine.UI;
namespace HStrong.Quests {
    public class QuestTooltipUI : MonoBehaviour {
        [SerializeField] TextMeshProUGUI title;
        [SerializeField] Transform objectiveContainer;
        [SerializeField] GameObject objectivePrefab;
        [SpritePreview][SerializeField] Sprite[] Tick;
        public void Setup(QuestStatus status) {
            // Quest quest = status.GetQuest();
            // title.text = quest.GetTitle();
            // objectiveContainer.DetachChildren();

            // foreach (string objective in quest.GetObjectives()) {

            //     GameObject objectiveInstance = Instantiate(objectivePrefab, objectiveContainer);
            //     TextMeshProUGUI objectiveText = objectiveInstance.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
            //     objectiveText.text = objective;

            //     Image checkBox = objectiveInstance.transform.GetChild(0).GetComponent<Image>();
            //     if (status.IsObjectiveComplete(objective)) {
            //         checkBox.sprite = Tick[1];
            //     } else {
            //         checkBox.sprite = Tick[0];
            //     }
            // }
        }
    }
}