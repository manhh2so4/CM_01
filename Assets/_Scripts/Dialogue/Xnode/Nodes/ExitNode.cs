using XNode;
public class ExitNode : BaseNode
{
    [Input] public Empty endNode;
    public override NodeType GetNodeType() => NodeType.ExitNode;
}