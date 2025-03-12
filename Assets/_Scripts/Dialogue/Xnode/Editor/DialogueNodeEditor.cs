using UnityEditor;
using UnityEngine;
using XNode;
using XNodeEditor;
[CustomNodeEditor(typeof(DialogueNode))]
public class DialogueNodeEditor : NodeEditor{
    public override void OnBodyGUI()
    {
        serializedObject.Update();
        DialogueNode node = target as DialogueNode;

        GUILayout.BeginHorizontal();
        NodeEditorGUILayout.PortField(GUIContent.none, target.GetInputPort("input"), GUILayout.MinWidth(0));
        GUILayout.Label("Speaker");
        EditorGUILayout.PropertyField(serializedObject.FindProperty("speaker"), GUIContent.none);
        NodeEditorGUILayout.PortField(GUIContent.none, target.GetOutputPort("exit"), GUILayout.MinWidth(0));
        GUILayout.EndHorizontal();

        GUILayout.Label("", new GUILayoutOption[]{GUILayout.Height(-20),}); 
        NodeEditorGUILayout.PropertyField(serializedObject.FindProperty("DialogueText"), GUIContent.none);
        serializedObject.ApplyModifiedProperties();
    }
    public override Color GetTint() {
        
        DialogueNode node = target as DialogueNode;
        Color col;
        switch(node.speaker){
            case Speaker.NPC:
                return  base.GetTint();
            case Speaker.Player:

                col =  Color.cyan;

                break;
            default:
                return  base.GetTint();
        }
        col *= .3f;
        col.a = 1f;
        return col;       
    }

}