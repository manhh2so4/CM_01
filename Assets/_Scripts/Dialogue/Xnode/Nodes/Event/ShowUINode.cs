using UnityEngine;
using XNode;
[NodeTint("#9e692e")]
[Node.CreateNodeMenuAttribute("Event/ShowUINode")] 
public class ShowUINode : BaseNode
{
    [Output(connectionType = ConnectionType.Override)] public Empty exit;
    public bool isShow;
    public override NodeType GetNodeType() => NodeType.ShowUINode;

}