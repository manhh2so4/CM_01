using UnityEngine;
using HStrong.Core.UI.Tooltips;
namespace HStrong.Quests {
    public class QuestTooltipSpawner : TooltipSpawner
    {
        public override bool CanCreateTooltip()
        {
            return true;
        }

        public override void UpdateTooltip(GameObject tooltip)
        {
            //QuestStatus status = GetComponent<QuestItemUI>().GetQuestStatus();
            //tooltip.GetComponent<QuestTooltipUI>().Setup(status);
        }
    }
}