using UnityEngine;
using XNodeEditor;
public class BaseConditionItemNodeEditor : NodeEditor
{
    protected bool TryPass => ((ConditionBaseNode)target).tryPass;
    public override void OnBodyGUI()
    {
        serializedObject.Update();
        Color previousColor = GUI.color;
        GUILayout.BeginHorizontal();
            NodeEditorGUILayout.PortField(GUIContent.none,target.GetInputPort("input"),GUILayout.MinWidth(0)); 

            GUILayout.Label("Try Pass",GUILayout.Width(52));
            NodeEditorGUILayout.PropertyField(serializedObject.FindProperty("tryPass"),GUIContent.none, true, GUILayout.MaxWidth(0));

            GUI.color = Color.green;
            NodeEditorGUILayout.PortField(target.GetOutputPort("pass"),GUILayout.MinWidth(27));
            GUI.color = previousColor;
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
            
            if(!TryPass) {
            GUI.color = Color.red;
            NodeEditorGUILayout.PortField(target.GetOutputPort("fail"), GUILayout.MinWidth(0));
            GUI.color = previousColor;
            }
        GUILayout.EndHorizontal();
    }

}