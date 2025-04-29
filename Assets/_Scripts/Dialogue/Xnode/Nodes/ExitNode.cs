using XNode;
[Node.CreateNodeMenuAttribute("Gate/ExitNode")] 
public class ExitNode : BaseNode
{
    [Input] public Empty endType;
    [Output] public Empty exit;
    public ExitType exitType;
    public override NodeType GetNodeType() => NodeType.ExitNode;
}
public enum ExitType{
    None,
    LoopDialogue,
    NextDialogue,
    StopDialogue
}
