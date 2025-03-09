using XNode;
public class ExitNode : BaseNode
{
    [Input] public NodePort  input;
    public override string GetString()
    {
        return "CloseDialogue_ExitNode";
    }
    public override object GetValue(NodePort port){
		return null;
	}
    public override NodeType GetNodeType() => NodeType.ExitNode;
}