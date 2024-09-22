using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_StatSlot : MonoBehaviour
{
    [SerializeField] private string statName;
    [SerializeField] private StatType statType;
    [SerializeField] private TextMeshProUGUI startValueText;
    [SerializeField] private TextMeshProUGUI startNameText;
    private void OnValidate() {
        gameObject.name = "Stat - " + statName;
        if(startNameText != null){
            startNameText.text = statName;
        }
    }
    private void Start() {
        UpdateStatValue();
    }
    public void UpdateStatValue(){
        if(startValueText != null){
            startValueText.text = PlayerManager.Instance.player.CharStats.GetStatOfType(statType).GetValue().ToString();
        }
    }
}
