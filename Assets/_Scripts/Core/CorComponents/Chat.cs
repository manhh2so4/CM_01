using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;

public class Chat : CoreComponent
{
    
    [SerializeField] private Box_Chat chatPrefab;
    public string NameChat;
    Movement movement;
    Box_Chat Box_chat;
    TextWriterSingle textWriter;
    protected override void Awake() {
        base.Awake();
        movement = core.GetCoreComponent<Movement>();
        
    }
    [Button]
    void DrawText(){
        SetUpChat("Sena Livestream - Bốc phét + Reaction + Tiktok + Game Gủng");
    }

    public bool SetUpChat(string text,Action action = null){

        if(textWriter != null && textWriter.IsActive()){
            textWriter.WriteAllAndDestroy();
            textWriter = null;
            return false;
        }

        Box_chat?.RemoveBoxChat();
        Box_chat = null;

        Box_chat = PoolsContainer.GetObject(chatPrefab);
        Box_chat.transform.parent = this.transform;
        textWriter = Box_chat.Setup(text,action, core.SortingLayerID); 
        return true;
    }
    public void RemoveBoxChat(){
        Box_chat?.RemoveBoxChat();
        Box_chat = null;
    }

    void OnEnable(){
        if(movement != null) movement.OnFlip += FlipUI;    
    }
    void OnDisable() {
        if(movement != null) movement.OnFlip -= FlipUI;
    }
    void FlipUI() => transform.Rotate(0, 180, 0);

}
