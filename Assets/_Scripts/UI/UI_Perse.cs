using TMPro;
using UnityEngine;

public class UI_Perse : MonoBehaviour {
    Purse playerPurse = null;
    [SerializeField] TextMeshProUGUI balanceField;
    void Start()
    {
        playerPurse = PlayerManager.GetPlayer().GetComponent<Purse>();
        if(playerPurse != null){
            playerPurse.onChange += RefreshUI;
        }
        RefreshUI();
    }
    void RefreshUI(){
        balanceField.text = playerPurse.GetBalance().ToString();
    }
}