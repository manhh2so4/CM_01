using UnityEngine;
using XNode;
[NodeTint("#9e692e")]
public class EventNode : BaseNode
{
    [SerializeField] EventDialogue[] trigger;
    [Output(connectionType = ConnectionType.Override)] public Empty exit;

    public override NodeType GetNodeType()
    {
        return NodeType.EventNode;
    }
    public void InvokeEvent()
    {
        foreach (var trigger in trigger){
            trigger.Raise();
        }
    }

}