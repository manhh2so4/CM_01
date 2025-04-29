using HStrong.Saving;
using UnityEngine;

public class Lock : MonoBehaviour,IInteractable,ISaveable {
    [SerializeField] InventoryItemSO itemCheck;
    [SerializeField] BaseEffect objLock;
    bool IsUnlock ;
    
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player")){
            PlayerManager.GetInteracButton().SetInteractable(this);
        }
    }
    void OnTriggerExit2D(Collider2D collision){
        if(collision.CompareTag("Player")){
            PlayerManager.GetInteracButton().SetInteractable();
        }
    }

    public void Interact(){
            bool success = PlayerManager.GetInventory().RemoveItem(itemCheck);
            if(success){
                Unlock();
                NotifyUIManager.NotifyUI("<color=green>Cổng đã mở");
            }else{
                NotifyUIManager.NotifyUI("<color=red>Bạn không có chìa khóa để mở");
            }
    }
    void Unlock(){
        IsUnlock = true;
        objLock.Trigger();
    }

    public object CaptureState()
    {
        return IsUnlock;
    }

    public void RestoreState(object state)
    {
        IsUnlock = (bool)state;
        if(IsUnlock){
            objLock.ReturnToPool();
        }
    }
}