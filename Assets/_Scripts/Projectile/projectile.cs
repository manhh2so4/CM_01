using UnityEngine;

public abstract class projectile : MonoBehaviour {
    [Header("base")]
    public float AutoDestroyTime = 5f;
    protected Rigidbody2D rb;
    protected SpriteRenderer mSPR;
    protected CharacterStats stats;
    [SerializeField] protected GameObject EffecCol;
    [SerializeField] protected GameObject EffecHit;
    [Header("-----------------")]
    protected const string DISABLE_METHOD_NAME = "remove";
    protected virtual void Awake() {
        rb = GetComponent<Rigidbody2D>();
        mSPR = GetComponent<SpriteRenderer>();
    } 
    protected virtual void OnEnable() {
        Invoke(DISABLE_METHOD_NAME,AutoDestroyTime) ;
    }
    public virtual void SetProjectile(float speed, Vector2 _dir, int damage , string tag,CharacterStats stats)
    {
        this.stats = stats;
        gameObject.tag = tag;
        rb.velocity = (_dir) * speed;
        float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
    public virtual void SetProjectile( int damage , string tag,CharacterStats stats)
    {
        this.stats = stats;
        gameObject.tag = tag;
    }
    protected void remove()
    {
        CancelInvoke(DISABLE_METHOD_NAME);
        Destroy(gameObject);
        //rb.velocity = Vector3.zero;
        //gameObject.SetActive(false);
    }
}