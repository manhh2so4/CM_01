using UnityEngine;
using TMPro;

public class UI_TextStat : MonoBehaviour{
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] TextMeshProUGUI upgradeText;
    void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
        upgradeText = transform.Find("Upgraded").GetComponent<TextMeshProUGUI>();    
    }
    public void SetTextUI( string _text , Color _color, string _upgradeText) {
        this.text.text = _text;

        if(_upgradeText != "") {
            this.upgradeText.text = _upgradeText;
        }else{
            this.upgradeText.text = "";
        }
        this.text.color = _color;
    }

}