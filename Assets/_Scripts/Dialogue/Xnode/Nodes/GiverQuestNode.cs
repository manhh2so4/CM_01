using System.Collections;
using System.Collections.Generic;
using HStrong.Quests;
using UnityEngine;
using XNode;
[NodeTint("#4B0082")]
public class GiverQuestNode : BaseNode
{
    [SerializeField] List<QuestInfoSO> QuestInfos;
    [Output(connectionType = ConnectionType.Override )] public Empty exit;

    public override NodeType GetNodeType()
    {
        return NodeType.GiverQuestNode;
    }
    public IEnumerable<QuestInfoSO> GetQuests()
    {
        return QuestInfos;
    }

}