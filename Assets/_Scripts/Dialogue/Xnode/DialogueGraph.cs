using HStrong.Quests;
using NaughtyAttributes;
using UnityEngine;
using XNode;
[CreateAssetMenu]
public class DialogueGraph : NodeGraph
{
    [SerializeField] BaseNode start;
    public BaseNode Current ;
    [SerializeField] BaseNode exit;
    public void Start() => Current = start;
    public void Exit() => Current = exit;

    
#if UNITY_EDITOR
    void OnDisable()
    {
        SetUpStart_End();
    }
    public void SetUpStart_End()
    {
        foreach(BaseNode node in nodes){
            if(node is StartNode){
                start = node;
                Current = start;
            }
            if(node is ExitNode){
                exit = node;
            }
        }
    }
#endif
}