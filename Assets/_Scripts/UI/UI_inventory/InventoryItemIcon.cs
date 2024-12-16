using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(Image))]
public class InventoryItemIcon : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI itemNumber = null;
    [SerializeField] Sprite iconDefault;
    public void SetItem(InventoryItemSO item)
    {
        SetItem(item, 0);
    }
    public void SetItem(InventoryItemSO item,int number){
        var iconImage = GetComponent<Image>();
        if (item == null)
        {
            if(iconDefault != null)
            {
                iconImage.sprite = iconDefault;
                Color color = iconImage.color;
                color.a = 0.3F;
                iconImage.color = color;
            }else iconImage.enabled = false;
        }
        else
        {
            iconImage.enabled = true;
            iconImage.sprite = item.GetIcon();
            iconImage.color = Color.white;
            iconImage.SetNativeSize();
        }

        if (itemNumber)
        {
            if (number <= 1)
            {
                itemNumber.gameObject.SetActive(false);
            }
            else
            {
                itemNumber.gameObject.SetActive(true);
                itemNumber.text = number.ToString();
            }
        }
    }

}
