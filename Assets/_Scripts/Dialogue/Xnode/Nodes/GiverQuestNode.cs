using System.Collections;
using System.Collections.Generic;
using HStrong.Quests;
using UnityEngine;
using XNode;
[NodeTint("#4B0082")]
public class GiverQuestNode : BaseNode
{
    [SerializeField] List<Quest> Quests;
    [Input] public NodePort input;
    [Output] public NodePort exit;

    public override NodeType GetNodeType()
    {
        return NodeType.GiverQuestNode;
    }
    public IEnumerable<Quest> GetQuests()
    {
        return Quests;
    }

}