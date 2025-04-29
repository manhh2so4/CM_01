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

[CustomNodeEditor(typeof(FinishQuestNode))]
public class FinishQuestNodeEditor : NodeEditor
{
    public override void OnBodyGUI()
    {
        serializedObject.Update();
        GUILayout.BeginHorizontal();
        NodeEditorGUILayout.PortField(GUIContent.none, target.GetInputPort("input"), GUILayout.MinWidth(0));
        NodeEditorGUILayout.PortField(GUIContent.none, target.GetOutputPort("exit"), GUILayout.MinWidth(0));
        GUILayout.EndHorizontal();
        NodeEditorGUILayout.PropertyField(serializedObject.FindProperty("QuestFinish"),GUIContent.none); 
        serializedObject.ApplyModifiedProperties();
    }
    public override int GetWidth() {
        return 150;
    }
}
[CustomNodeEditor(typeof(GiverItemsNode))]
public class GiverItemsNodeEditor : NodeEditor
{
    public override void OnBodyGUI()
    {
        serializedObject.Update();
        GUILayout.BeginHorizontal();
        NodeEditorGUILayout.PortField(GUIContent.none, target.GetInputPort("input"), GUILayout.MinWidth(0));
        NodeEditorGUILayout.PortField(GUIContent.none, target.GetOutputPort("exit"), GUILayout.MinWidth(0));
        GUILayout.EndHorizontal();

        NodeEditorGUILayout.PropertyField(serializedObject.FindProperty("ItemSOs")); 

        serializedObject.ApplyModifiedProperties();
    }
    public override int GetWidth() {
        return 150;
    }
}