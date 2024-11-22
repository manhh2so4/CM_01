using UnityEngine;
public class Projectile_anim : projectile {
    [SpritePreview]
    [SerializeField] Sprite[] sprites;
    [SerializeField] Pivot anchor;
    [SerializeField] float speedAnim;
    int FrameCurrent = 0; 
    public float life = 2;
    float startTime = 0,offsetY = 0;
    protected override void OnEnable() {
        FrameCurrent = 0;
        frameTimer = 99;
        startTime = Time.time;
        SetPivot();
        Paint();            
    }
    private void SetPivot(){
        float h = sprites[0].rect.height/100;
        float w = sprites[0].rect.width/100;
        switch (anchor)
        {               
            case Pivot.Top:
                offsetY = -h/2;
            break;
            case Pivot.Bot:
                offsetY = h/2 - 0.02f;
            break;
            case Pivot.Center:
            break;
        }
        Vector3 newPosition = new Vector3(0,offsetY,0);
        this.transform.localPosition += newPosition;
    }
    private void Update() {
        PlayEffect();
    }
    void PlayEffect(){   
        if(FrameRate(speedAnim)) return;
        Paint();
        FrameCurrent = ((FrameCurrent + 1)%sprites.Length);
    }
    protected void DestroyProjectile()
    {
        if(EffecCol) Instantiate(EffecCol, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D other) {
        IDamageable damageable;
        if(other.TryGetComponent<IDamageable>(out damageable)) {
            if(other.tag == gameObject.tag) return;
            stats.DoDamage(damageable.Target(EffecHit));
            remove();
            return;
        }
        DestroyProjectile();
    }
    private void Paint(){ 
        if(sprites == null) return;
        mSPR.sprite = sprites[FrameCurrent];          
    }
    bool FrameRate(float speed){        
        frameTimer += Time.deltaTime;
        if(frameTimer >= speed){
            frameTimer = 0;
            return false;
        }
        return true;
    }
    float frameTimer = 0;
}