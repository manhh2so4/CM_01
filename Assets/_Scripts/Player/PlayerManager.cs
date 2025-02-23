using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private Player player;
    public static PlayerManager Instance { get; private set; }
    private void Awake() {
        Instance = this;
    }
    public Player GetPlayer(){
        return player;
    }
}
