using UnityEngine;
using XNodeEditor;
[CustomNodeEditor(typeof(ConditionItemNode))]
public class ConditionItemNodeEditor : BaseConditionItemNodeEditor
{
    public override void OnBodyGUI()
    {
        base.OnBodyGUI();
        NodeEditorGUILayout.PropertyField(serializedObject.FindProperty("ConditionType"),GUIContent.none); 
        NodeEditorGUILayout.PropertyField(serializedObject.FindProperty("ItemSO"),GUIContent.none);

        if( ((ConditionItemNode)target).ConditionType == ConditionItemType.HasItemAmount){
            NodeEditorGUILayout.PropertyField(serializedObject.FindProperty("Amount"),GUIContent.none); 
        }
        serializedObject.ApplyModifiedProperties();
    }

}