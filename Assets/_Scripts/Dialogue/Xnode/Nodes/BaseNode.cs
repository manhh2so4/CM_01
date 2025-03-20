using XNode;
public abstract class BaseNode : Node
{
    [Input (connectionType = ConnectionType.Override ) ] public Empty input;

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
    GiverQuestNode,
    ConditionQuestNode
}
public enum Speaker{
    NPC,
    Player,
}
public class Empty {}