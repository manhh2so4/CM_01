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

[CustomNodeEditor(typeof(GotoPointNode))]
public class GotoPointNodeEditor : NodeEditor
{
    public override void OnBodyGUI()
    {
        serializedObject.Update();

        GUILayout.BeginHorizontal();

        NodeEditorGUILayout.PortField(GUIContent.none, target.GetInputPort("input"), GUILayout.MinWidth(0));
        NodeEditorGUILayout.PortField(GUIContent.none, target.GetOutputPort("exit"), GUILayout.MinWidth(0));
        GUILayout.EndHorizontal();

        NodeEditorGUILayout.PropertyField(serializedObject.FindProperty("destination"), GUIContent.none); 

        serializedObject.ApplyModifiedProperties();
    }
    public override int GetWidth() {
        return 150;
    }
}
[CustomNodeEditor(typeof(HideCharNode))]
public class HideCharNodeEditor : NodeEditor
{
    public override void OnBodyGUI()
    {
        serializedObject.Update();

        GUILayout.BeginHorizontal();

        NodeEditorGUILayout.PortField(GUIContent.none, target.GetInputPort("input"), GUILayout.MinWidth(0));
        NodeEditorGUILayout.PropertyField(serializedObject.FindProperty("duration"), GUIContent.none); 
        NodeEditorGUILayout.PortField(GUIContent.none, target.GetOutputPort("exit"), GUILayout.MinWidth(0));
        GUILayout.EndHorizontal();

        serializedObject.ApplyModifiedProperties();
    }
    public override int GetWidth() {
        return 100;
    }
}

[CustomNodeEditor(typeof(ShowUINode))]
public class ShowUINodeEditor : NodeEditor
{
    public override void OnBodyGUI()
    {
        serializedObject.Update();

        GUILayout.BeginHorizontal();
        NodeEditorGUILayout.PortField(GUIContent.none, target.GetInputPort("input"), GUILayout.MinWidth(0));
        NodeEditorGUILayout.PropertyField(serializedObject.FindProperty("isShow"), GUIContent.none); 
        NodeEditorGUILayout.PortField(GUIContent.none, target.GetOutputPort("exit"), GUILayout.MinWidth(0));
        GUILayout.EndHorizontal();

        serializedObject.ApplyModifiedProperties();
    }
    public override int GetWidth() {
        return 80;
    }
}
[CustomNodeEditor(typeof(WaitingNode))]
public class WaitingNodeEditor : NodeEditor
{
    public override void OnBodyGUI()
    {
        serializedObject.Update();

        GUILayout.BeginHorizontal();
        NodeEditorGUILayout.PortField(GUIContent.none, target.GetInputPort("input"), GUILayout.MinWidth(0));
        NodeEditorGUILayout.PropertyField(serializedObject.FindProperty("time"), GUIContent.none); 
        NodeEditorGUILayout.PortField(GUIContent.none, target.GetOutputPort("exit"), GUILayout.MinWidth(0));
        GUILayout.EndHorizontal();

        serializedObject.ApplyModifiedProperties();
    }
    public override int GetWidth() {
        return 80;
    }
}
