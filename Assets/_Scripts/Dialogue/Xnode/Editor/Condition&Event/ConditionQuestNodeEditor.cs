using UnityEngine;
using XNodeEditor;
[CustomNodeEditor(typeof(ConditionQuestNode))]
public class ConditionQuestNodeEditor : BaseConditionItemNodeEditor
{
    public override void OnBodyGUI()
    {
        base.OnBodyGUI();
        GUILayout.Label("Quest Info",GUILayout.Width(80));
        NodeEditorGUILayout.PropertyField(serializedObject.FindProperty("QInfo"),GUIContent.none); 
        NodeEditorGUILayout.PropertyField(serializedObject.FindProperty("QState"),GUIContent.none); 


        serializedObject.ApplyModifiedProperties();
    }

}