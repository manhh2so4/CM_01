using UnityEngine;
using XNode;
[NodeTint("#9e692e")]
[Node.CreateNodeMenuAttribute("Event/GotoPointNode")] 
public class GotoPointNode : BaseNode
{
    [Output(connectionType = ConnectionType.Override)] public Empty exit;
    public Vector2 destination;
    public override NodeType GetNodeType() => NodeType.GotoPointNode;

}