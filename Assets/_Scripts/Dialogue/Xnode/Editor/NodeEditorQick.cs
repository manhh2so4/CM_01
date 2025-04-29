using UnityEngine;
using UnityEditor;
using XNode;
using XNodeEditor;
using UnityEngine.UIElements;
[CustomNodeEditor(typeof(StartNode))]
public class StartNodeEditor : NodeEditor{
    public override void OnBodyGUI(){

        serializedObject.Update();

        NodeEditorGUILayout.PortField(GUIContent.none, target.GetOutputPort("exit"), GUILayout.MinWidth(0));
        serializedObject.ApplyModifiedProperties();
    }
    public override int GetWidth() {
        return 100;
    }
    public override Color GetTint() {
        
        Color col = Color.green;
        col *= .3f;
        col.a = 1f;
        return col;       
    }
}

[CustomNodeEditor(typeof(ExitNode))]
public class ExitNodeEditor : NodeEditor{

    public override void OnBodyGUI(){

        serializedObject.Update();
        GUILayout.BeginHorizontal();
        NodeEditorGUILayout.PortField( target.GetInputPort("endType") , GUILayout.MinWidth(0));
        if( ((ExitNode)target).exitType == ExitType.StopDialogue ){
            NodeEditorGUILayout.PortField( target.GetOutputPort("exit") , GUILayout.MinWidth(0));
        }
        GUILayout.EndHorizontal();
        //GUILayout.Label("exitType");
        EditorGUILayout.PropertyField(serializedObject.FindProperty("exitType"), GUIContent.none);
        
        serializedObject.ApplyModifiedProperties();
    }
    public override int GetWidth() {
        return 150;
    }
    public override Color GetTint() {
        
        Color col = Color.red;
        col *= .3f;
        col.a = 1f;
        return col;       
    }

}