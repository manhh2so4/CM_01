using UnityEngine;
using XNodeEditor;
[CustomNodeEditor(typeof(ConditionQuestNode))]
public class ConditionQuestNodeEditor : NodeEditor
{
    public override void OnBodyGUI()
    {
        serializedObject.Update();
        Color previousColor = GUI.color;
        GUILayout.BeginHorizontal();
            NodeEditorGUILayout.PortField(GUIContent.none,target.GetInputPort("input"),GUILayout.MinWidth(0));
            GUI.color = Color.green;
            NodeEditorGUILayout.PortField(target.GetOutputPort("pass"),GUILayout.MinWidth(0));
            GUI.color = previousColor;
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
            GUILayout.Label("Quest Info",GUILayout.Width(80));
            GUI.color = Color.red;
            NodeEditorGUILayout.PortField(target.GetOutputPort("fail"),GUILayout.MinWidth(0));
            GUI.color = previousColor;
        GUILayout.EndHorizontal();

        NodeEditorGUILayout.PropertyField(serializedObject.FindProperty("QInfo"),GUIContent.none); 
        NodeEditorGUILayout.PropertyField(serializedObject.FindProperty("QState"),GUIContent.none); 


        serializedObject.ApplyModifiedProperties();
    }
    public override int GetWidth() {
        return 150;
    }
    
}