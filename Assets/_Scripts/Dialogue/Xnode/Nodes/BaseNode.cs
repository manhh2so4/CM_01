using XNode;
public abstract class BaseNode : Node
{
    public virtual string GetString(){
		return null;
	}
    public abstract NodeType GetNodeType();
    public override object GetValue(NodePort port){
		return null;
	}
}
public enum NodeType{
    none,
    StartNode,
    DialogueNode,
    ChoiceDialogueNode,
    ExitNode
}