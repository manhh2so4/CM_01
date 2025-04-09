using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_EffectPress : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {
   Image image;
   Color normalColor = new Color(1,1,1,1);
   Color pressColor = new Color(.7f, .7f, .7f, 1f);
   public UnityEvent<TypeButton> Onclick;

   void Start()
   {
      image = GetComponent<Image>();
      image.color = normalColor;
   }
   void Press(){
      image.color = pressColor;
   }
   void Release(){
      image.color = normalColor;
   }

   public void OnPointerDown(PointerEventData eventData)
   {
      Press();
      Onclick.Invoke(TypeButton.Press);
   }

   public void OnPointerUp(PointerEventData eventData)
   {

      Release();
      Onclick.Invoke(TypeButton.Release);

   }
   
}
public enum TypeButton{
   Press,
   Release
}