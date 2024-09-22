using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float AutoDestroyTime = 5f;
    public int Damage = 5;
    private Rigidbody2D rb;
    private CharacterStats stats;
    [SerializeField] GameObject PrefabHit;


    private const string DISABLE_METHOD_NAME = "remove";
    
    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0.0f;
        
    } 
    private void OnEnable() {
        //CancelInvoke(DISABLE_METHOD_NAME);
        Invoke(DISABLE_METHOD_NAME,AutoDestroyTime) ;
    } 
    private void OnTriggerEnter2D(Collider2D other) {
        IDamageable damageable;
        if(other.TryGetComponent<IDamageable>(out damageable)) {
            if(other.tag == gameObject.tag) return;
            stats.DoDamage(damageable.Target(PrefabHit));
            remove();
        }
        
    }
    public void FireProjectile(float speed, Vector2 _dir, int damage , string tag,CharacterStats stats)
    {
        this.stats = stats;
        rb.velocity = (_dir) * speed;
        gameObject.tag = tag;
        this.Damage = damage;
        float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    private void remove()
    {
        CancelInvoke(DISABLE_METHOD_NAME);
        Destroy(gameObject);
        //rb.velocity = Vector3.zero;
        //gameObject.SetActive(false);
    }
}
