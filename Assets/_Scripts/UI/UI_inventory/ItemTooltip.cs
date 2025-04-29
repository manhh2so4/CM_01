using TMPro;
using UnityEngine;

public class ItemTooltip : MonoBehaviour {
    [SerializeField] TextMeshProUGUI titleText = null;
    [SerializeField] TextMeshProUGUI bodyText = null;
    [SerializeField] Transform Container = null;
    [SerializeField] UI_TextStat uiTextTooltipPrefab;

    public void Setup(InventoryItemSO item)
    {
        string Level = "";
        if(item is EquipableItemSO equipableItem){
            Level = "+" + equipableItem.Level.ToString();
        }
        titleText.text = item.GetDisplayName() + Level;
        bodyText.text = item.GetDescription();
        SetText( $"<color=#FFEB04><size={30}>Gía trị: {item.GetPrice()}vnđ" );
        SetEquipable(item as EquipableItemSO);
    }

    void SetEquipable(EquipableItemSO item){
        if(item == null) return;
        SetText( $"<size={30}>Yêu cầu cấp độ : {item.GetRequiredLevel()}" );
        SetText( $"<size={30}>________________________" );
        foreach(var modifier in item.GetAddtiveModifiers()){
            int value = modifier._value[item.Level];
            UI_TextStat uiTextTooltip = Instantiate( uiTextTooltipPrefab, Container);
            uiTextTooltip.SetTextUI( StaticValue.GetNameStat(modifier.statType, value), StaticValue.GetColorStat(modifier.statType), "");
        }

    }
    void SetText(string text){
        UI_TextStat uiTextTooltip = Instantiate(uiTextTooltipPrefab, Container);
        uiTextTooltip.SetTextUI(text, Color.white, "");
    }
}