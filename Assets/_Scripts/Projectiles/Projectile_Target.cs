using UnityEngine;

public class Projectile_Target : projectile_2 {
    Transform target;
    float speed;
    float rotationSpeed = 5f;
    void Update()
    {
        if (target == null) return;
        Vector3 direction = (target.position - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;

        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
    private Vector3 GetAimLocation()
    {
        CapsuleCollider targetCapsule = target.GetComponent<CapsuleCollider>();
        if (targetCapsule == null)
        {
            return target.transform.position;
        }
        return target.transform.position + Vector3.up * targetCapsule.height / 2;
    }
    public override void SetProjectile(float speed, Transform target , string tag, CharacterStats stats){
        base.SetProjectile( speed,  target , tag, stats);
        this.speed = speed;
        this.target = target;
    }
    protected override void OnEnable(){
        rb.gravityScale = 0.0f;
    }
    private void OnTriggerEnter2D(Collider2D other) {
        IDamageable damageable;
        if(other.TryGetComponent<IDamageable>(out damageable)) {
            if(other.tag == gameObject.tag) return;
            stats.DoDamage(damageable.Target(EffecHit));
            remove();
        }
    }
}