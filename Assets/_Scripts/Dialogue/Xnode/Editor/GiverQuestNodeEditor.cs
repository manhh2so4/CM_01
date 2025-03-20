using UnityEngine;
using UnityEditor;
using XNodeEditor;

[CustomNodeEditor(typeof(GiverQuestNode))]
public class GiverQuestNodeEditor : NodeEditor
{
    public override void OnBodyGUI()
    {
        serializedObject.Update();

        GUILayout.BeginHorizontal();

        NodeEditorGUILayout.PortField(GUIContent.none, target.GetInputPort("input"), GUILayout.MinWidth(0));
        NodeEditorGUILayout.PortField(GUIContent.none, target.GetOutputPort("exit"), GUILayout.MinWidth(0));
        GUILayout.EndHorizontal();

        NodeEditorGUILayout.PropertyField(serializedObject.FindProperty("QuestInfos")); 

        serializedObject.ApplyModifiedProperties();
    }
    public override int GetWidth() {
        return 150;
    }
}