
using System.Collections.Generic;
using UnityEngine;

public class ItemDrop : CoreComponent{
    // [SerializeField] private int possibleItemDrop;
    // [SerializeField] private DropInfo[] possibleDrop;
    // private List<ItemData> dropList = new List<ItemData>();
    // [SerializeField] private UnityEngine.GameObject dropPrefab;

    // public virtual void GenerateDrop()
    // {
    //     for (int i = 0; i < possibleDrop.Length; i++)
    //     {
    //         if (Random.Range(0, 100) <= possibleDrop[i].dropChance)
    //             dropList.Add(possibleDrop[i].item);
    //     }

    //     for (int i = 0; i < possibleItemDrop; i++)
    //     {
    //         if(dropList.Count <= 0) return;
    //         ItemData randomItem = dropList[Random.Range(0,dropList.Count - 1)];
    //         dropList.Remove(randomItem);
    //         DropItem(randomItem);
    //     }
    // }

    // protected void DropItem(ItemData _itemData)
    // {
    //     UnityEngine.GameObject newDrop = Instantiate(dropPrefab, transform.position, Quaternion.identity);

    //     Vector2 randomVelocity = new Vector2(Random.Range(-4, 4), Random.Range(3, 6));
    //     newDrop.GetComponent<ItemObject>().SetupItem(_itemData, randomVelocity);
    // }
    
}


//[System.Serializable]
// public class DropInfo
// {
// 	public ItemData item;
//     [Range(0,100)]
//     public float dropChance;
// }