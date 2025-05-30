using System;
using UnityEngine;

public class Purse : MonoBehaviour {
    [SerializeField] float startingBalance = 400f;
    float balance = 0;

    public event Action onChange;

    private void Awake() {
        balance = startingBalance;
    }

    public float GetBalance()
    {
        return balance;
    }

    public void UpdateBalance(float amount)
    {
        balance += amount;
        onChange?.Invoke(); 
    }
}