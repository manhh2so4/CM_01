using System;
using System.Collections;
using System.Collections.Generic;
using HStrong.Quests;
using UnityEngine;
using XNode;
[NodeTint("#8038b0")]
[NodeWidth(152)]
public abstract class ConditionBaseNode : BaseNode
{
    [ Output( connectionType = ConnectionType.Override ) ] public Empty pass;
    [ Output( connectionType = ConnectionType.Override ) ] public Empty fail;
    public bool tryPass;
    public override NodeType GetNodeType() => NodeType.ConditionNode;
    public BaseNode Trigger()
    {   
        NodePort port;
        if( CheckCondition() ) port = GetOutputPort("pass");
        else port = GetOutputPort("fail");
        return port.Connection.node as BaseNode;
    }
    public abstract bool CheckCondition();
}


