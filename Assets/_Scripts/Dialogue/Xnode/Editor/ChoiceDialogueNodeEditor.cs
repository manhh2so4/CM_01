using UnityEngine;
using UnityEditorInternal;
using XNode;
using XNodeEditor;
using UnityEditor;
[CustomNodeEditor(typeof(ChoiceDialogueNode))]
public class ChoiceDialogueNodeEditor : NodeEditor
{

    public override void OnBodyGUI()
    {
        serializedObject.Update();

        GUILayout.BeginHorizontal();
        NodeEditorGUILayout.PortField(GUIContent.none, target.GetInputPort("input"), GUILayout.MinWidth(0));
        GUILayout.Label("Speaker");
        EditorGUILayout.PropertyField(serializedObject.FindProperty("speaker"), GUIContent.none);
        GUILayout.Label("", new GUILayoutOption[]{GUILayout.Width(20),});
        GUILayout.EndHorizontal();

        GUILayout.Label("Dialogue Text");
        GUILayout.Label("", new GUILayoutOption[]{GUILayout.Height(-20),}); 
        NodeEditorGUILayout.PropertyField(serializedObject.FindProperty("DialogueText"), GUIContent.none);

        NodeEditorGUILayout.DynamicPortList(                    
                "Answers", // field name
                typeof(string), // field type
                serializedObject, // serializable object
                NodePort.IO.Input, // new port i/o
                Node.ConnectionType.Override, // new port connection type
                Node.TypeConstraint.None,
                OnCreateReorderableList); // onCreate override. This is where the magic 

        foreach (NodePort dynamicPort in target.DynamicPorts) {
            if (NodeEditorGUILayout.IsDynamicPortListPort(dynamicPort)) continue;
            Debug.Log("dynamicPortdynamicPort");
            NodeEditorGUILayout.PortField(dynamicPort);
        }
        serializedObject.ApplyModifiedProperties();

    }
    void OnCreateReorderableList(ReorderableList list)
    {
        list.elementHeightCallback = (int index) => { return 40;};
        list.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) =>
        {
            var segment = serializedObject.targetObject as ChoiceDialogueNode;
            NodePort port = segment.GetOutputPort("Answers " + index);
            segment.Answers[index] = GUI.TextField(rect, segment.Answers[index]);
            if (port != null){
                Vector2 pos = rect.position + (port.IsOutput? new Vector2(rect.width + 6 ,0 ) : new Vector2(-36, 0));
                NodeEditorGUILayout.PortField(pos, port);
            }
        };
        
    }
    public override Color GetTint() {
        
        ChoiceDialogueNode node = target as ChoiceDialogueNode;
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