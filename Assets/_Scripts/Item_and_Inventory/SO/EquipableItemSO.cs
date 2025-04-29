using NaughtyAttributes;
using UnityEngine;

[CreateAssetMenu( menuName = "Inventory/Equipment_no_ADD")]
public class EquipableItemSO : InventoryItemSO {
    [SerializeField] EquipType typeEquip;
    [SerializeField] int requiredLevel;
    
    public EquipType GetTypeEquip(){
        return typeEquip;
    }
    public int GetRequiredLevel(){
        return requiredLevel;
    }
    public int Level;
    [SerializeField] private ModifiersUpgrade[] AddtiveModifiers;
    public ModifiersUpgrade[] GetAddtiveModifiers(){
        return AddtiveModifiers;
    }

    [Header("Image_Character")]
    [SerializeField] Tex_NjPart_SO image_Draw;



    public Tex_NjPart_SO GetImageDraw(){
        return image_Draw;
    }
    public void SetImageDraw(string name){
        string resPath = "Char_tex/Weapon/" + name;
        this.image_Draw = Resources.Load<Tex_NjPart_SO>(resPath);
    }
    public void AddModifiers(CharacterStats playerStats)
    {
        foreach (ModifiersUpgrade modifier in AddtiveModifiers)
        {
            playerStats.AddModifier(modifier.statType, modifier._value[Level]);
        }
    }
    public void RemoveModifiers(CharacterStats playerStats) 
    {
        foreach (ModifiersUpgrade modifier in AddtiveModifiers)
        {
            playerStats.RemoveModifier(modifier.statType, modifier._value[Level]);
        }
    }
    public void SetTypeEquip(int type){
        this.typeEquip = (EquipType)type;
    }
    

#if UNITY_EDITOR
    public void SetData(string name, string description, Sprite icon, int price, int requiredLevel, EquipType typeEquip,ModifiersUpgrade[] AddtiveModifiers){
        this.SetData(name, description, icon, true , price);
        this.requiredLevel = requiredLevel;
        this.typeEquip = typeEquip;
        this.AddtiveModifiers = AddtiveModifiers;
    }
    public void SetImageDraw(Tex_NjPart_SO _image_Draw){
        this.image_Draw = _image_Draw;
    }
#endif
}