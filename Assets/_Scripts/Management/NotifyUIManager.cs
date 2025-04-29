using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotifyUIManager : Singleton<NotifyUIManager> {
    [SerializeField] Transform UINotifyContainer;
    [SerializeField] UI_NotifyText textNotifyPrefab;

    [SerializeField] Queue<Notify> queueText = new Queue<Notify>();

    [SerializeField] int [] slot = new int[5];
    public static void NotifyUI(string text, Sprite icon = null) {
        Instance.queueText.Enqueue(new Notify(text, icon));
        Instance.CheckAndShowText();
    }

    void ShowText(Notify notify, int index, Action OnComplete) {
        UI_NotifyText textPopupInstance = PoolsContainer.GetObject(textNotifyPrefab, UINotifyContainer);
        textPopupInstance.ShowNotification(notify.text, index, OnComplete, notify.icon);
    }
    void CheckAndShowText(){
        if(queueText.Count > 0 ){
            for(int i = 0; i < slot.Length; i++){
                if(slot[i] == 0){
                    slot[i] = 1;
                    ShowText(queueText.Dequeue(), i, () => {OnComplete(i);});
                    break;
                }
            }
        }
    }
    void OnComplete(int index){
        slot[index] = 0;
        CheckAndShowText();
    }
}
public class Notify{
    public string text;
    public Sprite icon;
    public Notify(string text, Sprite icon){
        this.text = text;
        this.icon = icon;
    }
}
