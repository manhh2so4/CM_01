
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Cathei.BakingSheet;
using Cathei.BakingSheet.Unity;
using UnityEditor;
using UnityEngine;

public class EquipmentSheet : Sheet<EquipmentSheet.Row>
{
    public class Row : SheetRowArray<Elem>
    {
        public string displayName { get; private set; }
        public string description { get; private set; }
        public int requiredLevel { get; private set; }
        public int icon { get; private set; }
        public int price { get; private set; }
        public EquipType equipType { get; private set; }
        public int Part { get; private set; }
    }
    public class Elem : SheetRowElem
    {
        public StatType statType { get; private set; }
        public List<int> value { get; private set; }
    }
}

[System.Serializable]
public class SheetEquipmentContainer : BaseSheetContainer{
    public EquipmentSheet Vukhi {get; private set;}
    public EquipmentSheet Quan {get; private set;}
    public EquipmentSheet Ao {get; private set;}
    public EquipmentSheet Giay {get; private set;}
    public EquipmentSheet Gang {get; private set;}
    public EquipmentSheet Non {get; private set;}
    public EquipmentSheet DayChuyen {get; private set;}
    public EquipmentSheet NgocBoi {get; private set;}
    public EquipmentSheet Bua {get; private set;}
    public EquipmentSheet Nhan {get; private set;}

    public override void BakeData(){

//--------------
        LoadData(Vukhi);
//---------------
        LoadData(Quan);
        LoadData(Ao);
        LoadData(Gang);
        LoadData(Giay);
        LoadData(Non);
//---------------
        LoadData(DayChuyen);
        LoadData(NgocBoi);
        LoadData(Bua);
        LoadData(Nhan);
    }
    public override void PostLoad(){}
    void LoadData( EquipmentSheet Datas ){
        //if(Datas.Count <= 0 ) return;
        string assetPath = "Assets/Resources/Inventory/Equipment/";

        foreach(var data in Datas){
            Directory.CreateDirectory(assetPath + data.equipType);
            string namePAth = assetPath + data.equipType+ "/"+ data.Id + ".asset";
            EquipableItemSO equipmentSO = AssetDatabase.LoadAssetAtPath<EquipableItemSO>( namePAth );
            if(equipmentSO == null){
                equipmentSO = ScriptableObject.CreateInstance<EquipableItemSO>();
                AssetDatabase.CreateAsset(equipmentSO, namePAth);
            }

            Sprite _sprite = GetSpritesID.Get()[data.icon];

            int Count = data.Count;

            ModifiersUpgrade[] AddtiveModifiers = new ModifiersUpgrade[Count];

            for(int i = 0; i < Count; i++){
                AddtiveModifiers[i] = new ModifiersUpgrade{ statType = data[i].statType, _value = data[i].value.ToArray()};
            }
            if(data.Part > 0){
                equipmentSO.SetImageDraw( LoadSpritePart(data.Part, data.equipType, data.Id) );
            }
            equipmentSO.SetData( data.displayName, data.description, _sprite, data.price, data.requiredLevel, data.equipType, AddtiveModifiers);
            EditorUtility.SetDirty(equipmentSO);
        }
    }
    Tex_NjPart_SO LoadSpritePart(int idPart, EquipType equipType, string Id){

        string assetPath = "Assets/Data/Char_tex/";
        Directory.CreateDirectory(assetPath + equipType);
        string namePAth = assetPath + equipType+ "/"+ Id + "_Sprite.asset";

        Tex_NjPart_SO NjPartSO = AssetDatabase.LoadAssetAtPath<Tex_NjPart_SO>( namePAth );
        if(NjPartSO == null){
            NjPartSO = ScriptableObject.CreateInstance<Tex_NjPart_SO>();
            AssetDatabase.CreateAsset(NjPartSO, namePAth);
        }
        
        NjPartSO.AddData(idPart, equipType);

        EditorUtility.SetDirty(NjPartSO);

        return NjPartSO;
    }
}