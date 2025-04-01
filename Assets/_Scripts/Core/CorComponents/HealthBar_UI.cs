using UnityEngine;
using UnityEngine.UI;

public class HealthBar_UI : CoreComponent
{
    [SerializeField] Movement movement;
    [SerializeField] CharacterStats stats;

    [SerializeField] Transform BarSprite;

    Vector3 value = Vector2.one, location = Vector3.zero;
    protected override void Awake()
    {
        base.Awake();
        stats = core.GetCoreComponent<CharacterStats>();
        movement = core.GetCoreComponent<Movement>();
        BarSprite = transform.Find("BarSprite");
    }

    private void Start() {

        transform.Find("Hp_Bg").GetComponent<SpriteRenderer>().sortingLayerID = core.SortingLayerID;
        BarSprite.GetComponent<SpriteRenderer>().sortingLayerID = core.SortingLayerID;
        location.Set(0,core.height + 0.2f,0);
        transform.localPosition = location;
        transform.localScale = new Vector3( core.size.x , core.size.y , 1 );

    }
    private void OnEnable(){

        stats.onChangeHP += UpdateHealthUI;
        movement.OnFlip += FlipUI;   

    }
    private void OnDisable(){

        stats.onChangeHP -= UpdateHealthUI;
        movement.OnFlip -= FlipUI;
        
    }
    private void FlipUI() => transform.Rotate(0, 180, 0);
    private void UpdateHealthUI( )
    {
        //Debug.Log("UpdateHealthUI " + stats.Health.currentValue +"/" + stats.Health.GetValue());
        value.Set( Mathf.Clamp( (float)stats.Health.currentValue / stats.Health.GetValue() ,0,1) , 1f,1f);
        BarSprite.localScale = value;
        
    }
}
