using System.Collections;
using System.Collections.Generic;
using HStrong.Quests;
using UnityEngine;
using XNode;
[Node.CreateNodeMenuAttribute("Action/FinishQuest")] 
public class FinishQuestNode : BaseActionNode
{
    [SerializeField] QuestInfoSO QuestFinish;
    public override void Trigger(){
        GameEventsManager.Instance.questEvent.FinishQuest(QuestFinish.id);
    }
}