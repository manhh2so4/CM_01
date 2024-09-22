using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : CoreComponent
{
    [field: SerializeField] public Stat Health { get; private set; }
    [field: SerializeField] public Stat Poise { get; private set; }
    [SerializeField] private float currentPoise,currentHealth;
    protected override void Awake()
    {
        base.Awake();
        Health.Init();
        Poise.Init();
    }
    private void Update()
    {
        currentPoise = Poise.CurrentValue;
        currentHealth = Health.CurrentValue;
        if (Poise.CurrentValue <= 0) return; 
        Poise.Decrease(Time.deltaTime);
    }

}
