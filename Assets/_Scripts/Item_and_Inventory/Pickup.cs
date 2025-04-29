using System.Threading;
using UnityEngine;

public class Pickup : MonoBehaviour ,IObjectPoolItem {
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
    public void PickUpItem()
    {

        bool foundSlot = inventoryPlayer.AddToFirstEmptySlot(item, number);
        
        if (foundSlot)
        {
            NotifyUIManager.NotifyUI( $"Nhận {number} {item.GetDisplayName()}", item.GetIcon() );
            RemovePickup();
        }
    }
    public bool CanBePickedUp()
    {
        return inventoryPlayer.HasSpaceFor(item);
    }

    private void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.CompareTag("Player")){
            PickUpItem();
        }
    }

    // void OnTriggerExit2D(Collider2D collision){
    //     if(collision.CompareTag("Player")){
    //         PlayerManager.GetInteracButton().SetPickuptable();
    //     }
    // }
    void RemovePickup(){
        item = null;
        number = 0;
        ReturnToPool();
    }

#region CreatPool
    ObjectPool objectPool;
    public void SetObjectPool(ObjectPool pool)
    {
        objectPool = pool;
    }

    public void Release()
    {
        objectPool = null;
    }

    public void ReturnToPool()
    {
        if (objectPool != null)
        {
            objectPool.ReturnObject(this);
            this.transform.SetParent(PoolsContainer.Instance.transform);
        }else
        {
            Destroy(gameObject);
        }
    }
#endregion
}
