using UnityEngine;
using HStrong.Core.UI.Tooltips;
public class ItemTooltipSpawner : TooltipSpawner
{
    public override bool CanCreateTooltip()
    {
        var item = GetComponent<IItemHolder>().GetItem();
            if (!item) return false;

            return true;
    }

    public override void UpdateTooltip(GameObject tooltip)
    {
        var itemTooltip = tooltip.GetComponent<ItemTooltip>();
        if (!itemTooltip) return;

        var item = GetComponent<IItemHolder>().GetItem();

        itemTooltip.Setup(item);
    }
}