using UnityEngine;
using XNode;

public class DialogueNode : BaseNode
{
	[Input] public NodePort  input;
	[Output] public NodePort  exit;
    public Speaker speaker;
    [TextArea] public string DialogueText;

    public override object GetValue(NodePort port){
		return null;
	}

    public override NodeType GetNodeType() => NodeType.DialogueNode;
}