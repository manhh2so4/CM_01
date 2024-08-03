using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Core : MonoBehaviour
{
    public Movement Movement{get; set;}
    private void Awake()
    {
        Movement = GetComponentInChildren<Movement>();
    }

    public void LogicUpdate()
    {
        Movement.LogicUpdate();
    }

}
