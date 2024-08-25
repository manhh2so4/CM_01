using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_StatSlot : MonoBehaviour
{
    [SerializeField] private string statName;
    [SerializeField] private TextMeshPro startValueText;
    [SerializeField] private TextMeshPro startNameText;
    private void OnValidate() {
        gameObject.name = "Stat - " + statName;
        if(startNameText != null){
            startNameText.text = statName;
        }
    }
    private void Start() {
        
    }
}
