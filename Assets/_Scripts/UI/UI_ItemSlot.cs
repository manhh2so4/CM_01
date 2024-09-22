using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_ItemSlot : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private Image itemImage;
    [SerializeField] private TextMeshProUGUI itemText;
    [SerializeField] private RectTransform rectTransform;

    public InventoryItem item;
    public void UpdateSlot(InventoryItem _newItem)
    {
        item = _newItem;

        itemImage.color = Color.white;

        if (item != null)
        {
            itemImage.sprite = item.data.icon;
            if(rectTransform == null) rectTransform = itemImage.gameObject.GetComponent<RectTransform>();

            rectTransform.sizeDelta = new Vector2( item.data.icon.rect.width, item.data.icon.rect.height );

            if (item.stackSize > 1)
            {
                itemText.text = item.stackSize.ToString();
            }
            else
            {
                itemText.text = "";
            }
        }
    }
    public void CleanUpSlot()
    {
        item = null;
        itemImage.sprite = null;
        itemImage.color = Color.clear;
        itemText.text = "";
    }
    public virtual void OnPointerDown(PointerEventData eventData)
    {
        if(item == null) return;
        if(item.data.itemType != ItemType.Equipment) return;
        Inventory.Instance.EquipItem(item.data);
    }
}