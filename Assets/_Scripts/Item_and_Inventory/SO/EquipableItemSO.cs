using NaughtyAttributes;
using UnityEngine;

[CreateAssetMenu( menuName = "Inventory/Equipment_no_ADD")]
public class EquipableItemSO : InventoryItemSO {
    [SerializeField] EquipType typeEquip;
    
    public EquipType GetTypeEquip(){
        return typeEquip;
    }
    [SerializeField] private Modifiers[] AddtiveModifiers;
    [SerializeField] private Modifiers[] percentageModifiers;
    [Header("Image_Character")]
    [SerializeField] Tex_NjPart_SO image_Draw;
    

    [System.Serializable]
    struct  Modifiers{
        public StatType statType;
        public int _value;
    }

    public Tex_NjPart_SO GetImageDraw(){
        return image_Draw;
    }
    public void SetImageDraw(string name){
        string resPath = "Char_tex/Weapon/" + name;
        this.image_Draw = Resources.Load<Tex_NjPart_SO>(resPath);
    }
    public void AddModifiers(CharacterStats playerStats)
    {
        foreach (Modifiers modifier in AddtiveModifiers)
        {
            playerStats.GetStatOfType(modifier.statType).AddModifier(modifier._value);
        }
    }
    public void RemoveModifiers(CharacterStats playerStats) 
    {
        foreach (Modifiers modifier in AddtiveModifiers)
        {
            playerStats.GetStatOfType(modifier.statType).RemoveModifier(modifier._value);
        }
    }
    public void SetTypeEquip(int type){
        this.typeEquip = (EquipType)type;
    }
}