using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HitBox
{
#region Component
    Collider2D mHitBox;
#endregion

#region Data
    string tag;
    ContactFilter2D contactFilter = new ContactFilter2D();
    int maxObjDetected ;
    Collider2D[] results;
#endregion

    public Action<Collider2D> OnHit;

    [field : SerializeField] List<Collider2D> objDetected = new List<Collider2D>();
    public HitBox(Collider2D mHitBox,LayerMask layer, int maxObjDetected, string tag){
        this.mHitBox = mHitBox;
        results = new Collider2D[maxObjDetected];
        this.tag = tag;
        contactFilter.SetLayerMask(layer);
    }
    public void OverLapObj(){
        

        if( objDetected.Count >= results.Length ) return;
        int Count = Physics2D.OverlapCollider( mHitBox, contactFilter, results);
        for(int i = 0; i < Count; i++){
            if( objDetected.Count >= results.Length ) continue;
            if( results[i].CompareTag( tag) ) continue;
            if( objDetected.Contains( results[i]) ) continue;
            objDetected.Add( results[i] );
            OnHit?.Invoke( results[i] );
        }
    }
    public void ClearObj(){
        objDetected.Clear();
    }
}