﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

public class StartNode : BaseNode {
    
    [Output] public NodePort exit;

    public override NodeType GetNodeType() => NodeType.StartNode;
    public override string GetString(){
        return "Start";
    }
}