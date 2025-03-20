using UnityEngine;
using UnityEditor;
using XNodeEditor;

[CustomNodeEditor(typeof(EventNode))]
public class EventNodeEditor : NodeEditor
{
    public override void OnBodyGUI()
    {
        serializedObject.Update();

        GUILayout.BeginHorizontal();

        NodeEditorGUILayout.PortField(GUIContent.none, target.GetInputPort("input"), GUILayout.MinWidth(0));
        NodeEditorGUILayout.PortField(GUIContent.none, target.GetOutputPort("exit"), GUILayout.MinWidth(0));
        GUILayout.EndHorizontal();

        NodeEditorGUILayout.PropertyField(serializedObject.FindProperty("trigger")); 

        serializedObject.ApplyModifiedProperties();
    }
    public override int GetWidth() {
        return 150;
    }
}