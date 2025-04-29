
using System.Collections.Generic;
using UnityEngine;

public class ItemDrop : CoreComponent {
    [SerializeField] int possibleItemDrop;
    [SerializeField] DropInfo[] possibleDrop;
    public void SetPossibleDrop(DropInfo[] _possibleDrop, int _possibleItemDrop){
        this.possibleDrop = _possibleDrop;
        this.possibleItemDrop = _possibleItemDrop;
    }
    private List<InventoryItemSO> dropList = new List<InventoryItemSO>();

    public virtual void GenerateDrop()
    {
        
        for (int i = 0; i < possibleDrop.Length; i++){

            if (Random.Range(0, 100) <= possibleDrop[i].dropChance)
                dropList.Add( possibleDrop[i].item );

        }

        for (int i = 0; i < possibleItemDrop; i++){

            if( dropList.Count <= 0 ) return;
            InventoryItemSO randomItem = dropList[ Random.Range(0, dropList.Count - 1) ];
            dropList.Remove(randomItem);
            DropItem(randomItem);

        }
    }

    protected void DropItem(InventoryItemSO item)
    {
        Vector2 randomVelocity = new Vector2( Random.Range(-4, 4), Random.Range(5, 8));
        item.SpawnPickup(transform.position, 0 ,randomVelocity);
    }
}


[System.Serializable]
public class DropInfo
{
	public InventoryItemSO item;
    [Range(0,100)]
    public float dropChance;
}