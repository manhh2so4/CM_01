using System.Collections;
using System.Collections.Generic;
using HStrong.Quests;
using UnityEngine;
using XNode;
[NodeTint("#4B0082")]
public abstract class BaseActionNode : BaseNode
{
    [Output(connectionType = ConnectionType.Override )] public Empty exit;

    public override NodeType GetNodeType() => NodeType.ActionNode;
    public abstract void Trigger();
}