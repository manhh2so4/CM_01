using System.Collections.Generic;
using UnityEngine;
public class ManagerDropEnemy : MonoBehaviour {
    [SerializeField] ItemDrop_SO itemDropSO;
    private List<InventoryItemSO> dropList = new List<InventoryItemSO>();
    void OnEnable()
    {
        this.GameEvents().miscEvents.onKillEnemy += GenerateDrop;
    }
    
    void OnDisable()
    {
        this.GameEvents().miscEvents.onKillEnemy -= GenerateDrop;
    }
    public virtual void GenerateDrop(EnemyEntity _enemy)
    {
        for (int i = 0; i < itemDropSO.dropInfos.Length; i++){

            if (Random.Range(0, 100) <= itemDropSO.dropInfos[i].dropChance)
                dropList.Add( itemDropSO.dropInfos[i].item );

        }

        if( dropList.Count <= 0 ) return;
        InventoryItemSO randomItem = dropList[ Random.Range(0, dropList.Count - 1) ];
        DropItem(randomItem, _enemy.transform.position);
        dropList.Clear();
    }

    protected void DropItem(InventoryItemSO item, Vector2 _position)
    {
        Vector2 randomVelocity = new Vector2( Random.Range(-4, 4), Random.Range(5, 8));
        item.SpawnPickup( _position, 1 ,randomVelocity);
    }

}