using UnityEngine;
using UnityEditor;

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