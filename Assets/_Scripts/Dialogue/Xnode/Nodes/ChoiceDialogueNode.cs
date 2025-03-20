using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

public class ChoiceDialogueNode : BaseNode {
    [Output( dynamicPortList = true, connectionType = ConnectionType.Override )] public List<string> Answers;
    public Speaker speaker;
    [TextArea] public string DialogueText;
    
    public override NodeType GetNodeType() => NodeType.ChoiceDialogueNode;

}
