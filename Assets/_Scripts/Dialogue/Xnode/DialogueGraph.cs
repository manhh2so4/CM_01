using HStrong.Quests;
using NaughtyAttributes;
using UnityEngine;
using XNode;
[CreateAssetMenu]
public class DialogueGraph : NodeGraph
{
    [SerializeField] BaseNode start;
    public BaseNode current; //very similar to function declaration
    [SerializeField] BaseNode exit;
    public void Start() => current = start;
    public void Exit() => current = exit;
    
#if UNITY_EDITOR
    public void SetUpStart_End()
    {
        foreach(BaseNode node in nodes){
            if(node is StartNode){
                start = node;
            }
            if(node is ExitNode){
                exit = node;
            }
        }
    }

#endif
}