using XNode;
public abstract class BaseNode : Node
{
    [Input] public Empty input;
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
    ExitNode,
    EventNode,
    ActionNode,
    ConditionNode,
    GotoPointNode,
    HideCharNode,
    ShowUINode,
    WaitingNode,
    
}
public enum Speaker{
    NPC,
    Player,
}
public class Empty {}