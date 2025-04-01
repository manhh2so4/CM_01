using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WeaponHitBox : WeaponComponents<HitBoxData>{

    public event Action<Collider2D> Action;
    Vector2 mSize,mOffset;
    [SerializeField] WeaponSprite weaponSprite;
    [SerializeField] int maxObjDetected = 3;
    [field : SerializeField] List<Collider2D> objDetected = new List<Collider2D>();
    public void OnHitBox(){

    }
    public void OffHitBox(){
        mSize = Vector2.zero;
        mOffset = Vector2.zero;
        objDetected.Clear();
    }

    public void RangeHitBox(Vector2 vec){ 
        if( vec.y > mSize.y || vec.x > mSize.x ) {
            if (vec.x > mSize.x){
                mSize.x = vec.x;
                mOffset.x = vec.x/2;
            }

            if (vec.y > mSize.y) {
                mSize.y = vec.y;
                mOffset.y = vec.y/2;         
            }
            OverLapObj();
        }
    }
    Collider2D[] results;
    Vector2 center;
    void OverLapObj(){

        if( objDetected.Count >= maxObjDetected ) return;
        CapsuleDirection2D Dir = mSize.x > mSize.y ? CapsuleDirection2D.Horizontal : CapsuleDirection2D.Vertical;
        center.Set(mOffset.x * coreMove.facingDirection, mOffset.y);
        int Count = Physics2D.OverlapCapsuleNonAlloc( (Vector2)transform.position + center, mSize , Dir, 0, results ,data.DetectableLayers);

        for(int i = 0; i < Count; i++){

            if( objDetected.Count >= maxObjDetected ) continue;
            if( results[i].CompareTag( transform.parent.tag) ) continue;
            if( objDetected.Contains( results[i]) ) continue;
            
            objDetected.Add( results[i] );
            Action?.Invoke( results[i] );

        }

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
        weaponSprite = GetComponent<WeaponSprite>();
        results = new Collider2D[maxObjDetected+1];
        OffHitBox();

        weaponSprite.setRange += RangeHitBox;
    }

    public override void Refest()
    { 
        base.Refest();
        weaponSprite.setRange -= RangeHitBox;
    }
    public void OnDrawGizmos(){
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube((Vector2)transform.position + center, mSize);
    }
}