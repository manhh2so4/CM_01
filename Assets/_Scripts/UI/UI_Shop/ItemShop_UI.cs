using UnityEngine;
using UnityEngine.UI;
using HStrong.Shops;
using TMPro;
using UnityEngine.EventSystems;
using System;

public class ItemShop_UI : MonoBehaviour, IPointerClickHandler {
    [SerializeField] Image iconField;
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] TextMeshProUGUI priceText;
    [SerializeField] TextMeshProUGUI countText;
    Shop currentShop = null;
    ShopItem item = null;
    Action OnSelect;

    public void Setup(Shop currentShop, ShopItem item, Action _OnSelect){
        this.currentShop = currentShop;
        this.item = item;

        iconField.sprite = item.GetIcon();
        nameText.text = item.GetName();
        priceText.text = item.GetPrice().ToString() + "\nđồng";
        countText.text = item.GetCount().ToString();
        this.OnSelect = _OnSelect;
    }

    public void OnPointerClick(PointerEventData eventData){
        OnSelect?.Invoke();
        UI_selected.SetSelected(this.transform as RectTransform);
    }
    public void OnDisable(){
        UI_selected.SetSelected(null);
    }
}