using UnityEngine;
using XNode;
[CreateAssetMenu]
public class DialogueGraph : NodeGraph
{
    [SerializeField] BaseNode start;
    public BaseNode current; //very similar to function declaration
    [SerializeField] BaseNode exit;
    public void Start(){
        current = start;
    }
    public void Exit(){
        current = exit;
    }
}