using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour
{
    [SerializeField] private ItemData itemData;
    [SerializeField] private Rigidbody2D rb;
    private CircleCollider2D mCol;
    private SpriteRenderer mSPR;

    private void OnEnable() {
        if(mCol == null) mCol = GetComponent<CircleCollider2D>();
        if(mSPR == null) mSPR = GetComponent<SpriteRenderer>();
        if(rb == null) rb = GetComponent<Rigidbody2D>();
        Invoke("Remove",5f);
    }
    void Remove(){
        Destroy(gameObject);
    }
    private void SetupVisuals()
    {
        if (itemData == null) return;
        mSPR.sprite = itemData.icon;
        gameObject.name = "Item - " + itemData.itemName;   
        //--------- Set Collider-----------
        float height = itemData.icon.rect.height/200f;
        float width = itemData.icon.rect.width/200f;
        mCol.radius = height>width ? width : height;
    }


    public void SetupItem(ItemData _itemData, Vector2 _velocity)
    {
        itemData = _itemData;
        rb.velocity = _velocity;
        SetupVisuals();
    }
    public void PickupItem()
    {
        Inventory.Instance.AddItem(itemData);
        Destroy(gameObject);
    }

    private void OnValidate() {
        if(mCol == null) mCol = GetComponent<CircleCollider2D>();
        if(mSPR == null) mSPR = GetComponent<SpriteRenderer>();
        SetupVisuals();
    }
    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Player")){
            PickupItem();
        }
    }
}
