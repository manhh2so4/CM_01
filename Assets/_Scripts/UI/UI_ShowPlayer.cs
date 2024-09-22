using UnityEngine;
using UnityEngine.UI;

public class UI_ShowPlayer : MonoBehaviour {
    [SerializeField] private Image mImage;
    [SerializeField] private RectTransform rectTran;
    [SerializeField] SpriteRenderer spr;
    Sprite sprite = null;
    [SerializeField] Transform mGo;
    private void Start()
    {
        LoadComponent();
    }

    private void FixedUpdate() {
        setImage();
    }

    private void setImage()
    {
            sprite = spr.sprite;
            if(sprite == null) return;
            rectTran.localPosition = mGo.localPosition * 100f;
            rectTran.sizeDelta = sprite.bounds.size * 100;
            mImage.sprite = sprite;
    }
    private void LoadComponent()
    {
        mImage = GetComponent<Image>();
        rectTran = GetComponent<RectTransform>();
        spr = mGo.GetComponent<SpriteRenderer>();
    }
    private void OnValidate() {
        LoadComponent();
        setImage();
    }
}