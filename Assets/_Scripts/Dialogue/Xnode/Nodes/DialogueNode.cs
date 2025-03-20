using UnityEngine;
using XNode;

public class DialogueNode : BaseNode
{
	[Output(ShowBackingValue.Never,ConnectionType.Override)] public Empty exit;

    public Speaker speaker;
    [TextArea] public string DialogueText;


    public override NodeType GetNodeType() => NodeType.DialogueNode;
}