using System;
using System.Collections;
using System.Collections.Generic;
using HStrong.Quests;
using UnityEngine;
using XNode;

public class ConditionQuestNode : BaseNode
{
    [Output(connectionType = ConnectionType.Override )] public Empty pass;
    [Output(connectionType = ConnectionType.Override )] public Empty fail;
    [SerializeField] QuestInfoSO QInfo;
    [SerializeField] QuestState QState;
    public override NodeType GetNodeType()
    {
        return NodeType.ConditionQuestNode;
    }
    
    public BaseNode Trigger()
    {
        bool success;
        switch (QState)
        {
            case QuestState.HAS_QUEST:
                success = QuestManager.Instance.HasQuest( QInfo.id );
                break;
            default:
                success = QuestManager.Instance.IsQuestState(QInfo.id, QState);
                break;
        }

        NodePort port;
        if(success) port = GetOutputPort("pass");
        else port = GetOutputPort("fail");
        return port.Connection.node as BaseNode;

    }



}