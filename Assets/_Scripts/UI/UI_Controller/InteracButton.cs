using UnityEngine;
using UnityEngine.UI;
public class InteracButton : MonoBehaviour{
   [SerializeField] GameObject InteractButton;
   IInteractable currentInteractable = null;
   void Start()
   {
      SetInteractable();
   }
   public void SetInteractable( IInteractable _interactable = null ){
      this.currentInteractable = _interactable;
      InteractButton.SetActive( currentInteractable != null );
      if (currentInteractable == null) return;
   }

   public void OnInteract(TypeButton typeButton){

      if(typeButton == TypeButton.Press){
         if( currentInteractable == null ) return;
         currentInteractable.Interact();
      }
   }
}