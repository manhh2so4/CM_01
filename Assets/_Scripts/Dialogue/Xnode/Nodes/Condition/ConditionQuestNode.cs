using System;
using System.Collections;
using System.Collections.Generic;
using HStrong.Quests;
using UnityEngine;
using XNode;
[Node.CreateNodeMenuAttribute("Condition/ConditionQuestNode")] 
public class ConditionQuestNode : ConditionBaseNode
{
    [SerializeField] QuestInfoSO QInfo;
    [SerializeField] QuestState QState;

    public override bool CheckCondition()
    {
        bool success;
        switch (QState)
        {
            case QuestState.HAS_QUEST:
                success = QuestManager.Instance.HasQuest( QInfo.id );
                break;
            default:
                success = QuestManager.Instance.IsQuestState( QInfo.id, QState);
                break;
        }
        return success;
    }  
}


