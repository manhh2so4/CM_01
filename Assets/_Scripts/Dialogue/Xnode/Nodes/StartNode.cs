using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

public class StartNode : BaseNode {
    
    [Output(connectionType = ConnectionType.Override )] public Empty exit;

    public override NodeType GetNodeType() => NodeType.StartNode;
}