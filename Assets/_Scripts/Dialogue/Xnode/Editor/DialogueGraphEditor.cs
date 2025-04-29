using UnityEngine;
using UnityEditor;
using XNodeEditor;
using System;
using XNode;
[CustomEditor(typeof(DialogueGraph))]
public class DialogueGraphEditor : Editor{

    public override void OnInspectorGUI() {
        base.OnInspectorGUI();
        if (GUILayout.Button("Setup Start and End"))
        {
            DialogueGraph dialogueGraph = (DialogueGraph)target;
            dialogueGraph.SetUpStart_End();
        }
    }
}

[CustomNodeGraphEditor(typeof(DialogueGraph))]
public class SetNodeGraphEditor : NodeGraphEditor {
    
    public override void OnGUI() {
        base.OnGUI();
        GUIStyle style = new GUIStyle(GUI.skin.label);
        style.fontSize = 30;
        GUILayout.Label(  ((DialogueGraph)target).name, style );
    }

}