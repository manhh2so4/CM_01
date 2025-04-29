using System.Collections;
using System.Collections.Generic;
using HStrong.Quests;
using UnityEngine;
using XNode;
[Node.CreateNodeMenuAttribute("Action/GiverQuest")] 
public class GiverQuestNode : BaseActionNode
{
    [SerializeField] List<QuestInfoSO> QuestInfos;
    public override void Trigger(){
        foreach(var quest in QuestInfos){
            GameEventsManager.Instance.questEvent.AddQuestToMap(quest);
        }
    }
}