using UnityEngine;
using XNode;
[CreateAssetMenu]
public class DialogueGraph : NodeGraph
{
    public BaseNode start;
    public BaseNode current; //very similar to function declaration
    public BaseNode initNode;
    public void Start(){
        start = initNode; //loops back to the start node
        current = initNode;
    }
}