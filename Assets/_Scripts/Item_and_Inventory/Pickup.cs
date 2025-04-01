using System.Threading;
using UnityEngine;

public class Pickup : MonoBehaviour,IClicker ,IObjectPoolItem {
    mPhysic2D mPhysic2D;
    InventoryItemSO item;
    int number = 1;
    Inventory inventoryPlayer;
    void Awake()
    {
        mPhysic2D = GetComponent<mPhysic2D>();
    }
    private void Start()
    {
        inventoryPlayer = PlayerManager.GetInventory();
    }
    public void Setup(InventoryItemSO item, int number, Vector2 dirV )
    {
        this.item = item;
        GetComponent<SpriteRenderer>().sprite = this.item.GetIcon();
        if (!item.IsStackable())
        {
            number = 1;
        }
        this.number = number;

        mPhysic2D.Velocity = dirV;
    }
    public InventoryItemSO GetItem()
    {
        return item;
    }
    public int GetNumber()
    {
        return number;
    }
    public void PickupItem()
    {

        bool foundSlot = inventoryPlayer.AddToFirstEmptySlot(item, number);
        if (foundSlot)
        {
            RemovePickup();
        }
    }
    public bool CanBePickedUp()
    {
        return inventoryPlayer.HasSpaceFor(item);
    }

    public void OnClick()
    {
        PickupItem();
    }
    private void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.CompareTag("Player")){
            PickupItem();
        }
    }
    void RemovePickup(){
        item = null;
        number = 0;
        ReturnItemToPool();
    }
#region CreatPool
    ObjectPool objectPool;
    void ReturnItemToPool()
    {
        if (objectPool != null)
        {
            objectPool.ReturnObject(this);
            this.transform.SetParent(PoolsContainer.Instance.transform);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    public void SetObjectPool(ObjectPool pool)
    {
        objectPool = pool;
    }

    public void Release()
    {
        objectPool = null;
    }
#endregion
}
