using TMPro;
using UnityEngine;

public class UI_UseItemAction : MonoBehaviour {
    [SerializeField] ActionItem actionItem;
    [SerializeField] TextMeshProUGUI text;
    Inventory inventory;
    void Awake()
    {
        inventory = PlayerManager.GetInventory();
    }
    void Start(){
        UpdateUI();
        inventory.inventoryUpdated += UpdateUI;
    }
    void OnDisable(){
        inventory.inventoryUpdated -= UpdateUI;
    }
    public void UseItem(TypeButton typeButton){
        if(typeButton == TypeButton.Press){
            PlayerManager.GetInventory().UseItemAction(actionItem);
        }
    }
    void UpdateUI()
    {
        text.text = inventory.GetNumberItem(actionItem).ToString();
    }
}