using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPoiseDamageable
{
    void DamagePoise(float amount,Poisetype poisetype,Effect_Instance prefabEff);
}
