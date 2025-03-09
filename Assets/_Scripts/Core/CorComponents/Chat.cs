using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;

public class Chat : CoreComponent
{
    [SerializeField] private Movement movement;
    [SerializeField] private Box_Chat chat;

    protected override void Awake() {
        base.Awake();
        movement = core.GetCoreComponent<Movement>();
    }
    
    public void SetUpChat(string text){
        Box_Chat newChat = PoolsContainer.GetObject(chat);
        newChat.Setup(text);
        newChat.transform.parent = this.transform;
    }

    void OnEnable(){
        if(movement != null) movement.OnFlip += FlipUI;    
    }
    void OnDisable() {
        if(movement != null) movement.OnFlip -= FlipUI;
    }
    void FlipUI() => transform.Rotate(0, 180, 0);

}
