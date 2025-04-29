using UnityEngine;
using XNode;
[Node.CreateNodeMenuAttribute("Dialogue/DialogueNode")] 
public class DialogueNode : BaseNode
{
	[Output(ShowBackingValue.Never,ConnectionType.Override)] public Empty exit;
    [Input] public Empty inputDialogue;

    public Speaker speaker;
    [TextArea] public string DialogueText;
    public override NodeType GetNodeType() => NodeType.DialogueNode;
}