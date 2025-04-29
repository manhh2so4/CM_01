using UnityEngine;
using XNode;
[NodeTint("#9e692e")]
[Node.CreateNodeMenuAttribute("Event/WaitingNode")] 
public class WaitingNode : BaseNode
{
    [Output(connectionType = ConnectionType.Override)] public Empty exit;
    public float time;
    public override NodeType GetNodeType() => NodeType.WaitingNode;
}