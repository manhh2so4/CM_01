using UnityEngine;
using System;
using TMPro;
using UnityEngine.UI;
using XNode;
using NaughtyAttributes;
public class TestParser : MonoBehaviour {
    public DialogueGraph graph;
    [Button]
    void Start()
    {
        foreach (BaseNode b in graph.nodes){  
            if (b.GetString() == "Start"){ //"b" is a reference to whatever node it's found next. It's an enumerator variable 
                graph.current = b;
                NextNode();
                break;      
            }    
        }
    }
    [Button]
    void ParseNode(){

        BaseNode b = graph.current; 
        Debug.Log(  "Node : " + b.GetString());
        if(b is DialogueNode){
            DialogueNode d = b as DialogueNode;
            Debug.Log(d.speakerName + ": " + d.dialogueLine);
        }    
    }
    public void NextNode(){
        NodePort exitPort = graph.current.GetOutputPort("exit");
        if (exitPort.IsConnected){
            graph.current = exitPort.Connection.node as BaseNode;
        }else{
            Debug.LogError("ERROR: No exit port connected");
        }
    }
}