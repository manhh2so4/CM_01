using UnityEngine;
using XNode;

public class DialogueNode : BaseNode
{
	[Input] public NodePort  input;
	[Output] public NodePort  exit;
    public string speakerName;
    [TextArea] public string dialogueLine;
    public override string GetString(){
		return "DialogueNode/" + speakerName + "/" + dialogueLine; 
	}
    public override object GetValue(NodePort port){
		return null;
	}

    public override NodeType GetNodeType() => NodeType.DialogueNode;
}