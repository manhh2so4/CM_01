
using System.Collections.Generic;
using System.Linq;
using Cathei.BakingSheet;
using Cathei.BakingSheet.Unity;
using UnityEditor;
using UnityEngine;

public class ItemDropSheet : Sheet<ItemDropSheet.Row>
{
    public class Row : SheetRowArray<Elem>
    {

    }
    public class Elem : SheetRowElem
    {
        public string nameItem { get; set; }
        public float dropChance { get; set; }
    }
}

[System.Serializable]
public class SheetItemDropContainer : BaseSheetContainer{
    public ItemDropSheet ItemDrop { get; set; }
    public override void BakeData(){
        ItemDropSheet.Row[] itemDropSheet = ItemDrop.ToArray();
        string assetPath = "Assets/Data/DataDrop/";
        foreach( var item in itemDropSheet ){
            string namePAth = assetPath + item.Id + ".asset";
            ItemDrop_SO itemDropSO = AssetDatabase.LoadAssetAtPath<ItemDrop_SO>( namePAth );
            if (itemDropSO == null)
            {
                itemDropSO = ScriptableObject.CreateInstance<ItemDrop_SO>();
                AssetDatabase.CreateAsset(itemDropSO, namePAth);
            }
            int countStat = item.Arr.Count;
            itemDropSO.dropInfos = new DropInfo[countStat];
            for(int i = 0; i < countStat; i++){
                DropInfo dropInfo = new DropInfo();
                dropInfo.item = InventoryItemSO.GetFromID(item.Arr[i].nameItem);
                dropInfo.dropChance = item.Arr[i].dropChance;
                itemDropSO.dropInfos[i] = dropInfo;
            }
            EditorUtility.SetDirty(itemDropSO);
        }
    }

}