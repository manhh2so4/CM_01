using UnityEngine;

public class TriggerInteractionBase : MonoBehaviour//, IInteractable
{
    public bool CanInteract { get ; set ; }
    public GameObject Player { get ; set ; }
    private void Start() {
        
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player"))
        {
            CanInteract = true;
            Player = other.gameObject;
        }
    }
    private void OnCollisionExit2D(Collision2D other) {
        if (other.gameObject.CompareTag("Player"))
        {
            CanInteract = false;
            Player = null;
        }
    }

    //public virtual void Interact(){}
}