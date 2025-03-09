using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

public class ChoiceDialogueNode : BaseNode {
    [Input] public NodePort  input;
    [Output(dynamicPortList = true)] public List<string> Answers;
    
    public string speakerName; 
    [TextArea] public string DialogueText;
    public override NodeType GetNodeType() => NodeType.ChoiceDialogueNode;
    public override string GetString(){
		return "ChoiceDialogueNode/" + speakerName + "/" + DialogueText + "/" + Answers[0];
	}

    public override object GetValue(NodePort port){
        return null;
    }
}
