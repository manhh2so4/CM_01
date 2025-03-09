using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;
using System;
using Unity.Mathematics;
namespace HStrong.Dialogue.Editor
{
    public class DialogueEditor : EditorWindow {
        Dialogue dialogueSelected;
        [NonSerialized] GUIStyle nodeStyle;
        [NonSerialized] GUIStyle playerNodeStyle;
        [NonSerialized] DialogueNode2 draggingNode = null;
        [NonSerialized] Vector2 draggingOffset;

        
        [NonSerialized] DialogueNode2 creatingNode = null;
        [NonSerialized] DialogueNode2 deletingNode = null;
        [NonSerialized] DialogueNode2 linkingParentNode = null;
        [NonSerialized] Vector2 ScrollPosition = Vector2.zero;
        [NonSerialized] bool draggingCanvas = false;
        [NonSerialized] Vector2 draggingCanvasOffset;  
        [NonSerialized] float zoomLevel = 1.0f;       

        const float canvasSize = 4000;
        const float backgroundSize = 50;



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

            nodeStyle = new GUIStyle();
            nodeStyle.normal.background = EditorGUIUtility.Load("node0") as Texture2D;
            nodeStyle.normal.textColor = Color.blue;
            nodeStyle.padding = new RectOffset(20,20,20,20);
            nodeStyle.border = new RectOffset(12,12,12,12);

            playerNodeStyle = new GUIStyle();
            playerNodeStyle.normal.background = EditorGUIUtility.Load("node1") as Texture2D;
            playerNodeStyle.normal.textColor = Color.blue;
            playerNodeStyle.padding = new RectOffset(20,20,20,20);
            playerNodeStyle.border = new RectOffset(12,12,12,12);
        }
        private void OnSelectionChanged()
        {
            Dialogue dialogue = Selection.activeObject as Dialogue;
            if(dialogue != null ) {
                dialogueSelected = dialogue;
                Repaint();
            }
        }
        private void OnGUI()
        {

            if(dialogueSelected != null){
                EditorGUILayout.TextField( dialogueSelected.name );
                
                ProcessEvent();

                ScrollPosition = EditorGUILayout.BeginScrollView(ScrollPosition);
                //zoomer.Begin();
                Rect canvas = GUILayoutUtility.GetRect(canvasSize,canvasSize);
                Texture2D backgroundTex = Resources.Load("background") as Texture2D;
                Rect texCoords = new Rect( 0, 0, canvasSize / backgroundSize, canvasSize/backgroundSize );
                GUI.DrawTextureWithTexCoords(canvas, backgroundTex, texCoords);

                foreach (DialogueNode2 node in dialogueSelected.GetAllNodes())
                {
                    DrawConnections(node);
                }
                foreach (DialogueNode2 node in dialogueSelected.GetAllNodes())
                {
                    DrawNode(node);
                }
                //zoomer.End();

                EditorGUILayout.EndScrollView();

                if (creatingNode != null) 
                {
                    
                    dialogueSelected.CreateNode(creatingNode);
                    creatingNode = null;
                }

                if (deletingNode != null)
                {
                    dialogueSelected.DeleteNode(deletingNode);
                    deletingNode = null;
                }
            } else EditorGUILayout.TextField( "No Dialogue Selected" );
            
            


        }
        void ProcessEvent(){
            if(Event.current.type == EventType.MouseDown && draggingNode == null){

                draggingNode = GetNodeAtPoint(Event.current.mousePosition + ScrollPosition);
                if(draggingNode != null){
                    draggingOffset =  draggingNode.GetRect().position - Event.current.mousePosition;
                    Selection.activeObject = draggingNode;    
                }else{
                    draggingCanvas = true;
                    draggingCanvasOffset = Event.current.mousePosition + ScrollPosition;
                    Selection.activeObject = dialogueSelected;
                }
            }else if(Event.current.type == EventType.MouseDrag && draggingNode != null){

                draggingNode.SetPosition(Event.current.mousePosition + draggingOffset); 
                GUI.changed = true;

            }else if(Event.current.type == EventType.MouseDrag && draggingCanvas){

                ScrollPosition = draggingCanvasOffset - Event.current.mousePosition;
                GUI.changed = true;

            }else if(Event.current.type == EventType.MouseUp && draggingNode != null){

                draggingNode = null;    

            }else if(Event.current.type == EventType.MouseUp && draggingCanvas){

                draggingCanvas = false;   

            }
        }

        private DialogueNode2 GetNodeAtPoint(Vector2 point)
        {
            DialogueNode2 foundNode = null;
            foreach (DialogueNode2 node in dialogueSelected.GetAllNodes())
            {
                if(node.GetRect().Contains(point)){
                    foundNode = node;
                }
            }
            return foundNode;
        }

        private void DrawNode(DialogueNode2 node)
        {
            GUIStyle style = nodeStyle;
            if( node.IsPlayerSpeaking() ){
                style = playerNodeStyle;
            }
            GUILayout.BeginArea(node.GetRect(), style);

            node.SetText( EditorGUILayout.TextField(node.GetText()) );

            GUILayout.BeginHorizontal();
            if (GUILayout.Button("x"))
            {
                deletingNode = node;
            }

            DrawLinkButton(node);

            if (GUILayout.Button("+"))
            {
                creatingNode = node;
            }
            GUILayout.EndHorizontal();

            GUILayout.EndArea();
        }

        private void DrawLinkButton(DialogueNode2 node)
        {
            if (linkingParentNode == null)
            {

                if (GUILayout.Button("Link"))
                {
                    linkingParentNode = node;
                }
            }else if (linkingParentNode == node){

                if (GUILayout.Button("Cancel"))
                {
                    linkingParentNode = null;
                }

            }else if (linkingParentNode.GetChildren().Contains(node.name)){

                if (GUILayout.Button("UnLink"))
                {
                    linkingParentNode.RemoveChild(node.name);
                    linkingParentNode = null;
                }

            }else{
                if (GUILayout.Button("Child"))
                {
                    linkingParentNode.AddChild(node.name);
                    linkingParentNode = null;
                }
            }
        }

        private void DrawConnections(DialogueNode2 node)
        {
            Vector3 startPosition = new Vector2(node.GetRect().xMax, node.GetRect().center.y);
            foreach (DialogueNode2 childNode in dialogueSelected.GetAllChildren(node))
            {
                Vector3 endPosition = new Vector2(childNode.GetRect().xMin, childNode.GetRect().center.y);
                Vector3 controlPointOffset = endPosition - startPosition;
                controlPointOffset.y = 0;
                controlPointOffset.x *= 0.8f;
                Handles.DrawBezier(
                    startPosition, endPosition, 
                    startPosition + controlPointOffset, 
                    endPosition - controlPointOffset, 
                    Color.white, null, 4f);
            }
        }
    }
}

