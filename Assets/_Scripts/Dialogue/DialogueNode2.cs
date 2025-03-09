using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[System.Serializable]
public class DialogueNode2 : ScriptableObject
{
    [SerializeField] bool isPlayerSpeaking = false;
    [SerializeField] string text;
    [SerializeField] List<string> children = new List<string>();
    [SerializeField] Rect rect = new Rect(0,0,200,100);
    [SerializeField] string onEnterAction;
    [SerializeField] string onExitAction;
    public bool IsPlayerSpeaking(){
        return isPlayerSpeaking;
    }
    public string GetText(){
        return text;
    }
    public Rect GetRect(){
        return rect;
    }
    public List<string> GetChildren(){
        return children;
    }
     public string GetOnEnterAction(){
        return onEnterAction;
    }

    public string GetOnExitAction(){
        return onExitAction;
    }
    #if UNITY_EDITOR
        public void SetIsPlayerSpeaking(bool newIsPlayerSpeaking){
            if(newIsPlayerSpeaking != isPlayerSpeaking){
                Undo.RecordObject(this, "Update Speaker");
                isPlayerSpeaking = newIsPlayerSpeaking;
                EditorUtility.SetDirty(this);
            }
        }
        public void SetPosition(Vector2 newPos){
            Undo.RecordObject(this, "Move Dialogue Node");
            rect.position = newPos;
            EditorUtility.SetDirty(this);
        }
        public void SetText(string newText){
            if(newText != null){
                Undo.RecordObject(this, "Update Dialogue Text");
                text = newText;
                EditorUtility.SetDirty(this);
            }
        }
        public void AddChild(string childID){

            if(children.Contains(childID)) return;

            Undo.RecordObject(this, "Add Dialogue Link");
            children.Add(childID);
            EditorUtility.SetDirty(this);
        }
        public void RemoveChild(string childID){

            if(children.Contains(childID)){
                Undo.RecordObject(this, "Remove Dialogue Link");
                children.Remove(childID);
                EditorUtility.SetDirty(this);
            }
        }
    #endif
    
}