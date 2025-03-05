using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;
namespace HStrong.Dialogue.Editor
{
    public class DialogueEditor : EditorWindow {
        Dialogue dialogueSelected;

        [MenuItem("CM_01/Dialogue")]
        private static void ShowWindow() {
            var window = GetWindow<DialogueEditor>();
            window.titleContent = new GUIContent("Dialogue");
            window.Show();
        }
        [OnOpenAsset(1)]
        public static bool OpenDialogue(int instanceID, int line){
            Dialogue dialogue = EditorUtility.InstanceIDToObject(instanceID) as Dialogue;
            if(dialogue == null) return false;
            ShowWindow();
            return true;
        }
        private void OnEnable()
        {
            Selection.selectionChanged += OnSelectionChanged;
        }

        private void OnSelectionChanged()
        {
            Dialogue dialogue = Selection.activeObject as Dialogue;
            if(dialogue != null) {
                dialogueSelected = dialogue;
                Repaint();
            }
        }

        private void OnGUI()
        {
            if(dialogueSelected != null){
                foreach (DialogueNode node in dialogueSelected.GetAllNode())
                {   
                    node.text = EditorGUILayout.TextField( node.text );
                }
                
            }else{
                EditorGUILayout.TextField( "No Dialogue Selected" );
            }

        }
    }
}

