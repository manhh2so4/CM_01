using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
[Node.CreateNodeMenuAttribute("Gate/StartNode")] 
public class StartNode : BaseNode {
    
    [Output(connectionType = ConnectionType.Override )] public Empty exit;
    public override NodeType GetNodeType() => NodeType.StartNode;
}