using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    static PlayerManager instance;
    [SerializeField] Player player;
    Purse purse;
    Inventory inventory;
    InteracButton interacButton;


    private void Awake() {
        instance = this;
        if(player == null) player = FindObjectOfType<Player>();
        purse = player.GetComponent<Purse>();   
        inventory = player.GetComponent<Inventory>();
        interacButton = player.GetComponent<InteracButton>();
    }

    public static Player GetPlayer(){
        if(instance == null) return null;
        return instance.player;
    }

    public static Purse GetPurse(){
        return instance.purse;
    }

    public static Inventory GetInventory(){
        return instance.inventory;
    }
    public static InteracButton GetInteracButton(){
        return instance.interacButton;
    }
}
