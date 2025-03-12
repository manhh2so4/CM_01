using UnityEngine;
using XNode;
[NodeTint("#9e692e")]
public class EventNode : BaseNode
{
    [SerializeField] EventDialogue[] trigger;
    [Input] public NodePort input;
    [Output] public NodePort exit;

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