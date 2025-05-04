using UnityEngine;

[CreateAssetMenu(fileName = "DataItemDrop ", menuName = "Data/ItemDrop", order = 0)]
public class ItemDrop_SO : ScriptableObject {
    public DropInfo[] dropInfos;
}