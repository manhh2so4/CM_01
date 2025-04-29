using UnityEngine;
using UnityEngine.UI;
public class InteracButton : MonoBehaviour{
   [SerializeField] GameObject InteractButton;
   [SerializeField] GameObject PickupButton;
   IInteractable currentInteractable = null;
   IPickUp currentPickUp = null;
   void Start()
   {
      SetInteractable();
      SetPickuptable();
   }
   public void SetInteractable( IInteractable _interactable = null ){
      this.currentInteractable = _interactable;
      InteractButton.SetActive( currentInteractable != null );
      if (currentInteractable == null) return;
   }
   public void SetPickuptable( IPickUp _pickUp = null ){
      this.currentPickUp = _pickUp;
      PickupButton.SetActive( currentPickUp != null );
      if (currentPickUp == null) return;
   }

   public void OnInteract(TypeButton typeButton){

      if(typeButton == TypeButton.Press){
         if( currentInteractable == null ) return;
         currentInteractable.Interact();
      }
   }

   public void OnPickup(TypeButton typeButton){

      if(typeButton == TypeButton.Press){
         if( currentPickUp == null ) return;
         currentPickUp.PickUpItem();
      }
   }




}