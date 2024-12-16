using System;
using UnityEngine;

public class WeaponHitBox : WeaponComponents<HitBoxData,AttackData>{

    public Action<Collider2D> Action;

    [SerializeField] CapsuleCollider2D mHitBox;
    Vector2 mSize,mOffset;
    WeaponSprite weaponSprite;

    public override void Init() {
        base.Init();        
    }

    public void OnHitBox(){
        mHitBox.enabled = true;
    }


    public void OffHitBox(){
        mHitBox.size = new Vector2(0,0);
        mHitBox.offset = new Vector2(0,0);
        mHitBox.enabled = false;
    }

    public void RangeHitBox(Vector3 vec){ 
        if (vec.x > mHitBox.size.x){
            if(vec.x > 2) vec.Set(vec.x-vec.x/10,0,vec.z);
            mSize.Set(vec.x,mHitBox.size.y);
            mOffset.Set(vec.x/2,vec.z);
            ChangeHitBox();
        }
        if (vec.y > mHitBox.size.y) {
            if(vec.y > 1) vec.Set(0,vec.y-vec.y/10,vec.z);
            mSize.Set(mHitBox.size.x,vec.y);
            mOffset.Set(mHitBox.size.x/2,vec.z);
            ChangeHitBox();           
        }              
    }

    void ChangeHitBox(){
        mHitBox.size = mSize;
        mHitBox.offset = mOffset;
        if(mHitBox.size.x > mHitBox.size.y) mHitBox.direction = CapsuleDirection2D.Horizontal;
        else mHitBox.direction = CapsuleDirection2D.Vertical;
    }
    protected override void HandleEnter()
    {
        base.HandleEnter();
        OnHitBox();
    }
    protected override void HandleExit()
    {
        base.HandleExit();
        OffHitBox();
    }

    protected override void SubscribeHandlers()
    {
        base.SubscribeHandlers();
        
        if (GetComponent<CapsuleCollider2D>() == null)
        {
            gameObject.AddComponent<CapsuleCollider2D>();
        }
        
        mHitBox = GetComponent<CapsuleCollider2D>();
        mHitBox.isTrigger = true;
        OffHitBox();
        weaponSprite = GetComponent<WeaponSprite>();
        weaponSprite.setRange += RangeHitBox;
    }
    protected override void OnDisable()
    {   base.OnDisable();
        Destroy(mHitBox);
        weaponSprite.setRange -= RangeHitBox;
    }
}