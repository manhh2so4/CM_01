using UnityEngine;
using UnityEngine.UI;

public class UI_selected : Singleton<UI_selected> {
    RectTransform rectTransform;
    protected override void Awake()
    {
        base.Awake();
        rectTransform = GetComponent<RectTransform>();
        gameObject.SetActive(false);
    }
    public static void SetSelected(RectTransform _rectTransform){
        
        if(_rectTransform == null){
            if(Instance.rectTransform.parent != null){
                Instance.rectTransform.SetParent(null);
                Instance.gameObject.SetActive(false);
            }
            return;
        }
        Instance.rectTransform.SetParent(_rectTransform);
        Instance.rectTransform.position = _rectTransform.position;
        Instance.rectTransform.sizeDelta = _rectTransform.sizeDelta;
        Instance.gameObject.SetActive(true);
    }
}