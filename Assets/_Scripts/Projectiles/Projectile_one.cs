using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile_one : projectile_2
{
    protected override void OnEnable(){
        rb.gravityScale = 0.0f;
    }
    private void OnTriggerEnter2D(Collider2D other) {
        IDamageable damageable;
        if(other.TryGetComponent<IDamageable>(out damageable)) {
            if(other.tag == gameObject.tag) return;
            //stats.DoDamage(damageable.Target(EffecHit));
            remove();
        }
    }
}
