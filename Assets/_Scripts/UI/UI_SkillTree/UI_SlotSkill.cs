using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_SlotSkill : MonoBehaviour, IPointerDownHandler {
    [SerializeField] Image _icon;
    public Material grayscaleMaterial; 
    Action onClick;

    public void OnPointerDown(PointerEventData eventData)
    {
        onClick?.Invoke();
        UI_selected.SetSelected(this.transform as RectTransform);
    }
    public void SetSkill(Sprite icon, Action _onClick, bool isUnlock){

        _icon.sprite = icon;
        onClick = _onClick;

        if(isUnlock){
            _icon.material = null;
        }else{
            _icon.material = grayscaleMaterial;
        }
    }

    void OnDisable()
    {
        UI_selected.SetSelected(null);
    }
}