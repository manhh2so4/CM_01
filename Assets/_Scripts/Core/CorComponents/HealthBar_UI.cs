using UnityEngine;
using UnityEngine.UI;

public class HealthBar_UI : CoreComponent
{
    [SerializeField] private Movement movement;
    [SerializeField] private CharacterStats myStats;
    [SerializeField] private CapsuleCollider2D mCapsul;
    Transform BarSprite;

    Vector3 value = Vector2.one,location = Vector3.zero;
    protected override void Awake()
    {
        base.Awake();
        myStats = core.GetCoreComponent<CharacterStats>();
        movement = core.GetCoreComponent<Movement>();
        mCapsul = transform.parent.parent.GetComponent<CapsuleCollider2D>();
        BarSprite = transform.Find("BarSprite");
    }
    private void Start() {
        UpdateHealthUI();

        transform.Find("Hp_Bg").GetComponent<SpriteRenderer>().sortingLayerID = core.layerID;
        BarSprite.GetComponent<SpriteRenderer>().sortingLayerID = core.layerID;

        location.Set(0,mCapsul.size.y + 0.2f,0);
        transform.localPosition = location;
    }
    private void OnEnable(){
        myStats.onHealthChanged += UpdateHealthUI;
        movement.OnFlip += FlipUI;    
    }
    private void OnDisable() {
        myStats.onHealthChanged -= UpdateHealthUI;
        movement.OnFlip -= FlipUI;
    }
    private void FlipUI() => transform.Rotate(0, 180, 0);
    private void UpdateHealthUI()
    {
        value.Set( Mathf.Clamp( (float)myStats.currentHealth / myStats.GetMaxHealthValue(),0,1) , 1f,1f);
        BarSprite.localScale = value;
    }
}
