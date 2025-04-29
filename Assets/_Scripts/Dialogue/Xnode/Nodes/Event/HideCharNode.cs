using UnityEngine;
using XNode;
[NodeTint("#9e692e")]
[Node.CreateNodeMenuAttribute("Event/HideCharNode")] 
public class HideCharNode : BaseNode
{
    [Output(connectionType = ConnectionType.Override)] public Empty exit;
    public float duration;
    public override NodeType GetNodeType() => NodeType.HideCharNode;

}