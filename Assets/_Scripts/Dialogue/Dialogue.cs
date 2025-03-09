using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
namespace HStrong.Dialogue
{
   // [CreateAssetMenu(fileName = "New Dialogue", menuName = "Dialogue", order = 0)]
    public class Dialogue : ScriptableObject,ISerializationCallbackReceiver
    {
        [SerializeField]
        List<DialogueNode2> nodes = new List<DialogueNode2>();
        Dictionary<string, DialogueNode2> nodeLookup = new Dictionary<string, DialogueNode2>();

        private void OnValidate() {
            
            if(nodeLookup != null) nodeLookup.Clear();

            foreach (DialogueNode2 node in GetAllNodes())
            {
                nodeLookup[node.name] = node;
            }
        }
        public IEnumerable<DialogueNode2> GetAllNodes(){
            return nodes;
        }
        
        public DialogueNode2 GetRootNode(){
            return nodes[0];
        }
        public IEnumerable<DialogueNode2> GetPlayerChildren(DialogueNode2 parentNode)
        {
            foreach (DialogueNode2 node in GetAllChildren(parentNode))
            {
                if (node.IsPlayerSpeaking())
                {
                    yield return node;
                }
            }
        }

        public IEnumerable<DialogueNode2> GetAllChildren(DialogueNode2 parentNode)
        {
            foreach (string childID in parentNode.GetChildren())
            {
                if (nodeLookup.ContainsKey(childID))
                {
                    yield return nodeLookup[childID];
                }
            }
        }
    #if UNITY_EDITOR
        public void CreateNode(DialogueNode2 parent)
        {
            DialogueNode2 newNode = MakeNode(parent);
            Undo.RegisterCreatedObjectUndo(newNode, "Created Dialogue Node");
            Undo.RecordObject(this, "Added Dialogue Node");
            AddNode(newNode);
        }
        private static DialogueNode2 MakeNode(DialogueNode2 parent)
        {
            DialogueNode2 newNode = CreateInstance<DialogueNode2>();
            newNode.name = Guid.NewGuid().ToString();
            if (parent != null)
            {
                parent.AddChild(newNode.name);
                newNode.SetIsPlayerSpeaking(!parent.IsPlayerSpeaking());
                newNode.SetPosition(parent.GetRect().position + Vector2.right * 220);
            }

            return newNode;
        }
        private void AddNode(DialogueNode2 newNode)
        {
            nodes.Add(newNode);
            OnValidate();
        }
        public void DeleteNode(DialogueNode2 nodeToDelete)
        {
            Undo.RecordObject(this, "Deleted Dialogue Node");
            nodes.Remove(nodeToDelete);
            OnValidate();
            CleanDanglingChildren(nodeToDelete);
            Undo.DestroyObjectImmediate(nodeToDelete);
        }

        private void CleanDanglingChildren(DialogueNode2 nodeToDelete)
        {
            foreach (DialogueNode2 node in GetAllNodes())
            {
                node.RemoveChild(nodeToDelete.name);
            }
        }
    #endif
        public void OnBeforeSerialize()
        {
    #if UNITY_EDITOR
            if(nodes.Count == 0){
                DialogueNode2 newNode = MakeNode(null);
                AddNode(newNode);
            }

            if(AssetDatabase.GetAssetPath(this) != ""){
                foreach (DialogueNode2 node in GetAllNodes()){

                    if(AssetDatabase.GetAssetPath(node) == ""){
                        AssetDatabase.AddObjectToAsset(node, this);
                    }

                }
            }
    #endif
        }

        public void OnAfterDeserialize()
        {
            
        }
    }
        
}