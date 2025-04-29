using UnityEngine;

public class BaseNPC : MonoBehaviour, IInteractable
{
    CircleCollider2D circleCollider;
    protected virtual void Awake(){
        circleCollider = GetComponent<CircleCollider2D>();
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player")){
            PlayerManager.GetInteracButton().SetInteractable(this);
        }
    }
    void OnTriggerExit2D(Collider2D collision){
        if(collision.gameObject.CompareTag("Player")){
            PlayerManager.GetInteracButton().SetInteractable();
        }
    }
    public void SetInteractable(bool isInteractable){
        circleCollider.enabled = isInteractable;
    }

    public virtual void Interact()
    {
    }
}