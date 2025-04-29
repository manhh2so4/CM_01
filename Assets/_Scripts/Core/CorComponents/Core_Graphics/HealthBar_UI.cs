using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class HealthBar_UI : CoreComponent
{
    [SerializeField] Movement movement;
    [SerializeField] CharacterStats stats;
    [SerializeField] Transform BarSprite;
    [SerializeField] Transform Hp_Bg;
    bool isShow = false;
    float timeShow = 5;
    Vector3 value = Vector2.one, location = Vector3.zero;
    protected override void Awake()
    {
        base.Awake();
        stats = core.GetCoreComponent<CharacterStats>();
        movement = core.GetCoreComponent<Movement>();
        BarSprite = transform.Find("BarSprite");
        Hp_Bg = transform.Find("Hp_Bg");
    }

    public override void LogicUpdate()
    {
        ShowHP(isShow);
        if(isShow == false) return;

        if(timeShow > 0) timeShow -= Time.deltaTime;
        if(timeShow <= 0) isShow = false;
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        stats.Health.OnChangeValue += UpdateHealthUI;
        movement.OnFlip += FlipUI;   

    }
    protected override void OnDisable() {
        base.OnDisable();
        stats.Health.OnChangeValue -= UpdateHealthUI;
        movement.OnFlip -= FlipUI;   
    }
    void ShowHP(bool isShow){
        Hp_Bg.gameObject.SetActive(isShow);
        BarSprite.gameObject.SetActive(isShow);
    }

    protected override void SetData(){
        base.SetData();
        transform.Find("Hp_Bg").GetComponent<SpriteRenderer>().sortingLayerID = core.SortingLayerID;
        BarSprite.GetComponent<SpriteRenderer>().sortingLayerID = core.SortingLayerID;
        location.Set(0,core.Height + 0.2f,0);
        transform.localPosition = location;
        transform.localScale = new Vector3( core.size.x , core.size.y , 1 );
    }

    private void FlipUI() => transform.Rotate(0, 180, 0);
    private void UpdateHealthUI()
    {
        isShow = true;
        timeShow = 3;
        value.Set( Mathf.Clamp( (float)stats.Health.CurrentValue / stats.Health.GetValue() ,0,1) , 1f,1f);
        BarSprite.localScale = value;
    }
}
