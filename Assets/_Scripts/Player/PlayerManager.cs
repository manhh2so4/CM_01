using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private Player player;
    private Purse purse;
    private Inventory inventory;
    
    static PlayerManager instance;

    private void Awake() {
        instance = this;
        if(player == null) player = FindObjectOfType<Player>();
        purse = player.GetComponent<Purse>();   
        inventory = player.GetComponent<Inventory>();
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

}
