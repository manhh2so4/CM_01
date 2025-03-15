using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private Player player;
    
    static PlayerManager instance;
    private void Awake() {
        instance = this;
    }
    public static Player GetPlayer(){
        return instance.player;
    }
}
